using System;
using System.Globalization;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Timing;
using Abp.Zero.Configuration;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Caching;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Editions;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Timing;

namespace CAPS.CORPACCOUNTING.Configuration.Host
{
    
    public class HostSettingsAppService : CORPACCOUNTINGAppServiceBase, IHostSettingsAppService
    {
        private readonly IEmailSender _emailSender;
        private readonly EditionManager _editionManager;
        private readonly ITimeZoneService _timeZoneService;
        private readonly ICachingAppService _cachingAppService;

        public HostSettingsAppService(
            IEmailSender emailSender,
            EditionManager editionManager,
            ITimeZoneService timeZoneService, ICachingAppService cachingAppService)
        {
            _emailSender = emailSender;
            _editionManager = editionManager;
            _timeZoneService = timeZoneService;
            _cachingAppService = cachingAppService;
        }

        public async Task<HostSettingsEditDto> GetAllSettings()
        {
            var timezone = await SettingManager.GetSettingValueForApplicationAsync(TimingSettingNames.TimeZone);
            var hostSettings = new HostSettingsEditDto
            {
                General = new GeneralSettingsEditDto
                {
                    WebSiteRootAddress = await SettingManager.GetSettingValueAsync(AppSettings.General.WebSiteRootAddress),
                    Timezone = timezone,
                    TimezoneForComparison = timezone,
                    AuditSaveToDB = await SettingManager.GetSettingValueAsync<bool>(AppSettings.General.AuditSaveToDB),
                    //Get RedisCache
                    UseRedisCacheByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettings.General.UseRedisCacheByDefault)
                },
                TenantManagement = new TenantManagementSettingsEditDto
                {
                    AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.TenantManagement.AllowSelfRegistration),
                    IsNewRegisteredTenantActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault),
                    UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.TenantManagement.UseCaptchaOnRegistration)
                },
                UserManagement = new HostUserManagementSettingsEditDto
                {
                    IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin)
                },
                Email = new EmailSettingsEditDto
                {
                    DefaultFromAddress = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress),
                    DefaultFromDisplayName = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromDisplayName),
                    SmtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host),
                    SmtpPort = await SettingManager.GetSettingValueAsync<int>(EmailSettingNames.Smtp.Port),
                    SmtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName),
                    SmtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password),
                    SmtpDomain = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Domain),
                    SmtpEnableSsl = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                    SmtpUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
                }
            };

            var defaultTenantId = await SettingManager.GetSettingValueAsync(AppSettings.TenantManagement.DefaultEdition);
            if (!string.IsNullOrEmpty(defaultTenantId) && (await _editionManager.FindByIdAsync(Convert.ToInt32(defaultTenantId)) != null))
            {
                hostSettings.TenantManagement.DefaultEditionId = Convert.ToInt32(defaultTenantId);
            }

            var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Application, AbpSession.TenantId);
            if (hostSettings.General.Timezone == defaultTimeZoneId)
            {
                hostSettings.General.Timezone = string.Empty;
            }

            return hostSettings;
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Host_Settings)]
        public async Task UpdateAllSettings(HostSettingsEditDto input)
        {
            //General
            await SettingManager.ChangeSettingForApplicationAsync(AppSettings.General.WebSiteRootAddress, input.General.WebSiteRootAddress.EnsureEndsWith('/'));
            
            //RedisCacheFlag By Default Apllication Uses Redis Cache if we don't want to Use RedisCache we can set this setting as false
            await SettingManager.ChangeSettingForApplicationAsync(AppSettings.General.UseRedisCacheByDefault, input.General.UseRedisCacheByDefault.ToString());

            if (Clock.SupportsMultipleTimezone())
            {
                if (input.General.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Application, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForApplicationAsync(TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForApplicationAsync(TimingSettingNames.TimeZone, input.General.Timezone);
                }
            }
            await SettingManager.ChangeSettingForApplicationAsync(AppSettings.General.AuditSaveToDB, input.General.AuditSaveToDB.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

            //Tenant management
            await SettingManager.ChangeSettingForApplicationAsync(AppSettings.TenantManagement.AllowSelfRegistration, input.TenantManagement.AllowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault, input.TenantManagement.IsNewRegisteredTenantActiveByDefault.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(AppSettings.TenantManagement.UseCaptchaOnRegistration, input.TenantManagement.UseCaptchaOnRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

            var defaultEditionId = (input.TenantManagement.DefaultEditionId == null ? null : input.TenantManagement.DefaultEditionId.Value.ToString()) ?? "";

            await SettingManager.ChangeSettingForApplicationAsync(AppSettings.TenantManagement.DefaultEdition, defaultEditionId);

            //User management
            await SettingManager.ChangeSettingForApplicationAsync(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, input.UserManagement.IsEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

            //Email
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromAddress, input.Email.DefaultFromAddress);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromDisplayName, input.Email.DefaultFromDisplayName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Host, input.Email.SmtpHost);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Port, input.Email.SmtpPort.ToString(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UserName, input.Email.SmtpUserName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Password, input.Email.SmtpPassword);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Domain, input.Email.SmtpDomain);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.EnableSsl, input.Email.SmtpEnableSsl.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.Email.SmtpUseDefaultCredentials.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

            if (!input.General.UseRedisCacheByDefault)
                await ClearAllSumitCache();

        }
        

        public async Task SendTestEmail(SendTestEmailInput input)
        {
            var subject = L("TestEmail_Subject");
            var body = L("TestEmail_Body");

            await _emailSender.SendAsync(input.EmailAddress, subject, body);
        }

        /// <summary>
        /// When we set UseRedisCache flag as flase then we are clearing all Sumit Caches
        /// </summary>
        /// <returns></returns>
        private async Task ClearAllSumitCache()
        {
            await _cachingAppService.ClearCache(new IdInput<string>() {Id = CacheKeyStores.CacheAccountStore });
            await _cachingAppService.ClearCache(new IdInput<string>() { Id = CacheKeyStores.CacheDivisionStore });
            await _cachingAppService.ClearCache(new IdInput<string>() { Id = CacheKeyStores.CacheSubAccountRestrictionStore });
            await _cachingAppService.ClearCache(new IdInput<string>() { Id = CacheKeyStores.CacheBankAccountStore });
            await _cachingAppService.ClearCache(new IdInput<string>() { Id = CacheKeyStores.CacheEmployeeStore });
            await _cachingAppService.ClearCache(new IdInput<string>() { Id = CacheKeyStores.CacheVendorStore });
            await _cachingAppService.ClearCache(new IdInput<string>() { Id = CacheKeyStores.CacheSubAccountStore });
        }
    }
}
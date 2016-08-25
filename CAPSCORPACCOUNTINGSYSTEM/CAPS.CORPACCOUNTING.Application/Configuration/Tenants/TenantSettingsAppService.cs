using System.Globalization;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap.Configuration;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Configuration.Tenants.Dto;
using CAPS.CORPACCOUNTING.Timing;

namespace CAPS.CORPACCOUNTING.Configuration.Tenants
{
    public class TenantSettingsAppService : CORPACCOUNTINGAppServiceBase, ITenantSettingsAppService
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;
        private readonly ITimeZoneService _timeZoneService;

        public TenantSettingsAppService(
            IMultiTenancyConfig multiTenancyConfig,
            IAbpZeroLdapModuleConfig ldapModuleConfig,
            ITimeZoneService timeZoneService)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _ldapModuleConfig = ldapModuleConfig;
            _timeZoneService = timeZoneService;
        }

        /// <summary>
        /// Abp Method to GetAllSettings
        /// </summary>
        /// <returns></returns>
        public async Task<TenantSettingsEditDto> GetAllSettings()
        {
            var settings = new TenantSettingsEditDto
            {
                UserManagement = new TenantUserManagementSettingsEditDto
                {
                    AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowSelfRegistration),
                    IsNewRegisteredUserActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault),
                    IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                    UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration)
                }
            };

            if (!_multiTenancyConfig.IsEnabled || Clock.SupportsMultipleTimezone)
            {
                //General
                settings.General = new GeneralSettingsEditDto();
                if (!_multiTenancyConfig.IsEnabled)
                {
                    settings.General.WebSiteRootAddress = await SettingManager.GetSettingValueAsync(AppSettings.General.WebSiteRootAddress);
                }

                if (Clock.SupportsMultipleTimezone)
                {
                    var timezone = await SettingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, AbpSession.GetTenantId());

                    settings.General.Timezone = timezone;
                    settings.General.TimezoneForComparison = timezone;
                }

                var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                if (settings.General.Timezone == defaultTimeZoneId)
                {
                    settings.General.Timezone = string.Empty;
                }
            }


            if (!_multiTenancyConfig.IsEnabled)
            {
                //Email
                settings.Email = new EmailSettingsEditDto
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
                };

                //Ldap
                if (_ldapModuleConfig.IsEnabled)
                {
                    settings.Ldap = new LdapSettingsEditDto
                    {
                        IsModuleEnabled = true,
                        IsEnabled = await SettingManager.GetSettingValueAsync<bool>(LdapSettingNames.IsEnabled),
                        Domain = await SettingManager.GetSettingValueAsync(LdapSettingNames.Domain),
                        UserName = await SettingManager.GetSettingValueAsync(LdapSettingNames.UserName),
                        Password = await SettingManager.GetSettingValueAsync(LdapSettingNames.Password),
                    };
                }
                else
                {
                    settings.Ldap = new LdapSettingsEditDto
                    {
                        IsModuleEnabled = false
                    };
                }
            }

            return settings;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Tenant_Settings)]
        public async Task UpdateAllSettings(TenantSettingsEditDto input)
        {
            //User management
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.AllowSelfRegistration, input.UserManagement.AllowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, input.UserManagement.IsNewRegisteredUserActiveByDefault.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, input.UserManagement.IsEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.UseCaptchaOnRegistration, input.UserManagement.UseCaptchaOnRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            
            if (Clock.SupportsMultipleTimezone)
            {
                if (input.General.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, input.General.Timezone);
                }
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                input.ValidateHostSettings();

                //General
                await SettingManager.ChangeSettingForApplicationAsync(AppSettings.General.WebSiteRootAddress, input.General.WebSiteRootAddress.EnsureEndsWith('/'));

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

                //Ldap
                if (_ldapModuleConfig.IsEnabled)
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.IsEnabled, input.Ldap.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Domain, input.Ldap.Domain.IsNullOrWhiteSpace() ? null : input.Ldap.Domain);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.UserName, input.Ldap.UserName.IsNullOrWhiteSpace() ? null : input.Ldap.UserName);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Password, input.Ldap.Password.IsNullOrWhiteSpace() ? null : input.Ldap.Password);
                }
            }
        }



        /// <summary>
        /// Sumit Method to GetAllSettings
        /// </summary>
        /// <returns></returns>
        public async Task<TenantSettingsEditDto> GetAllTenantSettings()
        {
            var settings = new TenantSettingsEditDto
            {
                UserManagement = new TenantUserManagementSettingsEditDto
                {
                    AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowSelfRegistration),
                    IsNewRegisteredUserActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault),
                    IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                    UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration)
                }
            };


            settings.CompanySettings = new CompanySettingsEditDto()
            {
                IsAllowDuplicateAPInvoiceNos = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowDuplicateAPInvoiceNos),

                IsAllowDuplicateARInvoiceNos = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowDuplicateARInvoiceNos),
                SetDefaultAPTerms = await SettingManager.GetSettingValueAsync<int>(AppSettings.UserManagement.SetDefaultAPTerms),
                SetDefaultARTerms = await SettingManager.GetSettingValueAsync<int>(AppSettings.UserManagement.SetDefaultARTerms),
                IsAllowAccountnumbersStartingwithZero = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowAccountNumbersStartingWithZero),
                IsImportPOlogsfromProducersActualUploads = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.ImportPOlogsfromProducersActualuploads),
                BuildAPuponCCstatementPosting = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.BuildAPuponCCstatementPosting),
                BuildAPuponPayrollPosting = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.BuildAPuponPayrollPosting),
                ARAgingDate = await SettingManager.GetSettingValueAsync(AppSettings.UserManagement.ARAgingDate),
                APAgingDate = await SettingManager.GetSettingValueAsync(AppSettings.UserManagement.APAgingDate),
                DepositGracePeriods = await SettingManager.GetSettingValueAsync<int>(AppSettings.UserManagement.DepositGracePeriods),
                PaymentsGracePeriods = await SettingManager.GetSettingValueAsync<int>(AppSettings.UserManagement.PaymentGracePeriods),
                AllowTransactionsJobWithGL = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowTransactionsactionsJobWithGL),
                DefaultAPPostingDate = await SettingManager.GetSettingValueAsync(AppSettings.UserManagement.APPostingDateDefault),
                DefaultBank = await SettingManager.GetSettingValueAsync<long>(AppSettings.UserManagement.DefaultBank),
                POAutoNumberingforProjects = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.POAutoNumberingforProjects),
                POAutoNumberingforDivisions = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.POAutoNumberingforDivisions),
            };


            if (!_multiTenancyConfig.IsEnabled || Clock.SupportsMultipleTimezone)
            {
                //General
                settings.General = new GeneralSettingsEditDto();
                if (!_multiTenancyConfig.IsEnabled)
                {
                    settings.General.WebSiteRootAddress = await SettingManager.GetSettingValueAsync(AppSettings.General.WebSiteRootAddress);
                }

                if (Clock.SupportsMultipleTimezone)
                {
                    var timezone = await SettingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, AbpSession.GetTenantId());

                    settings.General.Timezone = timezone;
                    settings.General.TimezoneForComparison = timezone;
                }

                var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                if (settings.General.Timezone == defaultTimeZoneId)
                {
                    settings.General.Timezone = string.Empty;
                }
            }


            if (!_multiTenancyConfig.IsEnabled)
            {
                //Email
                settings.Email = new EmailSettingsEditDto
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
                };

                //Ldap
                if (_ldapModuleConfig.IsEnabled)
                {
                    settings.Ldap = new LdapSettingsEditDto
                    {
                        IsModuleEnabled = true,
                        IsEnabled = await SettingManager.GetSettingValueAsync<bool>(LdapSettingNames.IsEnabled),
                        Domain = await SettingManager.GetSettingValueAsync(LdapSettingNames.Domain),
                        UserName = await SettingManager.GetSettingValueAsync(LdapSettingNames.UserName),
                        Password = await SettingManager.GetSettingValueAsync(LdapSettingNames.Password),
                    };
                }
                else
                {
                    settings.Ldap = new LdapSettingsEditDto
                    {
                        IsModuleEnabled = false
                    };
                }
            }

            return settings;
        }

        /// <summary>
        /// Sumit Method to  Update AllSettings
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_Tenant_Settings)]
        public async Task UpdateAllTenantSettings(TenantSettingsEditDto input)
        {
            //User management
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.AllowSelfRegistration, input.UserManagement.AllowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, input.UserManagement.IsNewRegisteredUserActiveByDefault.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, input.UserManagement.IsEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.UseCaptchaOnRegistration, input.UserManagement.UseCaptchaOnRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            //Sumit CompanySettings 
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.AllowDuplicateARInvoiceNos, input.CompanySettings.IsAllowDuplicateARInvoiceNos.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.AllowDuplicateAPInvoiceNos, input.CompanySettings.IsAllowDuplicateAPInvoiceNos.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.SetDefaultAPTerms, input.CompanySettings.SetDefaultAPTerms.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.SetDefaultARTerms, input.CompanySettings.SetDefaultARTerms.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.AllowAccountNumbersStartingWithZero, input.CompanySettings.IsAllowAccountnumbersStartingwithZero.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.ImportPOlogsfromProducersActualuploads, input.CompanySettings.IsImportPOlogsfromProducersActualUploads.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.BuildAPuponCCstatementPosting, input.CompanySettings.BuildAPuponCCstatementPosting.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.BuildAPuponPayrollPosting, input.CompanySettings.BuildAPuponPayrollPosting.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.ARAgingDate, input.CompanySettings.ARAgingDate);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.APAgingDate, input.CompanySettings.APAgingDate);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.DepositGracePeriods, input.CompanySettings.DepositGracePeriods.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.PaymentGracePeriods, input.CompanySettings.PaymentsGracePeriods.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.AllowTransactionsactionsJobWithGL, input.CompanySettings.AllowTransactionsJobWithGL.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.APPostingDateDefault, input.CompanySettings.DefaultAPPostingDate);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.DefaultBank, input.CompanySettings.DefaultBank.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.POAutoNumberingforProjects, input.CompanySettings.POAutoNumberingforProjects.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.POAutoNumberingforDivisions, input.CompanySettings.POAutoNumberingforDivisions.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

            if (Clock.SupportsMultipleTimezone)
            {
                if (input.General.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, input.General.Timezone);
                }
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                input.ValidateHostSettings();

                //General
                await SettingManager.ChangeSettingForApplicationAsync(AppSettings.General.WebSiteRootAddress, input.General.WebSiteRootAddress.EnsureEndsWith('/'));

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

                //Ldap
                if (_ldapModuleConfig.IsEnabled)
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.IsEnabled, input.Ldap.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Domain, input.Ldap.Domain.IsNullOrWhiteSpace() ? null : input.Ldap.Domain);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.UserName, input.Ldap.UserName.IsNullOrWhiteSpace() ? null : input.Ldap.UserName);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Password, input.Ldap.Password.IsNullOrWhiteSpace() ? null : input.Ldap.Password);
                }
            }
        }
    }
}
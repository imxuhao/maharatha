using System;
using System.Reflection;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Zero;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap;
using Abp.Zero.Ldap.Configuration;
using CAPS.CORPACCOUNTING.Authorization.Ldap;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.BackgroundJobs;
using CAPS.CORPACCOUNTING.Configuration;
using CAPS.CORPACCOUNTING.Debugging;
using CAPS.CORPACCOUNTING.Features;
using CAPS.CORPACCOUNTING.Journals;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.MultiTenancy;
using CAPS.CORPACCOUNTING.Notifications;

namespace CAPS.CORPACCOUNTING
{
    /// <summary>
    /// Core (domain) module of the application.
    /// </summary>
    [DependsOn(typeof(AbpZeroCoreModule), typeof(AbpZeroLdapModule), typeof(AbpAutoMapperModule))]
    public class CORPACCOUNTINGCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Add/remove localization sources
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "CORPACCOUNTING",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "CAPS.CORPACCOUNTING.Localization.CORPACCOUNTING"
                        )
                    )
                );

            //Adding feature providers
            Configuration.Features.Providers.Add<AppFeatureProvider>();

            //Adding setting providers
            Configuration.Settings.Providers.Add<AppSettingProvider>();

            //Adding notification providers
            Configuration.Notifications.Providers.Add<AppNotificationProvider>();

            //Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = true;

            //Enable LDAP authentication (It can be enabled only if MultiTenancy is disabled!)
            //Configuration.Modules.ZeroLdap().Enable(typeof(AppLdapAuthenticationSource));

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Add mappings. Example:
                //configuration.CreateMap<MySourceClass, MyDestClass>();
                cfg.CreateMap<CoaUnit, CoaUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                cfg.CreateMap<EmployeeUnit, EmployeeUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                cfg.CreateMap<CustomerUnit, CustomerUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                cfg.CreateMap<Role, Role>().ForMember(u => u.Id, ap => ap.Ignore());
                cfg.CreateMap<VendorUnit, VendorUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                cfg.CreateMap<JournalEntryDocumentUnit, JournalEntryDocumentUnit>().ForMember(dto => dto.Id, options => options.Ignore());
                cfg.CreateMissingTypeMaps =true;
            });

            //Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            if (DebugHelper.IsDebug)
            {
                //Disabling email sending in debug mode
                IocManager.Register<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IRecurringJobManager, HangfireRecurringJobManager>();
        }
    }
}

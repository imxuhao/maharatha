using System.Collections.Generic;
using System.Configuration;
using Abp.Configuration;

namespace CAPS.CORPACCOUNTING.Configuration
{
    /// <summary>
    /// Defines settings for the application.
    /// See <see cref="AppSettings"/> for setting names.
    /// </summary>
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       //Host settings
                       new SettingDefinition(AppSettings.General.WebSiteRootAddress, "http://localhost:6240/"),
                        new SettingDefinition(AppSettings.General.AuditSaveToDB,ConfigurationManager.AppSettings[AppSettings.General.AuditSaveToDB] ?? "false"),
                       new SettingDefinition(AppSettings.TenantManagement.AllowSelfRegistration, ConfigurationManager.AppSettings[AppSettings.TenantManagement.UseCaptchaOnRegistration] ?? "true"),
                       new SettingDefinition(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault, ConfigurationManager.AppSettings[AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault] ?? "false"),
                       new SettingDefinition(AppSettings.TenantManagement.UseCaptchaOnRegistration, ConfigurationManager.AppSettings[AppSettings.TenantManagement.UseCaptchaOnRegistration] ?? "true"),
                       new SettingDefinition(AppSettings.TenantManagement.DefaultEdition, ConfigurationManager.AppSettings[AppSettings.TenantManagement.DefaultEdition] ?? ""),

                       //Tenant settings
                       new SettingDefinition(AppSettings.UserManagement.AllowSelfRegistration, ConfigurationManager.AppSettings[AppSettings.UserManagement.UseCaptchaOnRegistration] ?? "true", scopes: SettingScopes.Tenant),
                       new SettingDefinition(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, ConfigurationManager.AppSettings[AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault] ?? "false", scopes: SettingScopes.Tenant),
                       new SettingDefinition(AppSettings.UserManagement.UseCaptchaOnRegistration, ConfigurationManager.AppSettings[AppSettings.UserManagement.UseCaptchaOnRegistration] ?? "true", scopes: SettingScopes.Tenant),

                    //Sumit companySettings
                    new SettingDefinition(AppSettings.UserManagement.AllowDuplicateAPInvoiceNos, ConfigurationManager.AppSettings[AppSettings.UserManagement.AllowDuplicateAPInvoiceNos] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.AllowDuplicateARInvoiceNos, ConfigurationManager.AppSettings[AppSettings.UserManagement.AllowDuplicateARInvoiceNos] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.SetDefaultAPTerms, ConfigurationManager.AppSettings[AppSettings.UserManagement.SetDefaultAPTerms]  ?? "0", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.SetDefaultARTerms, ConfigurationManager.AppSettings[AppSettings.UserManagement.SetDefaultARTerms]  ?? "0", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.AllowAccountNumbersStartingWithZero, ConfigurationManager.AppSettings[AppSettings.UserManagement.AllowAccountNumbersStartingWithZero] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.ImportPOlogsfromProducersActualuploads, ConfigurationManager.AppSettings[AppSettings.UserManagement.ImportPOlogsfromProducersActualuploads] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.BuildAPuponCCstatementPosting, ConfigurationManager.AppSettings[AppSettings.UserManagement.BuildAPuponCCstatementPosting] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.BuildAPuponPayrollPosting, ConfigurationManager.AppSettings[AppSettings.UserManagement.BuildAPuponPayrollPosting] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.ARAgingDate, ConfigurationManager.AppSettings[AppSettings.UserManagement.ARAgingDate]  ?? "1", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.APAgingDate, ConfigurationManager.AppSettings[AppSettings.UserManagement.APAgingDate]  ?? "1" , scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.DepositGracePeriods, ConfigurationManager.AppSettings[AppSettings.UserManagement.DepositGracePeriods]?? "0" , scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.PaymentGracePeriods, ConfigurationManager.AppSettings[AppSettings.UserManagement.PaymentGracePeriods]?? "0" , scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.AllowTransactionsactionsJobWithGL, ConfigurationManager.AppSettings[AppSettings.UserManagement.AllowTransactionsactionsJobWithGL] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.APPostingDateDefault, ConfigurationManager.AppSettings[AppSettings.UserManagement.APPostingDateDefault] ?? "1", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.DefaultBank, ConfigurationManager.AppSettings[AppSettings.UserManagement.DefaultBank] ?? "0", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.POAutoNumberingforProjects, ConfigurationManager.AppSettings[AppSettings.UserManagement.POAutoNumberingforProjects] ?? "false", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettings.UserManagement.POAutoNumberingforDivisions, ConfigurationManager.AppSettings[AppSettings.UserManagement.POAutoNumberingforDivisions] ?? "false", scopes: SettingScopes.Tenant),
                   };
        }
    }
}

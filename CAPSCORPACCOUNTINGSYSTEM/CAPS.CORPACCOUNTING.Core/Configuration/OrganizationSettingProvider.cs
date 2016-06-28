using Abp.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Configuration
{
   public class OrganizationSettingProvider: SettingProvider
    {

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       //Organization settings
                        new SettingDefinition(AppSettings.OrganizationManagement.AllowAccountNumbersStartingWithZero, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowAccountNumbersStartingWithZero] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.AllowDuplicateAPInvoiceNos, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowDuplicateAPInvoiceNos] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.AllowDuplicateARInvoiceNos, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowDuplicateARInvoiceNos] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.AllowTransactionsactionsJobWithGL, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowTransactionsactionsJobWithGL] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.APAgingDate, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.APAgingDate] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.APPostingDateDefault, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.APPostingDateDefault] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.ARAgingDate, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.ARAgingDate] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.BuildAPuponCCstatementPosting, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.BuildAPuponCCstatementPosting] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.BuildAPuponPayrollPosting, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.BuildAPuponPayrollPosting] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.DefaultBank, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.DefaultBank] ??"0"),
                        new SettingDefinition(AppSettings.OrganizationManagement.DepositGracePeriods, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.DepositGracePeriods] ??"0"),
                        new SettingDefinition(AppSettings.OrganizationManagement.ImportPOlogsfromProducersActualuploads, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.ImportPOlogsfromProducersActualuploads] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.PaymentGracePeriods, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.PaymentGracePeriods] ?? "0"),
                        new SettingDefinition(AppSettings.OrganizationManagement.POAutoNumbering, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.POAutoNumbering] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.SetDefaultAPTerms, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.SetDefaultAPTerms] ?? "false"),
                        new SettingDefinition(AppSettings.OrganizationManagement.SetDefaultARTerms, ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.SetDefaultARTerms] ?? "false"),
                   };
        }
    }
}

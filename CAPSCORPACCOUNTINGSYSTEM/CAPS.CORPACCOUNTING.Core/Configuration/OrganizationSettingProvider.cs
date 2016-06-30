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
                        new SettingDefinition(name: AppSettings.OrganizationManagement.AllowAccountNumbersStartingWithZero,defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowAccountNumbersStartingWithZero] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.AllowDuplicateAPInvoiceNos, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowDuplicateAPInvoiceNos] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.AllowDuplicateARInvoiceNos, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowDuplicateARInvoiceNos] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.AllowTransactionsactionsJobWithGL, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.AllowTransactionsactionsJobWithGL] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.APAgingDate, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.APAgingDate] ,isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.APPostingDateDefault, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.APPostingDateDefault] ,isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.ARAgingDate, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.ARAgingDate] ,isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.BuildAPuponCCstatementPosting, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.BuildAPuponCCstatementPosting] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.BuildAPuponPayrollPosting, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.BuildAPuponPayrollPosting] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.DefaultBank, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.DefaultBank] ,isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.DepositGracePeriods, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.DepositGracePeriods],isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.ImportPOlogsfromProducersActualuploads, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.ImportPOlogsfromProducersActualuploads] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.PaymentGracePeriods, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.PaymentGracePeriods] ,isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.POAutoNumbering, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.POAutoNumbering] ?? "False",isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.SetDefaultAPTerms, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.SetDefaultAPTerms] ,isVisibleToClients:true),
                        new SettingDefinition(name:AppSettings.OrganizationManagement.SetDefaultARTerms, defaultValue: ConfigurationManager.AppSettings[AppSettings.OrganizationManagement.SetDefaultARTerms])
                   };
        }
    }
}

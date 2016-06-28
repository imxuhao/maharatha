namespace CAPS.CORPACCOUNTING.Configuration
{
    /// <summary>
    /// Defines string constants for setting names in the application.
    /// See <see cref="AppSettingProvider"/> for setting definitions.
    /// </summary>
    public static class AppSettings
    {
        public static class General
        {
            public const string WebSiteRootAddress = "App.General.WebSiteRootAddress";
        }

        public static class TenantManagement
        {
            public const string AllowSelfRegistration = "App.TenantManagement.AllowSelfRegistration";
            public const string IsNewRegisteredTenantActiveByDefault = "App.TenantManagement.IsNewRegisteredTenantActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.TenantManagement.UseCaptchaOnRegistration";
            public const string DefaultEdition = "App.TenantManagement.DefaultEdition";
        }

        public static class UserManagement
        {
            public const string AllowSelfRegistration = "App.UserManagement.AllowSelfRegistration";
            public const string IsNewRegisteredUserActiveByDefault = "App.UserManagement.IsNewRegisteredUserActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.UserManagement.UseCaptchaOnRegistration";
        }

        public static class OrganizationManagement
        {
            public const string AllowDuplicateAPInvoiceNos = "Sumit.Org.AllowDuplicateAPinvoiceNos";
            public const string AllowDuplicateARInvoiceNos = "Sumit.Org.AllowDuplicateARinvoiceNos";
            public const string SetDefaultAPTerms = "Sumit.Org.SetDefaultAPTerms";
            public const string SetDefaultARTerms = "Sumit.Org.SetDefaultARTerms";
            public const string AllowAccountNumbersStartingWithZero = "Sumit.Org.AllowAccountnumbersStartingwithZero";
            public const string ImportPOlogsfromProducersActualuploads = "Sumit.Org.ImportPOlogsfromProducersActualuploads";
            public const string BuildAPuponCCstatementPosting = "Sumit.Org.BuildAPuponCCstatementPosting";
            public const string BuildAPuponPayrollPosting = "Sumit.Org.BuildAPuponPayrollPosting";
            public const string POAutoNumbering = "Sumit.Org.POAutoNumbering";
            public const string ARAgingDate = "Sumit.Org.ARAgingDate";
            public const string APAgingDate = "Sumit.Org.ARAgingDate";
            public const string DepositGracePeriods = "Sumit.Org.DepositGracePeriods";
            public const string PaymentGracePeriods = "Sumit.Org.PaymentGracePeriods";
            public const string AllowTransactionsactionsJobWithGL = "Sumit.Org.AllowtransactionsactionsJobWithGL";
            public const string APPostingDateDefault = "Sumit.Org.APpostingDateDefault";
            public const string DefaultBank = "Sumit.Org.DefaultBank";
        }
    }
}
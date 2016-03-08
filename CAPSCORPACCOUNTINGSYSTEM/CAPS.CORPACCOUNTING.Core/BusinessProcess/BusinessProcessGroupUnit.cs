using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    public enum BusinessProcessGroupCategory
    {
        [Display(Name = "Dashboard Render")]
        DashboardRender = 1,
        [Display(Name = "System Processes")]
        SystemProcesses = 2,
        [Display(Name = "General Ledger - (Chart Of Accounts)")]
        GeneralLedgerChartOfAccounts = 3,
        [Display(Name = "General Ledger - (Transactions)")]
        GeneralLedgerTransactions = 4,
        [Display(Name = "General Ledger - (Global)")]
        GeneralLedgerGlobal = 5,
        [Display(Name = "Global - (All Systems)")]
        GlobalAllSystems = 6,
        [Display(Name = "General Ledger - ( Inquiry/Reporting )")]
        GeneralLedgerInquiryReporting = 7,
        [Display(Name = "Projects")]
        Projects = 8,
        [Display(Name = "Receivables")]
        Receivables = 9,
        [Display(Name = "Banking")]
        Banking = 10,
        [Display(Name = "Credit Cards")]
        CreditCards = 11,
        [Display(Name = "Shipping")]
        Shipping = 12,
        [Display(Name = "Payables")]
        Payables = 13,
        [Display(Name = "Purchasing")]
        Purchasing = 14,
        [Display(Name = "Petty Cash")]
        PettyCash = 15
    }
    
    /// <summary>
    ///  BusinessProcessGroup is the table name in Lajit
    /// </summary>
    [Table("CAPS_BusinessProcessGroup")]
    public class BusinessProcessGroupUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxCaptionLength = 20;
        private const int MaxDescriptionLength = 100;

        /// <summary> Overriding the ID column with BusinessProcessGroupId field. </summary>
        [Column("BusinessProcessGroupId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [MaxLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the TypeOfBusinessProcessGroupId field. </summary>
        public virtual short TypeOfBusinessProcessGroupId { get; set; }
        [ForeignKey("TypeOfBusinessProcessGroupId")]
        public virtual TypeOfBusinessProcessGroupUnit TypeOfBusinessProcessGroupUnit { get; set; }

        /// <summary>Gets or sets the BusinessProcessGroupCategoryId field. </summary>
        public virtual BusinessProcessGroupCategory BusinessProcessGroupCategoryId { get; set; }

        /// <summary>Gets or sets the FormNameId field. </summary>
        public virtual int? FormNameId { get; set; }

        /// <summary>Gets or sets the IsProcessFlowRedirected field. </summary>
        public virtual bool IsProcessFlowRedirected { get; set; }

        /// <summary>Gets or sets the IsLogRequired field. </summary>
        public virtual bool IsLogRequired { get; set; }

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public virtual bool IsPrivate { get; set; }

        /// <summary>Gets or sets the IsOptional field. </summary>
        public virtual bool IsOptional { get; set; }

        /// <summary>Gets or sets the IsNotificationAllowed field. </summary>
        public virtual bool IsNotificationAllowed { get; set; }

        /// <summary>Gets or sets the NotificationMessageId field. </summary>
        public virtual int? NotificationMessageId { get; set; }

        /// <summary>Gets or sets the TimeOutPeriodBeforeRoleBroadCastDayHourMin field. </summary>
        public virtual short? TimeOutPeriodBeforeRoleBroadCastDayHourMin { get; set; }

        /// <summary>Gets or sets the TimeOutMessageId field. </summary>
        public virtual int? TimeOutMessageId { get; set; }

        /// <summary>Gets or sets the IsApprovalAllowed field. </summary>
        public virtual bool IsApprovalAllowed { get; set; }

        /// <summary>Gets or sets the ApprovalMessageId field. </summary>
        public virtual int? ApprovalMessageId { get; set; }

        /// <summary>Gets or sets the DashBoardGroupId field. </summary>
        public virtual int? DashBoardGroupId { get; set; }

        /// <summary>Gets or sets the DashBoardStepId field. </summary>
        public virtual int? DashBoardStepId { get; set; }

        /// <summary>Gets or sets the DashBoardApplicationSequence field. </summary>
        public virtual short? DashBoardApplicationSequence { get; set; }

        /// <summary>Gets or sets the IsStandardBrowserRequired field. </summary>
        public virtual bool IsStandardBrowserRequired { get; set; }

        /// <summary>Gets or sets the StandardIconId field. </summary>
        public virtual short? StandardIconId { get; set; }

        /// <summary>Gets or sets the WarningIconId field. </summary>
        public virtual short? WarningIconId { get; set; }

        /// <summary>Gets or sets the ErrorIconId field. </summary>
        public virtual short? ErrorIconId { get; set; }

        /// <summary>Gets or sets the IsUserInitiated field. </summary>
        public virtual bool IsUserInitiated { get; set; }

        /// <summary>Gets or sets the IsMultipleWorkFlowAllowed field. </summary>
        public virtual bool IsMultipleWorkFlowAllowed { get; set; }

        /// <summary>Gets or sets the IsWorkFlowNameAssignedByUser field. </summary>
        public virtual bool IsWorkFlowNameAssignedByUser { get; set; }

        /// <summary>Gets or sets the TestedByUser field. </summary>
        public virtual string TestedByUser { get; set; }

        /// <summary>Gets or sets the DateTested field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateTested { get; set; }

        /// <summary>Gets or sets the ApprovedByUser field. </summary>
        public virtual string ApprovedByUser { get; set; }

        /// <summary>Gets or sets the DateApproved field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsPopUp field. </summary>
        public virtual bool IsPopUp { get; set; }

        /// <summary>Gets or sets the IsDashBoardFriendly field. </summary>
        public virtual bool IsDashBoardFriendly { get; set; }

        /// <summary>Gets or sets the IsSmartPhoneFriendly field. </summary>
        public virtual bool IsSmartPhoneFriendly { get; set; }

        /// <summary>Gets or sets the IsReportBpgid field. </summary>
        public virtual bool? IsReportBpgid { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}

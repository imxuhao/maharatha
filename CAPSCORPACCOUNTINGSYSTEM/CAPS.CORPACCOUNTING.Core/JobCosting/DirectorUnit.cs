using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{

    public enum ProfitShareTerms
    {
        [Display(Name = "Under or Over")]
        UnderOrOver = 1,
        [Display(Name = "The %")]
        ThePercentage = 2,
        [Display(Name = "Milestone Marker")]
        MilestoneMarker = 3,
        [Display(Name = "Applying L&E")]
        ApplyingLandE = 4,
    }

    public enum ContractTypes
    {
        [Display(Name = "Live Action")]
        LiveAction = 1,
        [Display(Name = "Digital")]
        Digital = 2,
        [Display(Name = "Interactive Campaigns")]
        InteractiveCampaigns = 3,
        [Display(Name = "Feature")]
        Feature = 4,
        [Display(Name = "Documentary")]
        Documentary = 5,
        [Display(Name = "Webisodic")]
        Webisodic = 6,
        [Display(Name = "Episodic")]
        Episodic = 7,
    }

    [Table("CAPS_Director")]
    public class DirectorUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxNameLength = 100;
        public const int MaxTaxIdLength = 15;
        public const int MaxCommentsLength = 500;
        public const int MaxBusinessManagerLength = 500;
        public const int MaxPayToNameLength = 500;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with DirectorId</summary>
        [Column("DirectorId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string FirstName { get; set; }

        /// <summary>Gets or sets the LastName field. </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string LastName { get; set; }

        /// <summary>Gets or sets the [SSNTaxID] field. </summary>
        [StringLength(MaxTaxIdLength)]
        public virtual string SSNTaxId { get; set; }

        /// <summary>Gets or sets the [FederalTaxID] field. </summary>
        [StringLength(MaxTaxIdLength)]
        public virtual string FederalTaxId { get; set; }

        /// <summary>Gets or sets the BusinessManager field. </summary>
        [StringLength(MaxBusinessManagerLength)]
        public virtual string BusinessManager { get; set; }

        /// <summary>Gets or sets the PayToName field. </summary>
        [StringLength(MaxPayToNameLength)]
        public virtual string PayToName { get; set; }

        /// <summary>Gets or sets the Is Is1099 field. </summary>
        public virtual bool? Is1099 { get; set; }

        /// <summary>Gets or sets the Typeof1099T4 field. </summary>
        public virtual Typeof1099T4? Typeof1099Id { get; set; }

        /// <summary>Gets or sets the ContractDate field. </summary>
        public virtual DateTime? ContractDate { get; set; }

        /// <summary>Gets or sets the ExpirationDate field. </summary>
        public virtual DateTime? ExpirationDate { get; set; }

        /// <summary>Gets or sets the Fee field. </summary>
        [Column(TypeName = "Money")]
        public virtual decimal? Fee { get; set; }

        /// <summary>Gets or sets the ContractTypeId field. </summary>
        public virtual ContractTypes? ContractTypeId { get; set; }

        /// <summary>Gets or sets the ProfitShareTermId field. </summary>
        public virtual ProfitShareTerms? ProfitShareTermId { get; set; }

        /// <summary>Gets or sets the Comments field. </summary>
        [StringLength(MaxCommentsLength)]
        public virtual string Comments { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public DirectorUnit()
        {
        }
        public DirectorUnit(string firstname, string lastname, string ssntaxid, string federaltaxid, string businessmanager, string paytoname, bool? is1099, Typeof1099T4? typeof1099id,
             DateTime? contractdate, DateTime? expirationdate, decimal? fee, ContractTypes? contracttypeid, ProfitShareTerms? profitsharetermid, string comments, long? organizationunitid)
        {
            FirstName = firstname;
            LastName = lastname;
            SSNTaxId = ssntaxid;
            FederalTaxId = federaltaxid;
            BusinessManager = businessmanager;
            PayToName = paytoname;
            Is1099 = is1099;
            Typeof1099Id = typeof1099id;
            ContractDate = contractdate;
            ExpirationDate = expirationdate;
            Fee = fee;
            ContractTypeId = contracttypeid;
            ProfitShareTermId = profitsharetermid;
            Comments = comments;
            OrganizationUnitId = organizationunitid;
        }
    }
}

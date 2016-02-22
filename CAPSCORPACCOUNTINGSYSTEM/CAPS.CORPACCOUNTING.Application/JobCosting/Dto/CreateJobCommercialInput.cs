using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public  class CreateJobCommercialInput : IInputDto
    {       

        /// <summary>Gets or Sets JobId Field.  </summary>
        [Range(0, Int32.MaxValue)]
        public int JobId { get; set; }       

        /// <summary>Gets or Sets BidDate Field.  </summary>
        public DateTime? BidDate { get; set; }

        /// <summary>Gets or Sets AwardDate Field.  </summary>
        public DateTime? AwardDate { get; set; }

        /// <summary>Gets or Sets ShootingDate Field.  </summary>
        public DateTime? ShootingDate { get; set; }

        /// <summary>Gets or Sets WrapDate Field.  </summary>
        public DateTime? WrapDate { get; set; }

        /// <summary>Gets or Sets RoughCutDate Field.  </summary>
        public DateTime? RoughCutDate { get; set; }

        /// <summary>Gets or Sets AirDate Field.  </summary>
        public DateTime? AirDate { get; set; }

        /// <summary>Gets or Sets DateClosed Field.  </summary>
        public DateTime? DateClosed { get; set; }

        /// <summary>Gets or Sets FinalShootDate Field.  </summary>
        public DateTime? FinalShootDate { get; set; }

        /// <summary>Gets or Sets ProductOwner Field.  </summary>
        [StringLength(JobCommercialUnit.MaxProductLength)]
        public string ProductOwner { get; set; }

        /// <summary>Gets or Sets ProductName Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string ProductName { get; set; }

        /// <summary>Gets or Sets ExecutiveProducerId Field.  </summary>
        public int? ExecutiveProducerId { get; set; }       

        /// <summary>Gets or Sets DirectorEmployeeId Field.  </summary>
        public int? DirectorEmployeeId { get; set; }       

        /// <summary>Gets or Sets ProducerEmployeeId Field.  </summary>
        public int? ProducerEmployeeId { get; set; }       

        /// <summary>Gets or Sets DirOfPhotoEmployeeId Field.  </summary>
        public int? DirOfPhotoEmployeeId { get; set; }       

        /// <summary>Gets or Sets SetDesignerEmployeeId Field.  </summary>
        public int? SetDesignerEmployeeId { get; set; }       

        /// <summary>Gets or Sets EditorEmployeeId Field.  </summary>
        public int? EditorEmployeeId { get; set; }       

        /// <summary>Gets or Sets ArtDirectorEmployeeId Field.  </summary>
        public int? ArtDirectorEmployeeId { get; set; }       

        /// <summary>Gets or Sets SalesRepId Field.  </summary>
        public int? SalesRepId { get; set; }        

        /// <summary>Gets or Sets AgencyId Field.  </summary>
        public int? AgencyId { get; set; }        

        /// <summary>Gets or Sets AgencyClientCustomerId Field.  </summary>
        public int? AgencyClientCustomerId { get; set; }        

        /// <summary>Gets or Sets ThirdPartyCustomerId Field.  </summary>
        public int? ThirdPartyCustomerId { get; set; }        

        /// <summary>Gets or Sets AgencyProducer Field.  </summary>

        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string AgencyProducer { get; set; }

        /// <summary>Gets or Sets AgencyProducerContactInfo Field.  </summary>
        [StringLength(JobCommercialUnit.MaxInfoLength)]
        public string AgencyProducerContactInfo { get; set; }

        /// <summary>Gets or Sets AgencyArtDirector Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string AgencyArtDirector { get; set; }

        /// <summary>Gets or Sets AgencyArtDirContactInfo Field.  </summary>
        [StringLength(JobCommercialUnit.MaxInfoLength)]
        public string AgencyArtDirContactInfo { get; set; }

        /// <summary>Gets or Sets AgencyWriter Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string AgencyWriter { get; set; }

        /// <summary>Gets or Sets AgencyWriterContactInfo Field.  </summary>
        [StringLength(JobCommercialUnit.MaxInfoLength)]
        public string AgencyWriterContactInfo { get; set; }

        /// <summary>Gets or Sets AgencyBusinessManager Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string AgencyBusinessManager { get; set; }

        /// <summary>Gets or Sets AgencyBusMgrContactInfo Field.  </summary>
        [StringLength(JobCommercialUnit.MaxInfoLength)]
        public string AgencyBusMgrContactInfo { get; set; }

        /// <summary>Gets or Sets AgencyJobNumber Field.  </summary>
        [StringLength(JobCommercialUnit.MaxJobnumberLength)]
        public string AgencyJobNumber { get; set; }

        /// <summary>Gets or Sets AgencyPONumber Field.  </summary>
        [StringLength(JobCommercialUnit.MaxJobnumberLength)]
        public string AgencyPONumber { get; set; }

        /// <summary>Gets or Sets AgencyName Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string AgencyName { get; set; }

        /// <summary>Gets or Sets AgencyAddress Field.  </summary>
        [StringLength(JobCommercialUnit.MaxProductLength)]
        public string AgencyAddress { get; set; }

        /// <summary>Gets or Sets AgencyPhone Field.  </summary>
        [StringLength(JobCommercialUnit.MaxJobnumberLength)]
        public string AgencyPhone { get; set; }

        /// <summary>Gets or Sets CommercialTitle1 Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string CommercialTitle1 { get; set; }

        /// <summary>Gets or Sets CommercialTitle2 Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string CommercialTitle2 { get; set; }

        /// <summary>Gets or Sets CommercialTitle3 Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string CommercialTitle3 { get; set; }

        /// <summary>Gets or Sets CommercialTitle4 Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string CommercialTitle4 { get; set; }

        /// <summary>Gets or Sets CommercialTitle5 Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string CommercialTitle5 { get; set; }

        /// <summary>Gets or Sets CommercialTitle6 Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string CommercialTitle6 { get; set; }

        /// <summary>Gets or Sets ProjectTotal Field.  </summary>
        public decimal? ProjectTotal { get; set; }

        /// <summary>Gets or Sets CGITotal Field.  </summary>
        public decimal? CGITotal { get; set; }

        /// <summary>Gets or Sets MarkupPercent Field.  </summary>
        public decimal? MarkupPercent { get; set; }

        /// <summary>Gets or Sets MarkupTotal Field.  </summary>
        public decimal? MarkupTotal { get; set; }

        /// <summary>Gets or Sets RDARevenue Field.  </summary>
        public decimal? RDARevenue { get; set; }

        /// <summary>Gets or Sets IncomeAccrual Field.  </summary>
        public decimal? IncomeAccrual { get; set; }

        /// <summary>Gets or Sets CostAccrual Field.  </summary>
        public decimal? CostAccrual { get; set; }

        /// <summary>Gets or Sets PostProductionCompany Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string PostProductionCompany { get; set; }

        /// <summary>Gets or Sets DubbingHouse Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string DubbingHouse { get; set; }

        /// <summary>Gets or Sets StorageHouse Field.  </summary>
        [StringLength(JobCommercialUnit.MaxTitleLength)]
        public string StorageHouse { get; set; }

        /// <summary>Gets or Sets IsBudgetLocked Field.  </summary>
        public bool IsBudgetLocked { get; set; }

        /// <summary>Gets or Sets CommercialNumber Field.  </summary>
        [StringLength(JobCommercialUnit.MaxJobnumberLength)]
        public string CommercialNumber { get; set; }

        /// <summary>Gets or Sets CommercialLength Field.  </summary>
        [StringLength(JobCommercialUnit.MaxJobnumberLength)]
        public string CommercialLength { get; set; }

        /// <summary>Gets or Sets PreProductionDays Field.  </summary>
        public int? PreProductionDays { get; set; }

        /// <summary>Gets or Sets StrikeDays Field.  </summary>
        public int? StrikeDays { get; set; }

        /// <summary>Gets or Sets PreLightDays Field.  </summary>
        public int? PreLightDays { get; set; }

        /// <summary>Gets or Sets PreLightHours Field.  </summary>
        public int? PreLightHours { get; set; }

        /// <summary>Gets or Sets StrikeHours Field.  </summary>
        public int? StrikeHours { get; set; }

        /// <summary>Gets or Sets StudioShootDays Field.  </summary>
        public int? StudioShootDays { get; set; }

        /// <summary>Gets or Sets ShootHours Field.  </summary>
        public int? ShootHours { get; set; }

        /// <summary>Gets or Sets LocationDays Field.  </summary>
        public int? LocationDays { get; set; }

        /// <summary>Gets or Sets LocationHours Field.  </summary>
        public int? LocationHours { get; set; }

        /// <summary>Gets or Sets IsCostPlus Field.  </summary>
        public bool IsCostPlus { get; set; }

        /// <summary>Gets or Sets IsWrapUpInsurance Field.  </summary>
        public bool IsWrapUpInsurance { get; set; }

        /// <summary>Gets or Sets IsFringeAccountSeparate Field.  </summary>
        public bool IsFringeAccountSeparate { get; set; }

        /// <summary>Gets or Sets IsOTon Field.  </summary>
        public bool IsOTon { get; set; }

        /// <summary>Gets or Sets AgencyEmail Field.  </summary>
        [StringLength(JobCommercialUnit.MaxEmailLength)]
        public string AgencyEmail { get; set; }

        /// <summary>Gets or Sets Company Field.  </summary>
        public long? OrganizationUnitId { get; set; }
        /// <summary>Gets or Sets the JobLocations for Job </summary>
        public List<CreateJobLocationInput> JobLocations { get; set; }
    }
}

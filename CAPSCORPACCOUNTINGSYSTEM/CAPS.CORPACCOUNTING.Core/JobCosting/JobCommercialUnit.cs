using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAPS.CORPACCOUNTING.Masters;


namespace CAPS.CORPACCOUNTING.JobCosting
{
    //// <summary>
    ///  Lajit JobCommercial Table Renamed as  CAPS_JobDetail
    /// </summary>
    [Table("CAPS_JobCommercial")]
    public class JobCommercialUnit : JobUnit
    {
        public const int MaxProductLength = 500;
        public const int MaxJobnumberLength = 50;
        public const int MaxTitleLength = 200;
        public const int MaxInfoLength = 500;
        public const int MaxEmailLength = 500;


        #region Class Property Declarations

        /// <summary>Gets or Sets BidDate Field.  </summary>
        public virtual DateTime? BidDate { get; set; }

        /// <summary>Gets or Sets AwardDate Field.  </summary>
        public virtual DateTime? AwardDate { get; set; }

        /// <summary>Gets or Sets ShootingDate Field.  </summary>
        public virtual DateTime? ShootingDate { get; set; }

        /// <summary>Gets or Sets WrapDate Field.  </summary>
        public virtual DateTime? WrapDate { get; set; }

        /// <summary>Gets or Sets RoughCutDate Field.  </summary>
        public virtual DateTime? RoughCutDate { get; set; }

        /// <summary>Gets or Sets AirDate Field.  </summary>
        public virtual DateTime? AirDate { get; set; }

        /// <summary>Gets or Sets DateClosed Field.  </summary>
        public virtual DateTime? DateClosed { get; set; }

        /// <summary>Gets or Sets FinalShootDate Field.  </summary>
        public virtual DateTime? FinalShootDate { get; set; }

        /// <summary>Gets or Sets ProductOwner Field.  </summary>
        [StringLength(MaxProductLength)]
        public virtual string ProductOwner { get; set; }

        /// <summary>Gets or Sets ProductName Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string ProductName { get; set; }

        /// <summary>Gets or Sets ExecutiveProducerId Field.  </summary>
        public virtual int? ExecutiveProducerId { get; set; }

        [ForeignKey("ExecutiveProducerId")]
        public virtual EmployeeUnit Employee { get; set; }

        /// <summary>Gets or Sets DirectorEmployeeId Field.  </summary>
        public virtual int? DirectorEmployeeId { get; set; }

        [ForeignKey("DirectorEmployeeId")]
        public virtual EmployeeUnit DirectorEmployee { get; set; }

        /// <summary>Gets or Sets ProducerEmployeeId Field.  </summary>
        public virtual int? ProducerEmployeeId { get; set; }

        [ForeignKey("ProducerEmployeeId")]
        public virtual EmployeeUnit ProducerEmployee { get; set; }

        /// <summary>Gets or Sets DirOfPhotoEmployeeId Field.  </summary>
        public virtual int? DirOfPhotoEmployeeId { get; set; }

        [ForeignKey("DirOfPhotoEmployeeId")]
        public virtual EmployeeUnit DirOfPhotoEmployee { get; set; }

        /// <summary>Gets or Sets SetDesignerEmployeeId Field.  </summary>
        public virtual int? SetDesignerEmployeeId { get; set; }

        [ForeignKey("SetDesignerEmployeeId")]
        public virtual EmployeeUnit SetDesignerEmployee { get; set; }

        /// <summary>Gets or Sets EditorEmployeeId Field.  </summary>
        public virtual int? EditorEmployeeId { get; set; }

        [ForeignKey("EditorEmployeeId")]
        public virtual EmployeeUnit EditorEmployee { get; set; }

        /// <summary>Gets or Sets ArtDirectorEmployeeId Field.  </summary>
        public virtual int? ArtDirectorEmployeeId { get; set; }

        [ForeignKey("ArtDirectorEmployeeId")]
        public virtual EmployeeUnit ArtDirectorEmployee { get; set; }

        /// <summary>Gets or Sets SalesRepId Field.  </summary>
        public virtual int? SalesRepId { get; set; }

        [ForeignKey("SalesRepId")]
        public virtual SalesRepUnit SalesRep { get; set; }

        /// <summary>Gets or Sets AgencyId Field.  </summary>
        public virtual int? AgencyId { get; set; }

        [ForeignKey("AgencyId")]
        public virtual CustomerUnit Agency { get; set; }

        /// <summary>Gets or Sets AgencyClientCustomerId Field.  </summary>
        public virtual int? AgencyClientCustomerId { get; set; }

        [ForeignKey("AgencyClientCustomerId")]
        public virtual CustomerUnit AgencyClientCustomer { get; set; }

        /// <summary>Gets or Sets ThirdPartyCustomerId Field.  </summary>
        public virtual int? ThirdPartyCustomerId { get; set; }


        [ForeignKey("ThirdPartyCustomerId")]
        public virtual CustomerUnit ThirdPartyCustomer { get; set; }

        /// <summary>Gets or Sets AgencyProducer Field.  </summary>

        [StringLength(MaxTitleLength)]
        public virtual string AgencyProducer { get; set; }

        /// <summary>Gets or Sets AgencyProducerContactInfo Field.  </summary>
        [StringLength(MaxInfoLength)]
        public virtual string AgencyProducerContactInfo { get; set; }


        /// <summary>Gets or Sets AgencyArtDirector Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string AgencyArtDirector { get; set; }

        /// <summary>Gets or Sets AgencyArtDirContactInfo Field.  </summary>
        [StringLength(MaxInfoLength)]
        public virtual string AgencyArtDirContactInfo { get; set; }

        /// <summary>Gets or Sets AgencyWriter Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string AgencyWriter { get; set; }

        /// <summary>Gets or Sets AgencyWriterContactInfo Field.  </summary>
        [StringLength(MaxInfoLength)]
        public virtual string AgencyWriterContactInfo { get; set; }

        /// <summary>Gets or Sets AgencyBusinessManager Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string AgencyBusinessManager { get; set; }

        /// <summary>Gets or Sets AgencyBusMgrContactInfo Field.  </summary>
        [StringLength(MaxInfoLength)]
        public virtual string AgencyBusMgrContactInfo { get; set; }

        /// <summary>Gets or Sets AgencyJobNumber Field.  </summary>
        [StringLength(MaxJobnumberLength)]
        public virtual string AgencyJobNumber { get; set; }

        /// <summary>Gets or Sets AgencyPONumber Field.  </summary>
        [StringLength(MaxJobnumberLength)]
        public virtual string AgencyPONumber { get; set; }

        /// <summary>Gets or Sets AgencyName Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string AgencyName { get; set; }

        /// <summary>Gets or Sets AgencyAddress Field.  </summary>
        [StringLength(MaxProductLength)]
        public virtual string AgencyAddress { get; set; }

        /// <summary>Gets or Sets AgencyPhone Field.  </summary>
        [StringLength(MaxJobnumberLength)]
        public virtual string AgencyPhone { get; set; }

        /// <summary>Gets or Sets CommercialTitle1 Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string CommercialTitle1 { get; set; }

        /// <summary>Gets or Sets CommercialTitle2 Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string CommercialTitle2 { get; set; }

        /// <summary>Gets or Sets CommercialTitle3 Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string CommercialTitle3 { get; set; }

        /// <summary>Gets or Sets CommercialTitle4 Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string CommercialTitle4 { get; set; }

        /// <summary>Gets or Sets CommercialTitle5 Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string CommercialTitle5 { get; set; }

        /// <summary>Gets or Sets CommercialTitle6 Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string CommercialTitle6 { get; set; }

        /// <summary>Gets or Sets ProjectTotal Field.  </summary>
        public virtual decimal? ProjectTotal { get; set; }

        /// <summary>Gets or Sets CGITotal Field.  </summary>
        public virtual decimal? CGITotal { get; set; }

        /// <summary>Gets or Sets MarkupPercent Field.  </summary>
        public virtual decimal? MarkupPercent { get; set; }

        /// <summary>Gets or Sets MarkupTotal Field.  </summary>
        public virtual decimal? MarkupTotal { get; set; }

        /// <summary>Gets or Sets RDARevenue Field.  </summary>
        public virtual decimal? RDARevenue { get; set; }

        /// <summary>Gets or Sets IncomeAccrual Field.  </summary>
        public virtual decimal? IncomeAccrual { get; set; }

        /// <summary>Gets or Sets CostAccrual Field.  </summary>
        public virtual decimal? CostAccrual { get; set; }

        /// <summary>Gets or Sets PostProductionCompany Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string PostProductionCompany { get; set; }

        /// <summary>Gets or Sets DubbingHouse Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string DubbingHouse { get; set; }

        /// <summary>Gets or Sets StorageHouse Field.  </summary>
        [StringLength(MaxTitleLength)]
        public virtual string StorageHouse { get; set; }

        /// <summary>Gets or Sets IsBudgetLocked Field.  </summary>
        public virtual bool IsBudgetLocked { get; set; }

        /// <summary>Gets or Sets CommercialNumber Field.  </summary>
        [StringLength(MaxJobnumberLength)]
        public virtual string CommercialNumber { get; set; }

        /// <summary>Gets or Sets CommercialLength Field.  </summary>
        [StringLength(MaxJobnumberLength)]
        public virtual string CommercialLength { get; set; }

        /// <summary>Gets or Sets PreProductionDays Field.  </summary>
        public virtual int? PreProductionDays { get; set; }

        /// <summary>Gets or Sets StrikeDays Field.  </summary>
        public virtual int? StrikeDays { get; set; }

        /// <summary>Gets or Sets PreLightDays Field.  </summary>
        public virtual int? PreLightDays { get; set; }

        /// <summary>Gets or Sets PreLightHours Field.  </summary>
        public virtual int? PreLightHours { get; set; }

        /// <summary>Gets or Sets StrikeHours Field.  </summary>
        public virtual int? StrikeHours { get; set; }

        /// <summary>Gets or Sets StudioShootDays Field.  </summary>
        public virtual int? StudioShootDays { get; set; }

        /// <summary>Gets or Sets ShootHours Field.  </summary>
        public virtual int? ShootHours { get; set; }

        /// <summary>Gets or Sets LocationDays Field.  </summary>
        public virtual int? LocationDays { get; set; }

        /// <summary>Gets or Sets LocationHours Field.  </summary>
        public virtual int? LocationHours { get; set; }

        /// <summary>Gets or Sets IsCostPlus Field.  </summary>
        public virtual bool IsCostPlus { get; set; }

        /// <summary>Gets or Sets IsWrapUpInsurance Field.  </summary>
        public virtual bool IsWrapUpInsurance { get; set; }

        /// <summary>Gets or Sets IsFringeAccountSeparate Field.  </summary>
        public virtual bool IsFringeAccountSeparate { get; set; }

        /// <summary>Gets or Sets IsOTon Field.  </summary>
        public virtual bool IsOTon { get; set; }

        /// <summary>Gets or Sets AgencyEmail Field.  </summary>
        [StringLength(MaxEmailLength)]
        public virtual string AgencyEmail { get; set; }

        /// <summary>Gets or Sets ContractExecutionDate Field.  </summary>
        public virtual DateTime? ContractExecutionDate { get; set; }

        /// <summary>Gets or Sets DeliveryDate Field.  </summary>
        public virtual DateTime? DeliveryDate { get; set; }

        /// <summary>Gets or Sets LocationNames Field.  </summary>
        public virtual string LocationNames { get; set; }



        #endregion

        public JobCommercialUnit()
        {
        }

        public JobCommercialUnit( DateTime? biddate, DateTime? awarddate, DateTime? shootingdate,
            DateTime? wrapdate, DateTime? roughcutdate, DateTime? airdate, DateTime? dateclosed,
            DateTime? finalshootdate, string productowner, string productname, int? executiveproducerid,
            int? directoremployeeid,
            int? produceremployeeid, int? dirofphotoemployeeid, int? setdesigneremployeeid, int? editoremployeeid,
            int? artdirectoremployeeid, int? salesrepid, int? agencyid, int? agencyclientcustomerid,
            int? thirdpartycustomerid, string agencyproducer, string agencyproducercontactinfo, string agencyartdirector,
            string agencyartdircontactinfo,
            string agencywriter, string agencywritercontactinfo, string agencybusinessmanager,
            string agencybusmgrcontactinfo,
            string agencyjobnumber, string agencyponumber, string agencyname, string agencyaddress, string agencyphone,
            string commercialtitle1, string commercialtitle2, string commercialtitle3, string commercialtitle4,
            string commercialtitle5, string commercialtitle6,
            decimal? projecttotal, decimal? cgitotal, decimal? markuppercent, decimal? markuptotal,
            decimal? rdarevenue, decimal? incomeaccrual, decimal? costaccrual, string postproductioncompany,
            string dubbinghouse, string storagehouse, bool isbudgetlocked, string commercialnumber,
            string commerciallength, int? preproductiondays, int? strikedays, int? prelightdays, int? prelighthours,
            int? strikehours, int? studioshootdays, int? shoothours, int? locationdays, int? locationhours,
            bool iscostplus, bool iswrapupinsurance, bool isfringeaccountseparate, 
            string agencyemail, bool isoton, DateTime? contractexecutiondate, DateTime? deliverydate, string jobnumber,
            string caption, bool iscorporatedefault, int chartofaccountid,
            long? rollupaccountid, int? typeofcurrencyid, int? rollupjobid, ProjectStatus? typeofjobstatusid,
            BudgetSoftware? typeofbidsoftwareid, int? rollupcenterid, bool isapproved, bool isactive, bool isictdivision,
            long organizationunitid, TypeofProject? typeofprojectid, int? taxrecoveryid, int? taxcreditid, string locationnames)
            : base(jobnumber: jobnumber, caption: caption, iscorporatedefault: iscorporatedefault,
                rollupaccountid: rollupaccountid,
                typeofcurrencyid: typeofcurrencyid, rollupjobid: rollupjobid, typeofjobstatusid: typeofjobstatusid, typeofbidsoftwareid: typeofbidsoftwareid,
                isapproved: isapproved, isactive: isactive, isictdivision: isictdivision, organizationunitid: organizationunitid, typeofprojectid: typeofprojectid,
                taxrecoveryid: taxrecoveryid, chartofaccountid: chartofaccountid, rollupcenterid: rollupcenterid, isdivision: false, taxcreditid: taxcreditid)
        {
            //JobId = jobid;
            BidDate = biddate;
            AwardDate = awarddate;
            ShootingDate = shootingdate;
            WrapDate = wrapdate;
            RoughCutDate = roughcutdate;
            AirDate = airdate;
            DateClosed = dateclosed;
            FinalShootDate = finalshootdate;
            ProductOwner = productowner;
            ProductName = productname;
            ExecutiveProducerId = executiveproducerid;
            DirectorEmployeeId = directoremployeeid;
            ProducerEmployeeId = produceremployeeid;
            DirOfPhotoEmployeeId = dirofphotoemployeeid;
            SetDesignerEmployeeId = setdesigneremployeeid;
            EditorEmployeeId = editoremployeeid;
            ArtDirectorEmployeeId = artdirectoremployeeid;
            SalesRepId = salesrepid;
            AgencyId = agencyid;
            AgencyClientCustomerId = agencyclientcustomerid;
            ThirdPartyCustomerId = thirdpartycustomerid;
            AgencyProducer = agencyproducer;
            AgencyProducerContactInfo = agencyproducercontactinfo;
            AgencyArtDirector = agencyartdirector;
            AgencyArtDirContactInfo = agencyartdircontactinfo;
            AgencyWriter = agencywriter;
            AgencyWriterContactInfo = agencywritercontactinfo;
            AgencyBusinessManager = agencybusinessmanager;
            AgencyBusMgrContactInfo = agencybusmgrcontactinfo;
            AgencyJobNumber = agencyjobnumber;
            AgencyPONumber = agencyponumber;
            AgencyName = agencyname;
            AgencyAddress = agencyaddress;
            AgencyPhone = agencyphone;
            CommercialTitle1 = commercialtitle1;
            CommercialTitle2 = commercialtitle2;
            CommercialTitle3 = commercialtitle3;
            CommercialTitle4 = commercialtitle4;
            CommercialTitle5 = commercialtitle5;
            CommercialTitle6 = commercialtitle6;
            ProjectTotal = projecttotal;
            CGITotal = cgitotal;
            MarkupPercent = markuppercent;
            RDARevenue = rdarevenue;
            IncomeAccrual = incomeaccrual;
            CostAccrual = costaccrual;
            PostProductionCompany = postproductioncompany;
            DubbingHouse = dubbinghouse;
            StorageHouse = storagehouse;
            IsBudgetLocked = isbudgetlocked;
            CommercialNumber = commercialnumber;
            CommercialLength = commerciallength;
            PreProductionDays = preproductiondays;
            StrikeHours = strikehours;
            StrikeDays = strikedays;
            PreLightDays = prelightdays;
            PreLightHours = prelighthours;
            StudioShootDays = studioshootdays;
            ShootHours = shoothours;
            LocationDays = locationdays;
            LocationHours = locationhours;
            IsCostPlus = iscostplus;
            IsWrapUpInsurance = iswrapupinsurance;
            IsFringeAccountSeparate = isfringeaccountseparate;
            //OrganizationUnitId = organizationunitid;
            AgencyEmail = agencyemail;
            IsOTon = isoton;
            ContractExecutionDate = contractexecutiondate;
            DeliveryDate = deliverydate;
            LocationNames = locationnames;
        }

       
    }
}

                                                                                                                                                                                                                               
                                                                                                         
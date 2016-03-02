using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Data.Entity;
using Abp.Authorization;
using System.Linq;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobCommercialAppService : CORPACCOUNTINGServiceBase, IJobCommercialAppService
    {
        private readonly JobCommercialUnitManager _jobDetailUnitManager;
        private readonly IRepository<JobCommercialUnit> _jobDetailUnitRepository;
        private readonly IJobLocationAppService _jobLocationAppService;
        private readonly IRepository<JobLocationUnit> _jobLocationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JobCommercialAppService(JobCommercialUnitManager jobDetailUnitManager, IRepository<JobCommercialUnit> jobDetailUnitRepository, IUnitOfWorkManager unitOfWorkManager,
            IJobLocationAppService jobLocationAppService, IRepository<JobLocationUnit> jobLocationRepository)
        {
            _jobDetailUnitManager = jobDetailUnitManager;
            _jobDetailUnitRepository = jobDetailUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _jobLocationAppService = jobLocationAppService;
            _jobLocationRepository = jobLocationRepository;

        }
        /// <summary>
        /// Creating JobDetails
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<JobCommercialUnitDto> CreateJobDetailUnit(CreateJobCommercialInput input)
        {

            var jobDetailUnit = new JobCommercialUnit(jobid: input.JobId, biddate: input.BidDate, awarddate: input.AwardDate, shootingdate: input.ShootingDate,
                wrapdate: input.WrapDate, roughcutdate: input.RoughCutDate, airdate: input.AirDate, dateclosed: input.DateClosed,
                finalshootdate: input.FinalShootDate, productowner: input.ProductOwner, productname: input.ProductName,
                executiveproducerid: input.ExecutiveProducerId, directoremployeeid: input.DirectorEmployeeId, produceremployeeid: input.ProducerEmployeeId,
                dirofphotoemployeeid: input.DirOfPhotoEmployeeId, setdesigneremployeeid: input.SetDesignerEmployeeId,
                artdirectoremployeeid: input.ArtDirectorEmployeeId, salesrepid: input.SalesRepId, agencyid: input.AgencyId, agencyclientcustomerid: input.AgencyClientCustomerId,
                thirdpartycustomerid: input.ThirdPartyCustomerId, agencyproducer: input.AgencyProducer, agencyproducercontactinfo: input.AgencyProducerContactInfo,
                agencyartdirector: input.AgencyArtDirector, agencyartdircontactinfo: input.AgencyArtDirContactInfo, agencywriter: input.AgencyWriter,
                agencywritercontactinfo: input.AgencyWriterContactInfo, agencybusinessmanager: input.AgencyBusinessManager,
                agencybusmgrcontactinfo: input.AgencyBusMgrContactInfo, agencyjobnumber: input.AgencyJobNumber, agencyponumber: input.AgencyPONumber,
                agencyname: input.AgencyName, agencyaddress: input.AgencyAddress, agencyphone: input.AgencyPhone, commercialtitle1: input.CommercialTitle1,
                commercialtitle2: input.CommercialTitle2, commercialtitle3: input.CommercialTitle3, commercialtitle4: input.CommercialTitle4,
                commercialtitle5: input.CommercialTitle5, commercialtitle6: input.CommercialTitle6, commerciallength: input.CommercialLength,
                commercialnumber: input.CommercialNumber, prelightdays: input.PreLightDays, prelighthours: input.PreLightHours, preproductiondays: input.PreProductionDays,
                strikedays: input.StrikeDays, strikehours: input.StrikeHours, projecttotal: input.ProjectTotal, cgitotal: input.CGITotal,
                isbudgetlocked: input.IsBudgetLocked, iscostplus: input.IsCostPlus, isfringeaccountseparate: input.IsFringeAccountSeparate,
                isoton: input.IsOTon, iswrapupinsurance: input.IsWrapUpInsurance, agencyemail: input.AgencyEmail, organizationunitid: input.OrganizationUnitId,
                postproductioncompany: input.PostProductionCompany, costaccrual: input.CostAccrual, dubbinghouse: input.DubbingHouse,
                editoremployeeid: input.EditorEmployeeId, incomeaccrual: input.IncomeAccrual, locationdays: input.LocationDays, locationhours: input.LocationHours,
                markuppercent: input.MarkupPercent, markuptotal: input.MarkupTotal, rdarevenue: input.RDARevenue, shoothours: input.ShootHours,
                studioshootdays: input.StudioShootDays, storagehouse: input.StorageHouse);

            await _jobDetailUnitManager.CreateAsync(jobDetailUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            // Create Job Locations
            input.JobLocations.ForEach(a => { a.JobId = jobDetailUnit.JobId; a.JobDetailId = jobDetailUnit.Id;});
            foreach (var location in input.JobLocations)
            {
                await _jobLocationAppService.CreateJobLocationUnit(location);
            }
            return jobDetailUnit.MapTo<JobCommercialUnitDto>();
        }

        /// <summary>
        /// Delete the Jobdetails
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteJobDetailUnit(IdInput input)
        {
            await _jobLocationAppService.DeleteJobLocationUnit(input);
            await _jobDetailUnitManager.DeleteAsync(input);
        }

        /// <summary>
        /// Update Job Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<JobCommercialUnitDto> UpdateJobDetailUnit(UpdateJobCommercialnput input)
        {
            var jobDetailUnit = await _jobDetailUnitRepository.GetAsync(input.JobCommercialId);

            #region Setting the values to be updated

            jobDetailUnit.JobId = input.JobId;
            jobDetailUnit.BidDate = input.BidDate;
            jobDetailUnit.AwardDate = input.AwardDate;
            jobDetailUnit.ShootingDate = input.ShootingDate;
            jobDetailUnit.WrapDate = input.WrapDate;
            jobDetailUnit.RoughCutDate = input.RoughCutDate;
            jobDetailUnit.AirDate = input.AirDate;
            jobDetailUnit.DateClosed = input.DateClosed;
            jobDetailUnit.FinalShootDate = input.FinalShootDate;
            jobDetailUnit.ProductName = input.ProductName;
            jobDetailUnit.ProductOwner = input.ProductOwner;
            jobDetailUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobDetailUnit.ExecutiveProducerId = input.ExecutiveProducerId;
            jobDetailUnit.DirectorEmployeeId = input.DirectorEmployeeId;
            jobDetailUnit.ProducerEmployeeId = input.ProducerEmployeeId;
            jobDetailUnit.DirOfPhotoEmployeeId = input.DirOfPhotoEmployeeId;
            jobDetailUnit.ArtDirectorEmployeeId = input.ArtDirectorEmployeeId;
            jobDetailUnit.SalesRepId = input.SalesRepId;
            jobDetailUnit.AgencyId = input.AgencyId;
            jobDetailUnit.ThirdPartyCustomerId = input.ThirdPartyCustomerId;
            jobDetailUnit.AgencyClientCustomerId = input.AgencyClientCustomerId;
            jobDetailUnit.AgencyProducer = input.AgencyProducer;
            jobDetailUnit.AgencyProducerContactInfo = input.AgencyProducerContactInfo;
            jobDetailUnit.AgencyArtDirector = input.AgencyArtDirector;
            jobDetailUnit.AgencyArtDirContactInfo = input.AgencyArtDirContactInfo;
            jobDetailUnit.AgencyWriter = input.AgencyWriter;
            jobDetailUnit.AgencyWriterContactInfo = input.AgencyWriterContactInfo;
            jobDetailUnit.AgencyBusinessManager = input.AgencyBusMgrContactInfo;
            jobDetailUnit.AgencyBusMgrContactInfo = input.AgencyBusMgrContactInfo;
            jobDetailUnit.AgencyJobNumber = input.AgencyJobNumber;
            jobDetailUnit.AgencyPONumber = input.AgencyPONumber;
            jobDetailUnit.AgencyPhone = input.AgencyPhone;
            jobDetailUnit.AgencyAddress = input.AgencyAddress;
            jobDetailUnit.AgencyName = input.AgencyName;
            jobDetailUnit.CommercialTitle1 = input.CommercialTitle1;
            jobDetailUnit.CommercialTitle2 = input.CommercialTitle2;
            jobDetailUnit.CommercialTitle3 = input.CommercialTitle3;
            jobDetailUnit.CommercialTitle4 = input.CommercialTitle4;
            jobDetailUnit.CommercialTitle5 = input.CommercialTitle5;
            jobDetailUnit.CommercialTitle6 = input.CommercialTitle6;
            jobDetailUnit.CommercialNumber = input.CommercialNumber;
            jobDetailUnit.CommercialLength = input.CommercialLength;
            jobDetailUnit.PreLightHours = input.PreLightHours;
            jobDetailUnit.PreLightDays = input.PreLightDays;
            jobDetailUnit.PreProductionDays = input.PreProductionDays;
            jobDetailUnit.StrikeDays = input.StrikeDays;
            jobDetailUnit.StrikeHours = input.StrikeHours;
            jobDetailUnit.ProjectTotal = input.ProjectTotal;
            jobDetailUnit.CGITotal = input.CGITotal;
            jobDetailUnit.IsBudgetLocked = input.IsBudgetLocked;
            jobDetailUnit.IsCostPlus = input.IsCostPlus;
            jobDetailUnit.IsFringeAccountSeparate = input.IsFringeAccountSeparate;
            jobDetailUnit.IsOTon = input.IsOTon;
            jobDetailUnit.IsWrapUpInsurance = input.IsWrapUpInsurance;
            jobDetailUnit.AgencyEmail = input.AgencyEmail;
            jobDetailUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobDetailUnit.PostProductionCompany = input.PostProductionCompany;
            jobDetailUnit.CostAccrual = input.CostAccrual;
            jobDetailUnit.DubbingHouse = input.DubbingHouse;
            jobDetailUnit.EditorEmployeeId = input.EditorEmployeeId;
            jobDetailUnit.IncomeAccrual = input.IncomeAccrual;
            jobDetailUnit.LocationDays = input.LocationDays;
            jobDetailUnit.LocationHours = input.LocationHours;
            jobDetailUnit.MarkupPercent = input.MarkupPercent;
            jobDetailUnit.RDARevenue = input.RDARevenue;
            jobDetailUnit.ShootHours = input.ShootHours;
            jobDetailUnit.StudioShootDays = input.StudioShootDays;
            jobDetailUnit.StorageHouse = input.StorageHouse;
            jobDetailUnit.MarkupTotal = input.MarkupTotal;

            #endregion

            await _jobDetailUnitManager.UpdateAsync(jobDetailUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.JobLocations != null)
            {
                foreach (var location in input.JobLocations)
                {
                    if (location.JobLocationId != 0)
                        await _jobLocationAppService.UpdateJobLocationUnit(location);
                    else
                    {
                        AutoMapper.Mapper.CreateMap<UpdateJobLocationInput, CreateJobLocationInput>();
                        await _jobLocationAppService.CreateJobLocationUnit(AutoMapper.Mapper.Map<UpdateJobLocationInput, CreateJobLocationInput>(location));
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Job is Added*/
            };

            return jobDetailUnit.MapTo<JobCommercialUnitDto>();

        }

        /// <summary>
        /// Get JobDeatils By JobId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JobCommercialUnitDto> GetJobDetailsByJobId(IdInput input)
        {
            var jobitems = _jobDetailUnitRepository.GetAll().Include(q => q.JobLocations).Where(job => job.JobId == input.Id).ToList();

            var result = jobitems.FirstOrDefault().MapTo<JobCommercialUnitDto>();
            result.JobCommercialId = jobitems.FirstOrDefault().Id;
            return result;
        }
    }
}

using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Authorization;
using AutoMapper;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobCommercialAppService : CORPACCOUNTINGServiceBase, IJobCommercialAppService
    {
        private readonly JobCommercialUnitManager _jobDetailUnitManager;
        private readonly IRepository<JobCommercialUnit> _jobDetailUnitRepository;
        private readonly IJobLocationAppService _jobLocationAppService;
        private readonly IJobPORangeAllocationUnitAppService _poRangeAllocationAppService;

        public JobCommercialAppService(JobCommercialUnitManager jobDetailUnitManager, IRepository<JobCommercialUnit> jobDetailUnitRepository,
            IJobLocationAppService jobLocationAppService,IJobPORangeAllocationUnitAppService poRangeAllocationAppService)
        {
            _jobDetailUnitManager = jobDetailUnitManager;
            _jobDetailUnitRepository = jobDetailUnitRepository;
            _jobLocationAppService = jobLocationAppService;
            _poRangeAllocationAppService = poRangeAllocationAppService;

        }
        /// <summary>
        /// Creating JobDetails
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<IdOutputDto<int>> CreateJobDetailUnit(CreateJobCommercialInput input)
        {
            var jobDetailUnit = input.MapTo<JobCommercialUnit>();

            IdOutputDto<int> response = new IdOutputDto<int>()
            {
                JobId = await _jobDetailUnitManager.CreateAsync(jobDetailUnit)
            };

            await CurrentUnitOfWork.SaveChangesAsync();
            return response;

        }

        /// <summary>
        /// Update Job Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<JobCommercialUnitDto> UpdateJobDetailUnit(UpdateJobCommercialnput input)
        {
            string locationNames = string.Empty;
            if (!ReferenceEquals(input.JobLocations, null))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var joblocation in input.JobLocations)
                {
                    sb.Append(joblocation.LocationName + ",");
                }
                locationNames = sb.ToString().TrimEnd(',');
            }
            var jobDetailUnit = await _jobDetailUnitRepository.GetAsync(input.JobId);

            #region Setting the values to be updated

            jobDetailUnit.Id = input.JobId;
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
            jobDetailUnit.LocationNames = locationNames;
            #endregion

            await _jobDetailUnitManager.UpdateAsync(jobDetailUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            if (!ReferenceEquals(input.JobLocations, null))
            {
                foreach (var location in input.JobLocations)
                {
                    if (location.JobLocationId != 0)
                    {
                        await _jobLocationAppService.UpdateJobLocationUnit(location);
                    }
                    else
                    {
                       // AutoMapper.Mapper.CreateMap<UpdateJobLocationInput, CreateJobLocationInput>();
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<UpdateJobLocationInput, CreateJobLocationInput>();
                        });
                        await _jobLocationAppService.CreateJobLocationUnit(AutoMapper.Mapper.Map<UpdateJobLocationInput, CreateJobLocationInput>(location));
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }

            //PO Ranges
            if (!ReferenceEquals(input.POAllocations, null))
            {
                foreach (var poallocations in input.POAllocations)
                {
                    if (poallocations.PORangeAllocationId != 0)
                    {
                        await _poRangeAllocationAppService.UpdateJobPORangeAllocationUnit(poallocations);
                    }
                    else
                    {
                       // AutoMapper.Mapper.CreateMap<UpdateJobPORangeAllocationInput, CreateJobPORangeAllocationInput>();
                        //var config = new MapperConfiguration(cfg => {
                        //    cfg.CreateMap<UpdateJobPORangeAllocationInput, CreateJobPORangeAllocationInput>();
                        //});
                        await _poRangeAllocationAppService.CreateJobPORangeAllocationUnit(AutoMapper.Mapper.Map<UpdateJobPORangeAllocationInput, CreateJobPORangeAllocationInput>(poallocations));
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            return jobDetailUnit.MapTo<JobCommercialUnitDto>();

        }

    }
}

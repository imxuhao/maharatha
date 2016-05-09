using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Helpers;
using Abp.Collections.Extensions;
using System.Data.Entity;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using System;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobLocationAppService : CORPACCOUNTINGServiceBase, IJobLocationAppService
    {
        private readonly JobLocationUnitManager _jobLocationUnitManager;
        private readonly IRepository<JobLocationUnit> _jobLocationUnitRepository;
        private readonly IRepository<LocationSetUnit> _locationSetUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JobLocationAppService(JobLocationUnitManager jobLocationUnitManager, IRepository<JobLocationUnit> jobLocationUnitRepository, 
            IUnitOfWorkManager unitOfWorkManager, IRepository<LocationSetUnit> locationSetUnitRepository)
        {
            _jobLocationUnitManager = jobLocationUnitManager;
            _jobLocationUnitRepository = jobLocationUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _locationSetUnitRepository = locationSetUnitRepository;           
        }

        [UnitOfWork]
        public async Task<JobLocationUnitDto> CreateJobLocationUnit(CreateJobLocationInput input)
        {
            var jobLocationUnit = new JobLocationUnit(jobid: input.JobId, locationsitedate: input.LocationSiteDate,
                locationid: input.LocationId);
            await _jobLocationUnitManager.CreateAsync(jobLocationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return jobLocationUnit.MapTo<JobLocationUnitDto>();
        }

        public async Task<JobLocationUnitDto> UpdateJobLocationUnit(UpdateJobLocationInput input)
        {

            var jobLocationUnit = await _jobLocationUnitRepository.GetAsync(input.JobLocationId);

            #region Setting the values to be updated
            jobLocationUnit.LocationSiteDate = input.LocationSiteDate;
            jobLocationUnit.LocationId = input.LocationId;            
            #endregion
            await _jobLocationUnitManager.UpdateAsync(jobLocationUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return jobLocationUnit.MapTo<JobLocationUnitDto>();
        }

        public async Task<ListResultOutput<JobLocationUnitDto>> GetJobLocationUnitsByJobId(IdInput input)
        {
            var items = await (from joblocation in _jobLocationUnitRepository.GetAll()
                join location in _locationSetUnitRepository.GetAll() on joblocation.LocationId equals location.Id
                select new {JobLocation = joblocation, LocationName = location.Description}).ToListAsync();

            return new ListResultOutput<JobLocationUnitDto>(
                items.Select(item=>
                {
                    var dto = item.JobLocation.MapTo<JobLocationUnitDto>();
                    dto.JobLocationId = item.JobLocation.Id;
                    dto.LocationName = item.LocationName;
                    return dto;
                }).ToList());
        }

        public async Task DeleteJobLocationUnit(IdInput input)
        {
            await _jobLocationUnitManager.DeleteAsync(input);
        }
        
        public async Task<PagedResultOutput<JobLocationUnitDto>> GetJobLocationUnits(SearchInputDto input)
        {
            var query = from job in _jobLocationUnitRepository.GetAll()
                        join location in _locationSetUnitRepository.GetAll() on job.LocationId equals location.Id

                        select new { JobLocation = job, LocationName = location.Description };
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                query = Helper.CreateFilters(query, mapSearchFilters);
            }
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("LocationName ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<JobLocationUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.JobLocation.MapTo<JobLocationUnitDto>();
                dto.JobLocationId = item.JobLocation.Id;
                dto.LocationName = item.LocationName;
                return dto;

            }).ToList());
        }
    }
}

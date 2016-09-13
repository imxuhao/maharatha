using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Helpers;
using System.Data.Entity;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.PettyCash;
using CAPS.CORPACCOUNTING.PurchaseOrders;
using CAPS.CORPACCOUNTING.Sessions;
using CAPS.CORPACCOUNTING.Organization;
using CAPS.CORPACCOUNTING.Authorization.Users;
using Abp.Authorization.Users;
namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    /// 
    /// </summary>
   public class InvoiceBuilderAppService : CORPACCOUNTINGServiceBase, IInvoiceBuilderAppService
    {
        private readonly IRepository<JobUnit> _jobUnitRepository;
        public InvoiceBuilderAppService( IRepository<JobUnit> jobUnitRepository)
        {
            _jobUnitRepository = jobUnitRepository;

        }
        public async Task<List<NameValueDto>> GetProjectList()
        {
          //var projectList=await _jobUnitRepository.GetAll().Where(u=>u.)

            return null;
        }


    }
}

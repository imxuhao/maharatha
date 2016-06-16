using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Payables.Dto;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Masters;
using System.Linq.Dynamic;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Banking;
using Abp.Linq.Extensions;
using AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.PettyCash.Dto;

namespace CAPS.CORPACCOUNTING.PettyCash
{
    /// <summary>
    /// PettyCash AppService
    /// </summary>
    [AbpAuthorize] //This is to ensure only logged in user has access to this module.
    public class PettyCashEntryDocumentAppService : CORPACCOUNTINGServiceBase, IPettyCashEntryDocumentAppService
    {
        private readonly PettyCashEntryDocumnetManager _pettyCashEntryDocumnetManager;
        private readonly IRepository<PettyCashEntryDocumentUnit, long> _pcEntryDocumnetUnitRepository;
        private readonly IRepository<PettyCashEntryDocumentDetailUnit, long> _pcEntryDocumnetDetailUnitRepository;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IRepository<BatchUnit> _batchUnitRepository;
        private readonly PettyCashEntryDocumnetDetailManager _pcEntryDocumnetDetailManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;

        public PettyCashEntryDocumentAppService(PettyCashEntryDocumnetManager pettyCashEntryDocumnetManager, IRepository<PettyCashEntryDocumentUnit, long> pcEntryDocumnetUnitRepository,
            IRepository<PettyCashEntryDocumentDetailUnit, long> pcEntryDocumnetDetailUnitRepository, IRepository<VendorUnit> vendorUnitRepository,
            IRepository<BatchUnit> batchUnitRepository, PettyCashEntryDocumnetDetailManager pcEntryDocumnetDetailManager, IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository, IRepository<SubAccountUnit, long> subAccountUnitRepository, IRepository<TaxCreditUnit> taxCreditUnitRepository)
        {
            _pettyCashEntryDocumnetManager = pettyCashEntryDocumnetManager;
            _pcEntryDocumnetUnitRepository = pcEntryDocumnetUnitRepository;
            _pcEntryDocumnetDetailUnitRepository = pcEntryDocumnetDetailUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _batchUnitRepository = batchUnitRepository;
            _pcEntryDocumnetDetailManager = pcEntryDocumnetDetailManager;
            _jobUnitRepository = jobUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _subAccountUnitRepository = subAccountUnitRepository;
            _taxCreditUnitRepository = taxCreditUnitRepository;
        }

        /// <summary>
        /// Create APHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Payables_Invoices_Create)]
        public async Task<IdOutputDto<long>> CreatePettyCashEntryDocumentUnit(PettyCashEntryDocumentInput input)
        {
            var pcEntryDocumnetUnit = input.MapTo<PettyCashEntryDocumentUnit>();
            IdOutputDto<long> response = new IdOutputDto<long>
            {
                Id = await _pettyCashEntryDocumnetManager.CreateAsync(pcEntryDocumnetUnit)
            };

            //Null Checking of InvoiceEntryDocumentDetailList
            if (!ReferenceEquals(input.PettyCashEntryDocumentDetailList, null))
            {
                //Bulk Insertion of InvoiceEntryDocumentDetails
                foreach (var pcEntryDocumentDetail in input.PettyCashEntryDocumentDetailList)
                {
                    pcEntryDocumentDetail.AccountingDocumentId = response.Id;
                    var pcEntryDocumentDetailUnit = pcEntryDocumentDetail.MapTo<PettyCashEntryDocumentDetailUnit>();
                    await _pcEntryDocumnetDetailManager.CreateAsync(pcEntryDocumentDetailUnit);
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return response;
        }

        /// <summary>
        /// Update ApHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Payables_Invoices_Edit)]
        public async Task UpdatePettyCashEntryDocumentUnit(PettyCashEntryDocumentInput input)
        {

            var pcEntryDocumnetunit =await _pcEntryDocumnetUnitRepository.GetAsync(input.AccountingDocumentId);
            Mapper.Map(input, pcEntryDocumnetunit);
            await _pettyCashEntryDocumnetManager.UpdateAsync(pcEntryDocumnetunit);

            if (!ReferenceEquals(input.PettyCashEntryDocumentDetailList, null))
            {
                //Bulk CRUD operations of InvoiceEntryDocumentDetails
                foreach (var pcEntryDocumnetDetail in input.PettyCashEntryDocumentDetailList)
                {
                    if (pcEntryDocumnetDetail.AccountingItemId == 0)
                    {
                        pcEntryDocumnetDetail.AccountingDocumentId = input.AccountingDocumentId;
                        var pcEntryDocumentDetailUnit =
                            pcEntryDocumnetDetail.MapTo<PettyCashEntryDocumentDetailUnit>();
                        await _pcEntryDocumnetDetailManager.CreateAsync(pcEntryDocumentDetailUnit);
                    }
                    else if (pcEntryDocumnetDetail.AccountingItemId > 0)
                    {
                        var pcEntryDocumentDetailUnit = await _pcEntryDocumnetDetailUnitRepository.GetAsync(
                                    pcEntryDocumnetDetail.AccountingItemId);
                        Mapper.Map(pcEntryDocumnetDetail, pcEntryDocumentDetailUnit);
                        await _pcEntryDocumnetDetailManager.UpdateAsync(pcEntryDocumentDetailUnit);
                    }
                    else
                    {
                        IdInput<long> idInput = new IdInput<long>()
                        {
                            Id = (pcEntryDocumnetDetail.AccountingItemId*(-1))
                        };
                        await _pcEntryDocumnetDetailManager.DeleteAsync(idInput);
                    }
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete ApHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Payables_Invoices_Delete)]
        public async Task DeletePettyCashEntryDocumentUnit(IdInput<long> input)
        {
            await _pcEntryDocumnetDetailUnitRepository.DeleteAsync(p => p.AccountingDocumentId == input.Id);
            await _pettyCashEntryDocumnetManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        ///  Get all APHeaderTransactions with Paging and Sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Payables_Invoices)]
        public async Task<PagedResultOutput<PettyCashEntryDocumentUnitDto>> GetPettyCashEntryDocumentUnits(SearchInputDto input)
        {
            var query = from pcUnits in _pcEntryDocumnetUnitRepository.GetAll()
                              //join user in _userUnitRepository.GetAll() on journals.CreatorUserId equals user.Id
                            //  into users
                        join batch in _batchUnitRepository.GetAll() on pcUnits.BatchId equals batch.Id
                           into batchunit
                        from batchunits in batchunit.DefaultIfEmpty()
                        //join vendor in _vendorUnitRepository.GetAll() on pcUnits. equals vendor.Id
                        //    into vendorunit
                        //from vendors in vendorunit.DefaultIfEmpty()
                        select new { pcUnits = pcUnits, BatchName = batchunits.Description};

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = query.CreateFilters(mapSearchFilters);
            }
            query = query.Where(p => p.pcUnits.OrganizationUnitId == input.OrganizationUnitId);


            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("pcUnits.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<PettyCashEntryDocumentUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.pcUnits.MapTo<PettyCashEntryDocumentUnitDto>();
                dto.BatchName = item.BatchName;
                dto.AccountingDocumentId = item.pcUnits.Id;
                return dto;
            }).ToList());

        }
        /// <summary>
        /// Get APHeaderDetailsByAccountingDocumentId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payables_Invoices)]
        public async Task<PagedResultOutput<PettyCashEntryDocumentDetailDto>> GetPettyCashEntryDocumentDetailsByAccountingDocumentId(GetTransactionList input)
        {
            var query = from invoices in _pcEntryDocumnetDetailUnitRepository.GetAll()
                        join job in _jobUnitRepository.GetAll() on invoices.JobId equals job.Id
                        into jobunit
                        from jobs in jobunit.DefaultIfEmpty()
                        join line in _accountUnitRepository.GetAll() on invoices.AccountId equals line.Id
                        into account
                        from lines in account.DefaultIfEmpty()
                        join subAccount1 in _subAccountUnitRepository.GetAll() on invoices.SubAccountId1 equals subAccount1.Id
                        into subAccountunit1
                        from subAccounts1 in subAccountunit1.DefaultIfEmpty()
                        join subAccount2 in _subAccountUnitRepository.GetAll() on invoices.SubAccountId2 equals subAccount2.Id
                        into subAccountunit2
                        from subAccounts2 in subAccountunit2.DefaultIfEmpty()
                        join subAccount3 in _subAccountUnitRepository.GetAll() on invoices.SubAccountId3 equals subAccount3.Id
                        into subAccountunit3
                        from subAccounts3 in subAccountunit2.DefaultIfEmpty()
                        join taxCredit in _taxCreditUnitRepository.GetAll() on invoices.TaxRebateId equals taxCredit.Id
                        into taxCreditunit
                        from taxCredits in taxCreditunit.DefaultIfEmpty()
                        select new
                        {
                            InvoiceDetails = invoices,
                            JobDesc = jobs.JobNumber,
                            accountDesc = lines.AccountNumber,
                            subAccount1 = subAccounts1.Description,
                            subAccount2 = subAccounts2.Description,
                            subAccount3 = subAccounts3.Description,
                            taxCredit = taxCredits.Number
                        };

            query = query.Where(p => p.InvoiceDetails.AccountingDocumentId.Value == input.AccountingDocumentId);

            var results = await query.AsNoTracking().ToListAsync();
            return new PagedResultOutput<PettyCashEntryDocumentDetailDto>(results.Count, results.Select(item =>
            {
                var dto = item.InvoiceDetails.MapTo<PettyCashEntryDocumentDetailDto>();
                dto.AccountNumber = item.accountDesc;
                dto.SubAccountNumber1 = item.subAccount1;
                dto.SubAccountNumber2 = item.subAccount2;
                dto.SubAccountNumber3 = item.subAccount3;
                dto.TaxRebateNumber = item.taxCredit;
                dto.AccountingDocumentId = item.InvoiceDetails.Id;
                return dto;
            }).ToList());
        }
       
    }
}

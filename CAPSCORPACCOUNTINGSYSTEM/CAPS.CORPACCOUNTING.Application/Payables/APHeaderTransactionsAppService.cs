﻿using System;
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

namespace CAPS.CORPACCOUNTING.Payables
{
    [AbpAuthorize] //This is to ensure only logged in user has access to this module.
    public class APHeaderTransactionsAppService : CORPACCOUNTINGServiceBase, IAPHeaderTransactionsAppService
    {
        private readonly APHeaderTransactionsUnitManager _apHeaderTransactionsUnitManager;
        private readonly IRepository<ApHeaderTransactions, long> _apHeaderTransactionsUnitRepository;
        private readonly IRepository<InvoiceEntryDocumentDetailUnit, long> _invoiceEntryDocumentDetailUnitRepository;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IRepository<BatchUnit> _batchUnitRepository;
        private readonly InvoiceEntryDocumentDetailUnitManager _invoiceEntryDocumentDetailUnitManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;

        public APHeaderTransactionsAppService(APHeaderTransactionsUnitManager apHeaderTransactionsUnitManager,
            IRepository<ApHeaderTransactions, long> apHeaderTransactionsUnitRepository, IRepository<VendorUnit> vendorUnitRepository,
            IRepository<BatchUnit> batchUnitRepository, IRepository<InvoiceEntryDocumentDetailUnit, long> invoiceEntryDocumentDetailUnitRepository,
            InvoiceEntryDocumentDetailUnitManager invoiceEntryDocumentDetailUnitManager, IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository, IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<TaxCreditUnit> taxCreditUnitRepository)
        {
            _apHeaderTransactionsUnitManager = apHeaderTransactionsUnitManager;
            _apHeaderTransactionsUnitRepository = apHeaderTransactionsUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _batchUnitRepository = batchUnitRepository;
            _invoiceEntryDocumentDetailUnitRepository = invoiceEntryDocumentDetailUnitRepository;
            _invoiceEntryDocumentDetailUnitManager = invoiceEntryDocumentDetailUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _jobUnitRepository = jobUnitRepository;
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
        public async Task<IdOutputDto<long>> CreateAPHeaderTransactionUnit(APHeaderTransactionsInputUnit input)
        {
            var apHeaderTransactions = input.MapTo<ApHeaderTransactions>();
            IdOutputDto<long> response = new IdOutputDto<long>
            {
                Id = await _apHeaderTransactionsUnitManager.CreateAsync(apHeaderTransactions)
            };

            //Null Checking of InvoiceEntryDocumentDetailList
            if (!ReferenceEquals(input.InvoiceEntryDocumentDetailList, null))
            {
                //Bulk Insertion of InvoiceEntryDocumentDetails
                foreach (var invoiceEntryDocumentDetail in input.InvoiceEntryDocumentDetailList)
                {
                    invoiceEntryDocumentDetail.AccountingDocumentId = response.Id;
                    var invoiceEntryDocumentDetailUnit =invoiceEntryDocumentDetail.MapTo<InvoiceEntryDocumentDetailUnit>();
                    await _invoiceEntryDocumentDetailUnitManager.CreateAsync(invoiceEntryDocumentDetailUnit);
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
        public async Task UpdateAPHeaderTransactionUnit(APHeaderTransactionsInputUnit input)
        {

            var aPHeaderTransactionsInput = await _apHeaderTransactionsUnitRepository.GetAsync(input.AccountingDocumentId);
            Mapper.Map(input, aPHeaderTransactionsInput);
            await _apHeaderTransactionsUnitManager.UpdateAsync(aPHeaderTransactionsInput);

            if (!ReferenceEquals(input.InvoiceEntryDocumentDetailList, null))
            {
                //Bulk CRUD operations of InvoiceEntryDocumentDetails
                foreach (var invoiceEntryDocumentDetail in input.InvoiceEntryDocumentDetailList)
                {
                    if (invoiceEntryDocumentDetail.AccountingItemId == 0)
                    {
                        invoiceEntryDocumentDetail.AccountingDocumentId = input.AccountingDocumentId;
                        var invoiceEntryDocumentDetailUnit =
                            invoiceEntryDocumentDetail.MapTo<InvoiceEntryDocumentDetailUnit>();
                        await _invoiceEntryDocumentDetailUnitManager.CreateAsync(invoiceEntryDocumentDetailUnit);
                    }
                    else if (invoiceEntryDocumentDetail.AccountingItemId > 0)
                    {
                        var invoiceEntryDocumentDetailUnit =await _invoiceEntryDocumentDetailUnitRepository.GetAsync(
                                    invoiceEntryDocumentDetail.AccountingItemId);
                        Mapper.Map(invoiceEntryDocumentDetail, invoiceEntryDocumentDetailUnit);
                        await _invoiceEntryDocumentDetailUnitManager.UpdateAsync(invoiceEntryDocumentDetailUnit);
                    }
                    else
                    {
                        IdInput<long> idInput = new IdInput<long>()
                        {
                            Id = (invoiceEntryDocumentDetail.AccountingItemId*(-1))
                        };
                        await _invoiceEntryDocumentDetailUnitManager.DeleteAsync(idInput);
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
        public async Task DeleteAPHeaderTransactionUnit(IdInput<long> input)
        {
            await _invoiceEntryDocumentDetailUnitRepository.DeleteAsync(p => p.AccountingDocumentId == input.Id);
            await _apHeaderTransactionsUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        ///  Get all APHeaderTransactions with Paging and Sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Payables_Invoices)]
        public async Task<PagedResultOutput<APHeaderTransactionsUnitDto>> GetAPHeaderTransactionUnits(SearchInputDto input)
        {
            var query = from invoices in _apHeaderTransactionsUnitRepository.GetAll()
                            //  join user in _userUnitRepository.GetAll() on journals.CreatorUserId equals user.Id
                            //  into users
                        join batch in _batchUnitRepository.GetAll() on invoices.BatchId equals batch.Id
                           into batchunit
                        from batchunits in batchunit.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on invoices.VendorId equals vendor.Id
                            into vendorunit
                        from vendors in vendorunit.DefaultIfEmpty()
                        select new { Invoices = invoices, BatchName = batchunits.Description, VendorName = vendors.LastName };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = query.CreateFilters(mapSearchFilters);
            }
            query = query.Where(p => p.Invoices.OrganizationUnitId == input.OrganizationUnitId);


            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Invoices.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<APHeaderTransactionsUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Invoices.MapTo<APHeaderTransactionsUnitDto>();
                dto.BatchName = item.BatchName;
                dto.VendorName = item.VendorName;
                dto.AccountingDocumentId = item.Invoices.Id;
                return dto;
            }).ToList());

        }
        /// <summary>
        /// Get APHeaderDetailsByAccountingDocumentId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payables_Invoices)]
        public async Task<PagedResultOutput<InvoiceEntryDocumentDetailUnitDto>> GetAPHeaderTransactionDetailUnitsByAccountingDocumentId(GetTransactionList input)
        {
            var query = from invoices in _invoiceEntryDocumentDetailUnitRepository.GetAll()
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
            return new PagedResultOutput<InvoiceEntryDocumentDetailUnitDto>(results.Count, results.Select(item =>
            {
                var dto = item.InvoiceDetails.MapTo<InvoiceEntryDocumentDetailUnitDto>();
                dto.AccountDesc = item.accountDesc;
                dto.SubAccount1Desc = item.subAccount1;
                dto.SubAccount2Desc = item.subAccount2;
                dto.SubAccount3Desc = item.subAccount3;
                dto.TaxRebateDesc = item.taxCredit;
                dto.AccountingDocumentId = item.InvoiceDetails.Id;
                return dto;
            }).ToList());
        }
    }
}

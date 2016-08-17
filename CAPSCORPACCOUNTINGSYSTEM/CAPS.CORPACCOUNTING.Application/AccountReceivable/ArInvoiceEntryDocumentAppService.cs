using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Masters.Dto;
using AutoMapper;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.AccountReceivable.Dto;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Journals;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{

    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Receivables_Invoices_Entry)]
    public class ArInvoiceEntryDocumentAppService : CORPACCOUNTINGServiceBase, IArInvoiceEntryDocumentAppService
    {

        private readonly ArInvoiceEntryDocumentUnitManager _arInvoiceEntryDocumentUnitManager;
        private readonly ArInvoiceEntryDocumentDetailUnitManager _arInvoiceEntryDocumentDetailUnitManager;
        private readonly IRepository<ArInvoiceEntryDocumentUnit, long> _arInvoiceEntryDocumentUnitRepository;
        private readonly IRepository<ArInvoiceEntryDocumentDetailUnit, long> _arInvoiceEntryDocumentDetailUnitRepository;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<CustomerUnit, int> _customerUnitRepository;
        private readonly IRepository<TypeOfCurrencyUnit, short> _currencyUnitRepository;
        private readonly IRepository<CustomerPaymentTermUnit> _customerPaymentTermUnitRepository;
        private readonly IRepository<SalesRepUnit> _salesRepUnitRepository;
        private readonly IRepository<LocationSetUnit> _locationSetUnitRepository;
        private readonly IRepository<ARBillingTypeUnit> _aRBillingTypeUnitRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arInvoiceEntryDocumentUnitManager"></param>
        /// <param name="arInvoiceEntryDocumentUnitRepository"></param>
        /// <param name="arInvoiceEntryDocumentDetailUnitRepository"></param>
        /// <param name="arInvoiceEntryDocumentDetailUnitManager"></param>
        /// <param name="jobUnitRepository"></param>
        /// <param name="accountUnitRepository"></param>
        /// <param name="subAccountUnitRepository"></param>
        /// <param name="customerUnitRepository"></param>
        /// <param name="currencyUnitRepository"></param>
        /// <param name="customerPaymentTermRepository"></param>
        /// <param name="salesRepUnitTermRepository"></param>
        /// <param name="locationSetUnitRepository"></param>
        /// <param name="aRBillingTypeUnitRepository"></param>
        public ArInvoiceEntryDocumentAppService(
            ArInvoiceEntryDocumentUnitManager arInvoiceEntryDocumentUnitManager,
            IRepository<ArInvoiceEntryDocumentUnit, long> arInvoiceEntryDocumentUnitRepository,
            IRepository<ArInvoiceEntryDocumentDetailUnit, long> arInvoiceEntryDocumentDetailUnitRepository,
            ArInvoiceEntryDocumentDetailUnitManager arInvoiceEntryDocumentDetailUnitManager,
            IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<CustomerUnit, int> customerUnitRepository,
            IRepository<TypeOfCurrencyUnit, short> currencyUnitRepository,
            IRepository<CustomerPaymentTermUnit> customerPaymentTermRepository,
            IRepository<SalesRepUnit> salesRepUnitTermRepository,
            IRepository<LocationSetUnit> locationSetUnitRepository,
            IRepository<ARBillingTypeUnit> aRBillingTypeUnitRepository)
        {
            _arInvoiceEntryDocumentUnitManager = arInvoiceEntryDocumentUnitManager;
            _arInvoiceEntryDocumentUnitRepository = arInvoiceEntryDocumentUnitRepository;
            _arInvoiceEntryDocumentDetailUnitRepository = arInvoiceEntryDocumentDetailUnitRepository;
            _arInvoiceEntryDocumentDetailUnitManager = arInvoiceEntryDocumentDetailUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _subAccountUnitRepository = subAccountUnitRepository;
            _customerUnitRepository = customerUnitRepository;
            _currencyUnitRepository = currencyUnitRepository;
            _customerPaymentTermUnitRepository = customerPaymentTermRepository;
            _salesRepUnitRepository = salesRepUnitTermRepository;
            _locationSetUnitRepository = locationSetUnitRepository;
            _aRBillingTypeUnitRepository = aRBillingTypeUnitRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Receivables_Invoices_Entry_Create)]
        public async Task<IdOutputDto<long>> CreateArInvoiceEntryDocumentUnit(ArInvoiceEntryDocumentInputUnit input)
        {
            var arInvoiceTransactions = input.MapTo<ArInvoiceEntryDocumentUnit>();
            IdOutputDto<long> response = new IdOutputDto<long>
            {
                Id = await _arInvoiceEntryDocumentUnitManager.CreateAsync(arInvoiceTransactions)
            };

            //Null Checking of InvoiceEntryDocumentDetailList
            if (!ReferenceEquals(input.InvoiceEntryDocumentDetailList, null))
            {
                //Bulk Insertion of InvoiceEntryDocumentDetails
                foreach (var invoiceEntryDocumentDetail in input.InvoiceEntryDocumentDetailList)
                {
                    invoiceEntryDocumentDetail.AccountingDocumentId = response.Id;
                    var invoiceEntryDocumentDetailUnit = invoiceEntryDocumentDetail.MapTo<ArInvoiceEntryDocumentDetailUnit>();
                    await _arInvoiceEntryDocumentDetailUnitManager.CreateAsync(invoiceEntryDocumentDetailUnit);
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return response;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Receivables_Invoices_Entry_Edit)]
        public async Task UpdateArInvoiceEntryDocumentUnit(ArInvoiceEntryDocumentInputUnit input)
        {

            var arInvoiceTransactionsInput = await _arInvoiceEntryDocumentUnitRepository.GetAsync(input.AccountingDocumentId);
            Mapper.Map(input, arInvoiceTransactionsInput);
            await _arInvoiceEntryDocumentUnitManager.UpdateAsync(arInvoiceTransactionsInput);

            if (!ReferenceEquals(input.InvoiceEntryDocumentDetailList, null))
            {
                //Bulk CRUD operations of InvoiceEntryDocumentDetails
                foreach (var invoiceEntryDocumentDetail in input.InvoiceEntryDocumentDetailList)
                {
                    if (invoiceEntryDocumentDetail.AccountingItemId == 0)
                    {
                        invoiceEntryDocumentDetail.AccountingDocumentId = input.AccountingDocumentId;
                        var invoiceEntryDocumentDetailUnit =
                            invoiceEntryDocumentDetail.MapTo<ArInvoiceEntryDocumentDetailUnit>();
                        await _arInvoiceEntryDocumentDetailUnitManager.CreateAsync(invoiceEntryDocumentDetailUnit);
                    }
                    else if (invoiceEntryDocumentDetail.AccountingItemId > 0)
                    {
                        var invoiceEntryDocumentDetailUnit = await _arInvoiceEntryDocumentDetailUnitRepository.GetAsync(
                                    invoiceEntryDocumentDetail.AccountingItemId);
                        Mapper.Map(invoiceEntryDocumentDetail, invoiceEntryDocumentDetailUnit);
                        await _arInvoiceEntryDocumentDetailUnitManager.UpdateAsync(invoiceEntryDocumentDetailUnit);
                    }
                    else
                    {
                        IdInput<long> idInput = new IdInput<long>()
                        {
                            Id = (invoiceEntryDocumentDetail.AccountingItemId * (-1))
                        };
                        await _arInvoiceEntryDocumentDetailUnitManager.DeleteAsync(idInput);
                    }
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Receivables_Invoices_Entry_Delete)]
        public async Task DeleteArInvoiceEntryDocumentUnit(IdInput input)
        {
            await _arInvoiceEntryDocumentDetailUnitRepository.DeleteAsync(p => p.AccountingDocumentId == input.Id);
            await _arInvoiceEntryDocumentUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteArInvoiceDetailUnit(IdInput<long> input)
        {
            await _arInvoiceEntryDocumentDetailUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<ArInvoiceEntryDocumentUnitDto>> GetArInvoiceEntryDocumentUnits(SearchInputDto input)
        {

            bool unPosted = false;
            var query = from invoices in _arInvoiceEntryDocumentUnitRepository.GetAll()
                        join customer in _customerUnitRepository.GetAll() on invoices.CustomerId equals customer.Id
                            into customers
                        from customer in customers.DefaultIfEmpty()
                        join currency in _currencyUnitRepository.GetAll() on invoices.TypeOfCurrencyId equals currency.Id into
                           currencys
                        from currency in currencys.DefaultIfEmpty()
                        join paymentTerm in _customerPaymentTermUnitRepository.GetAll() on invoices.ArPaymentTermId equals paymentTerm.Id into
                           paymentTerms
                        from paymentTerm in paymentTerms.DefaultIfEmpty()
                        join salesRep in _salesRepUnitRepository.GetAll() on invoices.SalesRepId equals salesRep.Id into
                         salesReps
                        from salesRep in salesReps.DefaultIfEmpty()

                        select new
                        {
                            invoices,
                            TypeOfCurrency = currency.Description,
                            SalesRepName = salesRep.LastName,
                            ArPaymentTerm = paymentTerm.Description,
                            CustomerName = customer.LastName
                        };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = query.CreateFilters(mapSearchFilters);
            }

            query = query.Where(u =>
                        u.invoices.TypeOfAccountingDocumentId == TypeOfAccountingDocument.AccountsReceivable &&
                        u.invoices.IsPosted == unPosted);



            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("invoices.DocumentReference DESC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            var arIdList = string.Join(",", results.Select(x => x.invoices.Id).ToArray());

            var arDetailsList = from arDetail in _arInvoiceEntryDocumentDetailUnitRepository.GetAll()
                                join account in _accountUnitRepository.GetAll() on arDetail.AccountId equals account.Id
                                join job in _jobUnitRepository.GetAll() on arDetail.JobId equals job.Id
                                select new
                                {
                                    AccountingDocumentId = arDetail.AccountingDocumentId,
                                    Account = account.AccountNumber,
                                    Job = job.JobNumber,
                                    JobName = job.Caption,
                                    Amount = arDetail.Amount
                                };

            var arDetails = await arDetailsList.Where(u => arIdList.Contains(u.AccountingDocumentId.ToString())).ToListAsync();

            var arDetailGroup = (arDetails.GroupBy(i => i.AccountingDocumentId)
                .Select(g => new
                {
                    g.Key,
                    Jobs = string.Join(",", g.Select(u => u.Job)),
                    JobName = string.Join(",", g.Select(u => u.JobName)),
                    Account = string.Join(",", g.Select(u => u.Account)),
                    Amount = g.Sum(u => u.Amount)
                })).ToList();

            var arList = from ar in results
                         join details in arDetailGroup on ar.invoices.Id equals details.Key
                         select new { arData = ar, arDetailGroup = details };

            return new PagedResultOutput<ArInvoiceEntryDocumentUnitDto>(resultCount, arList.Select(item =>
  {
      var dto = item.arData.invoices.MapTo<ArInvoiceEntryDocumentUnitDto>();
      dto.JobNumber = item.arDetailGroup.Jobs;
      dto.JobName = item.arDetailGroup.JobName;
      dto.AccountNumber = item.arDetailGroup.Account;
      dto.TypeOfCurrency = item.arData.TypeOfCurrency;
      dto.SalesRepName = item.arData.SalesRepName;
      dto.ArPaymentTerm = item.arData.ArPaymentTerm;
      dto.AccountingDocumentId = item.arData.invoices.Id;
      dto.CustomerName = item.arData.CustomerName;
      return dto;
  }).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<ArInvoiceEntryDetailUnitDto>> GetArInvoiceByAccountingDocumentId(GetTransactionList input)
        {
            var query = from invoices in _arInvoiceEntryDocumentDetailUnitRepository.GetAll()
                        join job in _jobUnitRepository.GetAll() on invoices.JobId equals job.Id
                        into jobunit
                        from jobs in jobunit.DefaultIfEmpty()
                        join line in _accountUnitRepository.GetAll() on invoices.AccountId equals line.Id
                        into account
                        from lines in account.DefaultIfEmpty()
                        join subAccount1 in _subAccountUnitRepository.GetAll() on invoices.SubAccountId1 equals subAccount1.Id
                            into subAccount1
                        from subAccounts1 in subAccount1.DefaultIfEmpty()
                        join subAccount4 in _subAccountUnitRepository.GetAll() on invoices.SubAccountId4 equals subAccount4.Id
                       into subAccountunit4
                        from subAccounts4 in subAccountunit4.DefaultIfEmpty()

                        select new
                        {
                            InvoiceDetails = invoices,
                            JobNumber = jobs.JobNumber,
                            accountNumber = lines.AccountNumber,
                            subAccount1 = subAccounts1.Description,
                            subAccount4 = subAccounts4.Description
                        };

            query = query.Where(p => p.InvoiceDetails.AccountingDocumentId.Value == input.AccountingDocumentId);

            var results = await query.AsNoTracking().ToListAsync();
            return new PagedResultOutput<ArInvoiceEntryDetailUnitDto>(results.Count, results.Select(item =>
            {
                var dto = item.InvoiceDetails.MapTo<ArInvoiceEntryDetailUnitDto>();
                dto.SubAccountNumber1 = item.subAccount1;
                dto.SubAccountNumber4 = item.subAccount4;
                dto.JobNumber = item.JobNumber;
                dto.AccountNumber = item.accountNumber;
                dto.AccountingDocumentId = item.InvoiceDetails.Id;
                return dto;
            }).ToList());
        }
        
        /// <summary>
        /// Get GetArBillingTypeList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetBillingTypeList(AutoSearchInput input)
        {
            var billingTypeList = await _aRBillingTypeUnitRepository.GetAll()
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return billingTypeList;
        }
        
        /// <summary>
        /// Get CustomerPaymentTermsList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetCustomerPaymentTermsList(AutoSearchInput input)
        {
            var paymentTermsList = await _customerPaymentTermUnitRepository.GetAll()
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return paymentTermsList;
        }
    }
}

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
                        join invoiceDetail in _arInvoiceEntryDocumentDetailUnitRepository.GetAll() on invoices.Id equals invoiceDetail.AccountingDocumentId
                            into invoiceDetails
                        from invoiceDetail in invoiceDetails.DefaultIfEmpty()
                        join job in _jobUnitRepository.GetAll() on invoiceDetail.JobId equals job.Id into jobs
                        from job in jobs.DefaultIfEmpty()
                        join account in _accountUnitRepository.GetAll() on invoiceDetail.AccountId equals account.Id into account
                        from accounts in account.DefaultIfEmpty()
                        select new
                        {
                            invoices,
                            JobNumber = job.JobNumber,
                            JobName = job.Caption,
                            TypeOfCurrency = currency.Description,
                            SalesRepName = salesRep.LastName,
                            ArPaymentTerm = paymentTerm.Description,
                        };

            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null),
                p => p.invoices.OrganizationUnitId == input.OrganizationUnitId)
                .Where(u =>
                        u.invoices.TypeOfAccountingDocumentId == TypeOfAccountingDocument.AccountsReceivable &&
                        u.invoices.IsPosted == unPosted);



            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("invoices.DocumentReference DESC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<ArInvoiceEntryDocumentUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.invoices.MapTo<ArInvoiceEntryDocumentUnitDto>();
                dto.JobNumber = item.JobNumber;
                dto.JobName = item.JobName;
                dto.TypeOfCurrency = item.TypeOfCurrency;
                dto.SalesRepName = item.SalesRepName;
                dto.ArPaymentTerm = item.ArPaymentTerm;
                dto.AccountingDocumentId = item.invoices.Id;
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
                            //join line in _accountUnitRepository.GetAll() on invoices.AccountId equals line.Id
                            //into account
                            //from lines in account.DefaultIfEmpty()
                        join subAccount1 in _subAccountUnitRepository.GetAll() on invoices.SubAccountId1 equals subAccount1.Id
                            into subAccount1
                        from subAccounts1 in subAccount1.DefaultIfEmpty()
                            //join subAccount2 in _locationSetUnitRepository.GetAll() on invoices. equals subAccount2.Id
                            //into subAccountunit2
                            //from subAccounts2 in subAccountunit2.DefaultIfEmpty()

                        select new
                        {
                            InvoiceDetails = invoices,
                            JobDesc = jobs.JobNumber,
                            //accountDesc = lines.AccountNumber,
                            subAccount1 = subAccounts1.Description,
                            //subAccount2 = subAccounts2.Description

                        };

            query = query.Where(p => p.InvoiceDetails.AccountingDocumentId.Value == input.AccountingDocumentId);

            var results = await query.AsNoTracking().ToListAsync();
            return new PagedResultOutput<ArInvoiceEntryDetailUnitDto>(results.Count, results.Select(item =>
            {
                var dto = item.InvoiceDetails.MapTo<ArInvoiceEntryDetailUnitDto>();
                dto.SubAccountNumber1 = item.subAccount1;
                //dto.SubAccountNumber2 = item.subAccount2;
                //dto.SubAccountNumber3 = item.subAccount3;

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
                 .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
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
            var paymenttermsList = await _customerPaymentTermUnitRepository.GetAll()
                 .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return paymenttermsList;
        }



        /// <summary>
        /// Get LocationsList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetLocationsList(AutoSearchInput input)
        {
            var locationList = await _locationSetUnitRepository.GetAll()
                 .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                 .Where(u=>u.TypeOfLocationSetId==LocationSets.Location)
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return locationList;
        }




    }
}

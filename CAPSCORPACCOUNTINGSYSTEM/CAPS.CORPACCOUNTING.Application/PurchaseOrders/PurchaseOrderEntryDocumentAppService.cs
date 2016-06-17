using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
using CAPS.CORPACCOUNTING.AccountReceivable;
using CAPS.CORPACCOUNTING.AccountReceivable.Dto;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Journals;
using CAPS.CORPACCOUNTING.PurchaseOrders;
using CAPS.CORPACCOUNTING.PurchaseOrders.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Common;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{

    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class PurchaseOrderEntryDocumentAppService : CORPACCOUNTINGServiceBase, IPurchaseOrderEntryDocumentAppService
    {

        private readonly PurchaseOrderEntryDocumentUnitManager _purchaseOrderEntryDocumentUnitManager;
        private readonly PurchaseOrderDetailUnitManager _purchaseOrderDetailUnitManager;
        private readonly IRepository<PurchaseOrderEntryDocumentUnit, long> _purchaseOrderEntryDocumentUnitRepository;
        private readonly IRepository<PurchaseOrderEntryDocumentDetailUnit, long> _purchaseOrderDetailUnitRepository;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<TypeOfCurrencyUnit, short> _currencyUnitRepository;
        private readonly IRepository<User, long> _userUnitRepository;
        private readonly IRepository<VendorUnit, int> _vendorUnitRepository;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;
        private readonly IRepository<BankAccountUnit, long> _bankAccountUnitRepository;
        private readonly IRepository<ApprovedSoxUnit, long> _approvedSoxUnitRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseOrderEntryDocumentUnitManager"></param>
        /// <param name="purchaseOrderEntryDocumentUnitRepository"></param>
        /// <param name="purchaseOrderDetailUnitRepository"></param>
        /// <param name="purchaseOrderDetailUnitManager"></param>
        /// <param name="jobUnitRepository"></param>
        /// <param name="accountUnitRepository"></param>
        /// <param name="subAccountUnitRepository"></param>
        /// <param name="customerUnitRepository"></param>
        /// <param name="currencyUnitRepository"></param>
        /// <param name="customerPaymentTermRepository"></param>
        /// <param name="salesRepUnitTermRepository"></param>
        /// <param name="locationSetUnitRepository"></param>
        /// <param name="userUnitRepository"></param>
        /// <param name="vendorUnitRepository"></param>
        /// <param name="taxCreditUnitRepository"></param>
        /// <param name="bankAccountUnitRepository"></param>
        public PurchaseOrderEntryDocumentAppService(
            PurchaseOrderEntryDocumentUnitManager purchaseOrderEntryDocumentUnitManager,
            IRepository<PurchaseOrderEntryDocumentUnit, long> purchaseOrderEntryDocumentUnitRepository,
            IRepository<PurchaseOrderEntryDocumentDetailUnit, long> purchaseOrderDetailUnitRepository,
            PurchaseOrderDetailUnitManager purchaseOrderDetailUnitManager,
            IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<TypeOfCurrencyUnit, short> currencyUnitRepository,
            IRepository<User, long> userUnitRepository,
            IRepository<VendorUnit, int> vendorUnitRepository,
            IRepository<TaxCreditUnit> taxCreditUnitRepository,
            IRepository<BankAccountUnit, long> bankAccountUnitRepository, IRepository<ApprovedSoxUnit, long> approvedSoxUnitRepository)
        {
            _purchaseOrderEntryDocumentUnitManager = purchaseOrderEntryDocumentUnitManager;
            _purchaseOrderEntryDocumentUnitRepository = purchaseOrderEntryDocumentUnitRepository;
            _purchaseOrderDetailUnitRepository = purchaseOrderDetailUnitRepository;
            _purchaseOrderDetailUnitManager = purchaseOrderDetailUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _subAccountUnitRepository = subAccountUnitRepository;
            _currencyUnitRepository = currencyUnitRepository;
            _userUnitRepository = userUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _taxCreditUnitRepository = taxCreditUnitRepository;
            _bankAccountUnitRepository = bankAccountUnitRepository;
            _approvedSoxUnitRepository = approvedSoxUnitRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_PurchaseOrders_Entry_Create)]
        public async Task<IdOutputDto<long>> CreatePurchaseOrderEntryDocumentUnit(PurchaseOrderEntryDocumentInputUnit input)
        {
            var purchaseOrderTransactions = input.MapTo<PurchaseOrderEntryDocumentUnit>();
            IdOutputDto<long> response = new IdOutputDto<long>
            {
                Id = await _purchaseOrderEntryDocumentUnitManager.CreateAsync(purchaseOrderTransactions)
            };

            //Null Checking of PurchaseOrderDetailList
            if (!ReferenceEquals(input.PurchaseOrderDetailList, null))
            {
                //Bulk Insertion of PurchaseOrderDetail
                foreach (var purchaseOrderDetail in input.PurchaseOrderDetailList)
                {
                    purchaseOrderDetail.AccountingDocumentId = response.Id;
                    var purchaseOrderEntryDocumentDetailUnit = purchaseOrderDetail.MapTo<PurchaseOrderEntryDocumentDetailUnit>();
                    await _purchaseOrderDetailUnitManager.CreateAsync(purchaseOrderEntryDocumentDetailUnit);
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

        [AbpAuthorize(AppPermissions.Pages_PurchaseOrders_Entry_Edit)]
        public async Task UpdatePurchaseOrderEntryDocumentUnit(PurchaseOrderEntryDocumentInputUnit input)
        {

            var purchaseOrderTransactionsInput = await _purchaseOrderEntryDocumentUnitRepository.GetAsync(input.AccountingDocumentId);
            Mapper.Map(input, purchaseOrderTransactionsInput);
            await _purchaseOrderEntryDocumentUnitManager.UpdateAsync(purchaseOrderTransactionsInput);

            if (!ReferenceEquals(input.PurchaseOrderDetailList, null))
            {
                //Bulk CRUD operations of PurchaseOrderDetail
                foreach (var purchaseOrderDetail in input.PurchaseOrderDetailList)
                {
                    if (purchaseOrderDetail.AccountingItemId == 0)
                    {
                        purchaseOrderDetail.AccountingDocumentId = input.AccountingDocumentId;
                        var purchaseOrderDetailUnit =
                            purchaseOrderDetail.MapTo<PurchaseOrderEntryDocumentDetailUnit>();
                        await _purchaseOrderDetailUnitManager.CreateAsync(purchaseOrderDetailUnit);
                    }
                    else if (purchaseOrderDetail.AccountingItemId > 0)
                    {
                        var purchaseOrderDetailUnit = await _purchaseOrderDetailUnitRepository.GetAsync(
                                    purchaseOrderDetail.AccountingItemId);
                        Mapper.Map(purchaseOrderDetail, purchaseOrderDetailUnit);
                        await _purchaseOrderDetailUnitManager.UpdateAsync(purchaseOrderDetailUnit);
                    }
                    else
                    {
                        IdInput<long> idInput = new IdInput<long>()
                        {
                            Id = (purchaseOrderDetail.AccountingItemId * (-1))
                        };
                        await _purchaseOrderDetailUnitManager.DeleteAsync(idInput);
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
        [AbpAuthorize(AppPermissions.Pages_PurchaseOrders_Entry_Delete)]
        public async Task DeletePurchaseOrderEntryDocumentUnit(IdInput input)
        {
            await _purchaseOrderDetailUnitRepository.DeleteAsync(p => p.AccountingDocumentId == input.Id);
            await _purchaseOrderEntryDocumentUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_PurchaseOrders_Entry_Delete)]
        public async Task DeletePurchaseOrderDetailUnit(IdInput<long> input)
        {
            await _purchaseOrderDetailUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<PurchaseOrderEntryDocumentUnitDto>> GetPurchaseOrderEntryDocumentUnits(SearchInputDto input)
        {

            bool unPosted = false;
            var query = from purchaseOrder in _purchaseOrderEntryDocumentUnitRepository.GetAll()
                join users in _userUnitRepository.GetAll() on purchaseOrder.CreatorUserId equals users.Id
                join vendor in _vendorUnitRepository.GetAll() on purchaseOrder.VendorId equals vendor.Id into vendor
                from vendors in vendor.DefaultIfEmpty()
                join approvedsox in
                    _approvedSoxUnitRepository.GetAll()
                        .Where(u => u.TypeOfObjectId == TypeofObject.PurchaseOrderEntryDocument) on purchaseOrder.Id
                    equals approvedsox.ObjectId into approvedsoxs
                from approvedsox in approvedsoxs.DefaultIfEmpty()
                join approvedUser in _vendorUnitRepository.GetAll() on approvedsox.ApprovedByUserId equals
                    approvedUser.Id into approvedUser
                from approvedUsers in approvedUser.DefaultIfEmpty()
                join purchaseOrderDetail in _purchaseOrderDetailUnitRepository.GetAll() on purchaseOrder.Id equals
                    purchaseOrderDetail.AccountingDocumentId into purchaseOrderDetails
                from purchaseOrderDetail in purchaseOrderDetails.DefaultIfEmpty()
                join job in _jobUnitRepository.GetAll() on purchaseOrderDetail.JobId equals job.Id into jobs
                from job in jobs.DefaultIfEmpty()
                join account in _accountUnitRepository.GetAll() on purchaseOrderDetail.AccountId equals account.Id into
                    account
                from accounts in account.DefaultIfEmpty()
                join currency in _currencyUnitRepository.GetAll() on purchaseOrder.TypeOfCurrencyId equals currency.Id
                    into currencys
                from currency in currencys.DefaultIfEmpty()
                select new
                {
                    purchaseOrder,
                    job.JobNumber,
                    VendorName = vendors.LastName,
                    accounts.AccountNumber,
                    CreatedUser = users.UserName,
                    TypeOfCurrency = currency.Description,
                    ApprovedBy = approvedUser
                };

            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null),
                p => p.purchaseOrder.OrganizationUnitId == input.OrganizationUnitId)
                .Where(u =>
                        u.purchaseOrder.TypeOfAccountingDocumentId == TypeOfAccountingDocument.PurchaseOrders &&
                        u.purchaseOrder.IsPosted == unPosted);



            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("purchaseOrder.DocumentReference DESC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<PurchaseOrderEntryDocumentUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.purchaseOrder.MapTo<PurchaseOrderEntryDocumentUnitDto>();
                dto.JobNumber = item.JobNumber;
                dto.TypeOfCurrency = item.TypeOfCurrency;
                dto.AccountNumber = item.AccountNumber;
                dto.AccountingDocumentId = item.purchaseOrder.Id;
                dto.VendorName = item.VendorName;
                dto.CreatedUser = item.CreatedUser;
                return dto;
            }).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<PurchaseOrderDetailUnitDto>> GetPurchaseOrdersByAccountingDocumentId(GetTransactionList input)
        {
            var query = from pODetail in _purchaseOrderDetailUnitRepository.GetAll()
                        join job in _jobUnitRepository.GetAll() on pODetail.JobId equals job.Id
                        into jobunit
                        from jobs in jobunit.DefaultIfEmpty()
                        join line in _accountUnitRepository.GetAll() on pODetail.AccountId equals line.Id
                        into account
                        from lines in account.DefaultIfEmpty()
                        join subAccount1 in _subAccountUnitRepository.GetAll() on pODetail.SubAccountId1 equals subAccount1.Id
                            into subAccount1
                        from subAccounts1 in subAccount1.DefaultIfEmpty()
                        join subAccount2 in _subAccountUnitRepository.GetAll() on pODetail.SubAccountId2 equals subAccount2.Id
                           into subAccount2
                        from subAccounts2 in subAccount2.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on pODetail.VendorId equals vendor.Id into vendor
                        from vendors in vendor.DefaultIfEmpty()
                        join taxCredit in _taxCreditUnitRepository.GetAll() on pODetail.TaxRebateId equals taxCredit.Id into
                            taxCredit
                        from taxCredits in taxCredit.DefaultIfEmpty()
                        select new
                        {
                            InvoiceDetails = pODetail,
                            jobs.JobNumber,
                            lines.AccountNumber,
                            subAccount1 = subAccounts1.Description,
                            taxCreditNumber = taxCredits.Number,
                            VendorName = vendors.LastName

                        };

            query = query.Where(p => p.InvoiceDetails.AccountingDocumentId.Value == input.AccountingDocumentId);

            var results = await query.AsNoTracking().ToListAsync();
            return new PagedResultOutput<PurchaseOrderDetailUnitDto>(results.Count, results.Select(item =>
            {
                var dto = item.InvoiceDetails.MapTo<PurchaseOrderDetailUnitDto>();
                dto.SubAccountNumber1 = item.subAccount1;
                dto.JobNumber = item.JobNumber;
                dto.AccountNumber = item.AccountNumber;
                dto.TaxRebateNumber = item.taxCreditNumber;
                dto.TypeOf1099T4 = item.InvoiceDetails.TypeOf1099T4Id != null ? item.InvoiceDetails.TypeOf1099T4Id.ToDisplayName() : "";
                dto.VendorName = item.VendorName;
                dto.AccountingItemId = item.InvoiceDetails.Id;
                return dto;
            }).ToList());
        }


        /// <summary>
        /// Get CardHolder Information
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<AutoFillDto>> GetCardInfoList(AutoSearchInput input)
        {
            var cardLength = 0;
            var cardHolderList = await _bankAccountUnitRepository.GetAll()
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null),
                    p => p.OrganizationUnitId == input.OrganizationUnitId)
                .WhereIf(!string.IsNullOrEmpty(input.Query),
                    p => p.Description.Contains(input.Query) || p.Description.Contains(input.Query))
                    .Where(p => p.IsClosed == false)
                .ToListAsync();

            var cardHolders = cardHolderList.
                Select(item =>
                {
                    cardLength = item.CCFullAccountNO.Length;
                    var dto = new AutoFillDto
                    {
                        Name = item.BankAccountName,
                        Value = item.Id.ToString(),
                        Column1 = item.TypeOfBankAccountId.ToDisplayName(),
                        Column2 = !string.IsNullOrEmpty(item.BankAccountNumber) ? item.BankAccountNumber.Substring(cardLength - 5, cardLength) : ""
                    };
                    return dto;
                }).ToList();
            return cardHolders;
        }
    }
}

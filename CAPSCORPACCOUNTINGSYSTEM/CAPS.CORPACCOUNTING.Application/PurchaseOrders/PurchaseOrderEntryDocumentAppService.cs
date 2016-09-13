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
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.PurchaseOrders.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Common;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Payables;
using static CAPS.CORPACCOUNTING.Helpers.Helper;
using CAPS.CORPACCOUNTING.CoreHelper;
using System;

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
        private readonly IRepository<PurchaseOrderHistoryUnit, long> _purchaseOrderHistoryRepository;
        private readonly IRepository<AccountingItemUnit, long> _accountingItemUnitRepository;



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
        /// <param name="currencyUnitRepository"></param>
        /// <param name="purchaseOrderHistoryRepository"></param>
        /// <param name="approvedSoxUnitRepository"></param>
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
            IRepository<BankAccountUnit, long> bankAccountUnitRepository,
            IRepository<ApprovedSoxUnit, long> approvedSoxUnitRepository,
            IRepository<PurchaseOrderHistoryUnit, long> purchaseOrderHistoryRepository,
            IRepository<AccountingItemUnit, long> accountingItemUnitRepository

            )
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
            _purchaseOrderHistoryRepository = purchaseOrderHistoryRepository;
            _accountingItemUnitRepository = accountingItemUnitRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_PurchaseOrders_Entry_Create)]
        [UnitOfWork]
        public async Task<IdOutputDto<long>> CreatePurchaseOrderEntryDocumentUnit(PurchaseOrderEntryDocumentInputUnit input)
        {
            var purchaseOrderTransactions = input.MapTo<PurchaseOrderEntryDocumentUnit>();
            purchaseOrderTransactions.TypeOfAccountingDocumentId = TypeOfAccountingDocument.PurchaseOrders;

            //Sum of purchase order Details amount
            purchaseOrderTransactions.PoOriginalAmount = !ReferenceEquals(input.PurchaseOrderDetailList, null) ? input.PurchaseOrderDetailList.Sum(u => u.Amount.Value) : 0;

            IdOutputDto<long> response = new IdOutputDto<long>
            {
                Id = await _purchaseOrderEntryDocumentUnitManager.CreateAsync(purchaseOrderTransactions)
            };

            //Null Checking of PurchaseOrderDetailList
            if (!ReferenceEquals(input.PurchaseOrderDetailList, null))
            {

                var source = CoreHelpers.GetSourceType(typeof(PurchaseOrderEntryDocumentDetailUnit).Name);
                //Bulk Insertion of PurchaseOrderDetail
                foreach (var purchaseOrderDetail in input.PurchaseOrderDetailList)
                {
                    purchaseOrderDetail.AccountingDocumentId = response.Id;
                    var purchaseOrderDetailUnit = purchaseOrderDetail.MapTo<PurchaseOrderEntryDocumentDetailUnit>();
                    purchaseOrderDetailUnit.AccountingItemOrigAmount = purchaseOrderDetail.Amount;
                    purchaseOrderDetailUnit.SourceTypeId = SourceType.PO;
                    await _purchaseOrderDetailUnitManager.CreateAsync(purchaseOrderDetailUnit);
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
        [UnitOfWork]
        public async Task UpdatePurchaseOrderEntryDocumentUnit(PurchaseOrderEntryDocumentInputUnit input)
        {

            var purchaseOrderTransactionsInput = await _purchaseOrderEntryDocumentUnitRepository.GetAsync(input.AccountingDocumentId);

            //Tracking Purchase Order Lines into Purchase Order History Table when PO is Reopend
            if (purchaseOrderTransactionsInput.CloseDate.HasValue && !input.CloseDate.HasValue)
            {
                var PoDetailsList = await _purchaseOrderDetailUnitRepository.GetAllListAsync(u => u.AccountingDocumentId == input.AccountingDocumentId);
            }


            Mapper.Map(input, purchaseOrderTransactionsInput);
            purchaseOrderTransactionsInput.TypeOfAccountingDocumentId = TypeOfAccountingDocument.PurchaseOrders;
            await _purchaseOrderEntryDocumentUnitManager.UpdateAsync(purchaseOrderTransactionsInput);

            if (!ReferenceEquals(input.PurchaseOrderDetailList, null))
            {
                var source = CoreHelpers.GetSourceType(typeof(PurchaseOrderEntryDocumentDetailUnit).Name);
                //Bulk CRUD operations of PurchaseOrderDetail
                foreach (var purchaseOrderDetail in input.PurchaseOrderDetailList)
                {
                    if (purchaseOrderDetail.AccountingItemId == 0)
                    {
                        purchaseOrderDetail.AccountingDocumentId = input.AccountingDocumentId;
                        var purchaseOrderDetailUnit = purchaseOrderDetail.MapTo<PurchaseOrderEntryDocumentDetailUnit>();
                        purchaseOrderDetailUnit.AccountingItemOrigAmount = purchaseOrderDetail.Amount;
                        purchaseOrderDetailUnit.SourceTypeId = SourceType.PO;
                        await _purchaseOrderDetailUnitManager.CreateAsync(purchaseOrderDetailUnit);
                    }
                    else if (purchaseOrderDetail.AccountingItemId > 0)
                    {

                        var purchaseOrderDetailUnit = await _purchaseOrderDetailUnitRepository.GetAsync(purchaseOrderDetail.AccountingItemId);
                        //If PO is Not releaving changing OrignalAmount
                        if (purchaseOrderDetailUnit.AccountingItemOrigAmount == purchaseOrderDetailUnit.Amount && purchaseOrderDetailUnit.Amount != purchaseOrderDetail.Amount)
                        {
                            purchaseOrderDetailUnit.AccountingItemOrigAmount = purchaseOrderDetail.Amount;
                        }
                        Mapper.Map(purchaseOrderDetail, purchaseOrderDetailUnit);
                        purchaseOrderDetailUnit.SourceTypeId = SourceType.PO;
                        if (purchaseOrderDetailUnit.IsClose.Value)
                            purchaseOrderDetailUnit.RemainingAmount = 0;
                        await _purchaseOrderDetailUnitManager.UpdateAsync(purchaseOrderDetailUnit);
                    }
                }
            }

            //Tracking Purchase Order Lines into Purchase Order History Table when PO is closed
            if (!purchaseOrderTransactionsInput.CloseDate.HasValue && input.CloseDate.HasValue)
            {
                var PoDetailsList = await _purchaseOrderDetailUnitRepository.GetAllListAsync(u => u.AccountingDocumentId == input.AccountingDocumentId);
            }
            await CurrentUnitOfWork.SaveChangesAsync();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_PurchaseOrders_Entry_Delete)]
        [UnitOfWork]
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
        [UnitOfWork]
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
                                .Where(u => u.TypeOfObjectId == TypeofObject.AccountingHeaderTransactionsUnit) on purchaseOrder.Id
                            equals approvedsox.ObjectId into approvedsoxs
                        from approvedsox in approvedsoxs.DefaultIfEmpty()
                        join approvedUser in _vendorUnitRepository.GetAll() on approvedsox.ApprovedByUserId equals
                            approvedUser.Id into approvedUser
                        from approvedUsers in approvedUser.DefaultIfEmpty()
                        select new
                        {
                            purchaseOrder,
                            VendorName = vendors.LastName,
                            CreatedUser = users.UserName,
                            ApprovedBy = approvedUser
                        };

            query = query.Where(u =>
                        u.purchaseOrder.TypeOfAccountingDocumentId == TypeOfAccountingDocument.PurchaseOrders &&
                        u.purchaseOrder.IsPosted == unPosted);



            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("purchaseOrder.DocumentReference DESC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            var poIdList = string.Join(",", results.Select(x => x.purchaseOrder.Id).ToArray());

            var poDetailsList = from poDetail in _purchaseOrderDetailUnitRepository.GetAll()
                                join account in _accountUnitRepository.GetAll() on poDetail.AccountId equals account.Id
                                join job in _jobUnitRepository.GetAll() on poDetail.JobId equals job.Id
                                select new
                                {
                                    AccountingDocumentId = poDetail.AccountingDocumentId,
                                    Account = account.AccountNumber,
                                    Job = job.JobNumber,
                                    Amount = poDetail.Amount
                                };

            var poDetails = await poDetailsList.Where(u => poIdList.Contains(u.AccountingDocumentId.ToString())).ToListAsync();
            // var poDetails = poDetailsList.ToListAsync();
            var poOrders = (poDetails.GroupBy(i => i.AccountingDocumentId)
                .Select(g => new
                {
                    g.Key,
                    Jobs = string.Join(",", g.Select(u => u.Job)),
                    Account = string.Join(",", g.Select(u => u.Account)),
                    Amount = g.Sum(u => u.Amount)
                })).ToList();

            var poList = from PO in results
                         join details in poOrders on PO.purchaseOrder.Id equals details.Key
                         select new { PurchaseOrder = PO, PODetails = details };


            return new PagedResultOutput<PurchaseOrderEntryDocumentUnitDto>(resultCount, poList.Select(item =>
            {
                var dto = item.PurchaseOrder.purchaseOrder.MapTo<PurchaseOrderEntryDocumentUnitDto>();
                dto.JobNumber = item.PODetails.Jobs;
                // dto.TypeOfCurrency = item.TypeOfCurrency;
                dto.AccountNumber = item.PODetails.Account;
                dto.AccountingDocumentId = item.PurchaseOrder.purchaseOrder.Id;
                dto.VendorName = item.PurchaseOrder.VendorName;
                dto.CreatedUser = item.PurchaseOrder.CreatedUser;
                dto.RemainingBalance = item.PurchaseOrder.purchaseOrder.ControlTotal.HasValue ? (item.PurchaseOrder.purchaseOrder.ControlTotal.Value - item.PODetails.Amount.Value) : -item.PODetails.Amount.Value;
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
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<PurchaseOrderHistoryUnitDto>> GetPurchaseOrderHistoryByAccountingDocumentId(GetTransactionList input)
        {

            var query = from pODetail in _purchaseOrderHistoryRepository.GetAll()
                        join user in _userUnitRepository.GetAll() on pODetail.CreatorUserId equals user.Id into users
                        from userunits in users.DefaultIfEmpty()
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
                            POHDetails = pODetail,
                            jobs.JobNumber,
                            lines.AccountNumber,
                            subAccount1 = subAccounts1.Description,
                            taxCreditNumber = taxCredits.Number,
                            VendorName = vendors.LastName,
                            CreatedUser= userunits.Name
                        };

            query = query.Where(p => p.POHDetails.AccountingDocumentId.Value == input.AccountingDocumentId);

            var results = await query
                .AsNoTracking()
                 .OrderBy(Helper.GetSort("POHDetails.CreationTime DESC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<PurchaseOrderHistoryUnitDto>(results.Count, results.Select(item =>
            {
                var dto = item.POHDetails.MapTo<PurchaseOrderHistoryUnitDto>();
                dto.SubAccountNumber1 = item.subAccount1;
                dto.JobNumber = item.JobNumber;
                dto.AccountNumber = item.AccountNumber;
                dto.TaxRebateNumber = item.taxCreditNumber;
                dto.TypeOf1099T4 = item.POHDetails.TypeOf1099T4Id != null ? item.POHDetails.TypeOf1099T4Id.ToDisplayName() : "";
                dto.VendorName = item.VendorName;
                dto.AccountingItemId = item.POHDetails.Id;
                dto.ModificationType = item.POHDetails.ModificationTypeId != null ? item.POHDetails.ModificationTypeId.ToDisplayName() : "";
                dto.SourceType=item.POHDetails.SourceTypeId != null ? item.POHDetails.SourceTypeId.ToDisplayName() : "";
                dto.CreatedUser = item.CreatedUser;
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


        /// <summary>
        /// PO Processing By AP/CC/PC/MC/JE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalOrderDetails"></param>
        /// <param name="newOrderDetails"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task PoProcessingByPayType<T>(T originalOrderDetails, T newOrderDetails) where T : InvoiceEntryDocumentDetailUnit, new()
        {
            decimal resultAmount;
            decimal? amount = 0;

            var poAccountingItemId = !ReferenceEquals(newOrderDetails, null) ? newOrderDetails.PoAccountingItemId.Value : originalOrderDetails.PoAccountingItemId.Value;

            //get poDetails by poAccountingItemId
            var poDetail = await _purchaseOrderDetailUnitRepository.GetAsync(poAccountingItemId);

            //Get Source Type based on class name
            //ex InvoiceEntryDocumentDetailUnit class returns source='AP'
            poDetail.SourceTypeId = CoreHelpers.GetSourceType(typeof(T).Name);
           
            if (!ReferenceEquals(newOrderDetails, null))
            {

                // PO is first Time releaving if job or account changes in AP/CC/PC/MC/JE
                //if po amount and AccountingItemOrigAmount is same they allow to change job or account in AP/CC/PC/MC/JE
                if (poDetail.Amount == poDetail.AccountingItemOrigAmount)
                {
                    poDetail.JobId = newOrderDetails.JobId;
                    poDetail.AccountId = newOrderDetails.AccountId;
                    poDetail.SubAccountId1 = newOrderDetails.SubAccountId1;
                    poDetail.SubAccountId2 = newOrderDetails.SubAccountId2;
                    poDetail.SubAccountId3 = newOrderDetails.SubAccountId3;
                    poDetail.ItemMemo = newOrderDetails.ItemMemo;
                    poDetail.AccountRef3 = newOrderDetails.AccountRef3;
                    poDetail.TaxRebateId = newOrderDetails.TaxRebateId;
                    poDetail.VendorId = newOrderDetails.VendorId;
                }
                poDetail.DocumentReference = newOrderDetails.AccountRef3;
                //new invoice Entry 
                if (ReferenceEquals(originalOrderDetails, null) && newOrderDetails.Id == 0)
                {

                    //1000 - 100=900
                    //POAmount - releavingamount from AP/CC/PC/MC/JE
                    resultAmount = poDetail.Amount.Value - newOrderDetails.Amount.Value;
                }// Update invoice Entry
                else
                {
                    //900+100-200=800
                    //POAmount + Existing releavingamount - newly changing releavingamount  from AP/CC/PC/MC/JE
                    resultAmount = poDetail.Amount.Value + originalOrderDetails.Amount.Value - newOrderDetails.Amount.Value;

                    ////history tracking purpose
                    //oldPoOrder.Amount = originalOrderDetails.Amount.Value;
                    //changeInAmount = -newOrderDetails.Amount.Value;// - oldPoOrder.Amount.Value;

                }

                //set OverRelieveAmount based on resultAmount
                if (resultAmount >= 0)
                {
                    poDetail.OverRelieveAmount = 0;
                    amount = resultAmount;
                }
                else
                {
                    poDetail.OverRelieveAmount = resultAmount;
                    amount = 0;
                }

                poDetail.RemainingAmount = resultAmount;
                //PO AccountingItemOrigAmount-resultAmount(Calculated amount)
                poDetail.PendingAmount = poDetail.AccountingItemOrigAmount.Value - resultAmount;
                poDetail.Amount = amount;

            }//while deleting details from AP/CC/PC/MC/JE
            else if (!ReferenceEquals(originalOrderDetails, null))
            {
                //900+100=1000
                //POAmount + Existing releavingamount
                poDetail.Amount = poDetail.Amount + originalOrderDetails.Amount;

                //set OverRelieveAmount value
                if (poDetail.OverRelieveAmount.HasValue)
                {
                    if (poDetail.OverRelieveAmount - originalOrderDetails.Amount > 0)
                        poDetail.OverRelieveAmount = poDetail.OverRelieveAmount - originalOrderDetails.Amount;
                    else
                        poDetail.OverRelieveAmount = 0;
                }
                poDetail.DocumentReference = newOrderDetails.AccountRef3;
            }
            await _purchaseOrderDetailUnitRepository.UpdateAsync(poDetail);
        }


        /// <summary>
        /// Close Purchase Orders by Purchase Orders List
        /// </summary>
        /// <returns></returns>
        [UnitOfWork]
        public async Task ClosePurchaseOrders(ClosePurchaseOrderInputDto input)
        {
            foreach (var accountDocumentId in input.AccountDocumentList)
            {

                //Close Purchase Order
                var purchaseOrder = await _purchaseOrderEntryDocumentUnitRepository.GetAsync(accountDocumentId);
                if (!ReferenceEquals(purchaseOrder, null))
                {
                    purchaseOrder.IsClose = true;
                    purchaseOrder.CloseDate = input.CloseDate;
                    await _purchaseOrderEntryDocumentUnitManager.UpdateAsync(purchaseOrder);
                }
            }
        }
    }
}

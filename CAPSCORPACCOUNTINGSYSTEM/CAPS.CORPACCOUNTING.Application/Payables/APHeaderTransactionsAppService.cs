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
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.PettyCash;
using CAPS.CORPACCOUNTING.PurchaseOrders;
using Abp.Events.Bus;

namespace CAPS.CORPACCOUNTING.Payables
{

    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Payables_Invoices)] //This is to ensure only logged in user has access to this module.
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
        private readonly IRepository<User, long> _userUnitRepository;
        private readonly IRepository<PettyCashAccountUnit, long> _pettyCashAccountUnitRepository;
        private readonly IRepository<VendorPaymentTermUnit> _vendorPaymentTermUnitRepository;
        private readonly IRepository<BankAccountUnit, long> _bankAccountUnitRepository;
        private readonly IRepository<PurchaseOrderEntryDocumentDetailUnit, long> _purchaseOrderEntryDocumentDetailUnitRepository;
        private readonly PurchaseOrderEntryDocumentAppService _purchaseOrderEntryDocumentAppService;

        /// <summary>
        /// 
        /// </summary>
        public IEventBus EventBus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apHeaderTransactionsUnitManager"></param>
        /// <param name="apHeaderTransactionsUnitRepository"></param>
        /// <param name="vendorUnitRepository"></param>
        /// <param name="batchUnitRepository"></param>
        /// <param name="invoiceEntryDocumentDetailUnitRepository"></param>
        /// <param name="invoiceEntryDocumentDetailUnitManager"></param>
        /// <param name="jobUnitRepository"></param>
        /// <param name="accountUnitRepository"></param>
        /// <param name="subAccountUnitRepository"></param>
        /// <param name="taxCreditUnitRepository"></param>
        /// <param name="userUnitRepository"></param>
        /// <param name="pettyCashAccountUnitRepository"></param>
        /// <param name="bankAccountUnitRepository"></param>
        /// <param name="vendorPaymentTermUnitRepository"></param>
        /// <param name="purchaseOrderEntryDocumentDetailUnitRepository"></param>
        /// <param name="purchaseOrderEntryDocumentAppService"></param>
        public APHeaderTransactionsAppService(APHeaderTransactionsUnitManager apHeaderTransactionsUnitManager,
            IRepository<ApHeaderTransactions, long> apHeaderTransactionsUnitRepository, IRepository<VendorUnit> vendorUnitRepository,
            IRepository<BatchUnit> batchUnitRepository, IRepository<InvoiceEntryDocumentDetailUnit, long> invoiceEntryDocumentDetailUnitRepository,
            InvoiceEntryDocumentDetailUnitManager invoiceEntryDocumentDetailUnitManager, IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository, IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<TaxCreditUnit> taxCreditUnitRepository,
            IRepository<User, long> userUnitRepository,
            IRepository<PettyCashAccountUnit, long> pettyCashAccountUnitRepository,
            IRepository<BankAccountUnit, long> bankAccountUnitRepository,
            IRepository<VendorPaymentTermUnit> vendorPaymentTermUnitRepository,
            IRepository<PurchaseOrderEntryDocumentDetailUnit, long> purchaseOrderEntryDocumentDetailUnitRepository,
            PurchaseOrderEntryDocumentAppService purchaseOrderEntryDocumentAppService)
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
            _userUnitRepository = userUnitRepository;
            _pettyCashAccountUnitRepository = pettyCashAccountUnitRepository;
            _bankAccountUnitRepository = bankAccountUnitRepository;
            _vendorPaymentTermUnitRepository = vendorPaymentTermUnitRepository;
            _purchaseOrderEntryDocumentDetailUnitRepository = purchaseOrderEntryDocumentDetailUnitRepository;
            _purchaseOrderEntryDocumentAppService = purchaseOrderEntryDocumentAppService;
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
                    await CreateAPHeaderTransactionDetailUnit(invoiceEntryDocumentDetail, response.Id);
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
                        await CreateAPHeaderTransactionDetailUnit(invoiceEntryDocumentDetail, input.AccountingDocumentId);
                    }
                    else if (invoiceEntryDocumentDetail.AccountingItemId > 0)
                    {
                        await UpdateAPHeaderTransactionDetailUnit(invoiceEntryDocumentDetail);
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
            var invoiceDetailsList = await _invoiceEntryDocumentDetailUnitRepository.GetAllListAsync(p => p.AccountingDocumentId == input.Id);

            foreach (var invoiceDetail in invoiceDetailsList)
            {
                if (invoiceDetail.PoAccountingItemId.Value > 0)
                {
                    await _purchaseOrderEntryDocumentAppService.PoProcessingByPayType(invoiceDetail, null);
                }
            }

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
            bool unPosted = false;
            var query = from invoices in _apHeaderTransactionsUnitRepository.GetAll()
                        join user in _userUnitRepository.GetAll() on invoices.CreatorUserId equals user.Id
                        into users
                        from userunits in users.DefaultIfEmpty()
                        join pcaccount in _pettyCashAccountUnitRepository.GetAll() on invoices.PettyCashAccountId equals pcaccount.Id
                           into pcaccountunit
                        from pcaccountunits in pcaccountunit.DefaultIfEmpty()
                        join account in _accountUnitRepository.GetAll() on pcaccountunits.AccountId equals account.Id
                           into accountunit
                        from accountunits in accountunit.DefaultIfEmpty()
                        join paymentterms in _vendorPaymentTermUnitRepository.GetAll() on invoices.PaymentTermId equals paymentterms.Id
                          into paymenttermsunit
                        from paymenttermunits in paymenttermsunit.DefaultIfEmpty()
                        join batch in _batchUnitRepository.GetAll() on invoices.BatchId equals batch.Id
                           into batchunit
                        from batchunits in batchunit.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on invoices.VendorId equals vendor.Id
                            into vendorunit
                        from vendors in vendorunit.DefaultIfEmpty()
                        join bankaccount in _bankAccountUnitRepository.GetAll() on invoices.BankAccountId equals bankaccount.Id
                            into bankaccountunit
                        from bankaccounts in bankaccountunit.DefaultIfEmpty()
                        select new
                        {
                            Invoices = invoices,
                            BatchName = batchunits.Description,
                            VendorName = vendors.LastName,
                            CreatedUser = userunits.UserName,
                            PaymentTerm = paymenttermunits.Description,
                            BankAccount = bankaccounts.BankAccountNumber,
                            PettyCashAccount = accountunits.Caption

                        };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = query.CreateFilters(mapSearchFilters);
            }
            query = query.Where(p => p.Invoices.OrganizationUnitId == input.OrganizationUnitId)
                .Where(u => u.Invoices.TypeOfAccountingDocumentId == TypeOfAccountingDocument.AccountsPayable &&
                       u.Invoices.IsPosted == unPosted);


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
                dto.CreatedUser = item.CreatedUser;
                dto.PaymentTerm = item.PaymentTerm;
                dto.BankAccount = item.BankAccount;
                dto.PettyCashAccount = item.PettyCashAccount;
                dto.TypeOfCheckGroup = item.Invoices.TypeOfCheckGroupId != null ? item.Invoices.TypeOfCheckGroupId.ToDisplayName() : "";
                dto.TypeOfInvoice = item.Invoices.TypeOfInvoiceId.ToDisplayName();
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
                dto.AccountNumber = item.accountDesc;
                dto.SubAccountNumber1 = item.subAccount1;
                dto.SubAccountNumber2 = item.subAccount2;
                dto.SubAccountNumber3 = item.subAccount3;
                dto.TaxRebateNumber = item.taxCredit;
                dto.AccountingDocumentId = item.InvoiceDetails.Id;
                dto.ActualAmount = item.InvoiceDetails.Amount.Value;// this is to maintainning the actual Amount on calculation
                return dto;
            }).ToList());
        }


        /// <summary>
        /// Creating InvoiceEntryDocumentDetails
        /// </summary>
        /// <param name="input"></param>
        /// /// <param name="accountingDocumnetId"></param>
        /// <returns></returns>
        private async Task CreateAPHeaderTransactionDetailUnit(InvoiceEntryDocumentDetailInputUnit input, long accountingDocumnetId)
        {
            input.AccountingDocumentId = accountingDocumnetId;
            var invoiceEntryDocumentDetailUnit = input.MapTo<InvoiceEntryDocumentDetailUnit>();
            await _invoiceEntryDocumentDetailUnitManager.CreateAsync(invoiceEntryDocumentDetailUnit);

            if (input.PoAccountingItemId.Value > 0)
            {
                await _purchaseOrderEntryDocumentAppService.PoProcessingByPayType(null, invoiceEntryDocumentDetailUnit);
            }
        }

        /// <summary>
        /// Updating APDetails
        /// </summary>
        /// <param name="invoiceEntryDocumentDetail"></param>
        /// <returns></returns>
        private async Task UpdateAPHeaderTransactionDetailUnit(InvoiceEntryDocumentDetailInputUnit invoiceEntryDocumentDetail)
        {
            var invoiceEntryDocumentDetailUnit = await _invoiceEntryDocumentDetailUnitRepository.GetAsync(invoiceEntryDocumentDetail.AccountingItemId);

            if (invoiceEntryDocumentDetail.PoAccountingItemId.Value > 0)
            {
                var newInvoiceDetails = new InvoiceEntryDocumentDetailUnit();
                Mapper.CreateMap<InvoiceEntryDocumentDetailUnit, InvoiceEntryDocumentDetailUnit>();
                invoiceEntryDocumentDetail.MapTo(newInvoiceDetails);
                newInvoiceDetails.Id = invoiceEntryDocumentDetail.AccountingItemId;
                await _purchaseOrderEntryDocumentAppService.PoProcessingByPayType(invoiceEntryDocumentDetailUnit, newInvoiceDetails);
            }
            Mapper.Map(invoiceEntryDocumentDetail, invoiceEntryDocumentDetailUnit);
           
            await _invoiceEntryDocumentDetailUnitManager.UpdateAsync(invoiceEntryDocumentDetailUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_PurchaseOrders_Entry_Delete)]
        [UnitOfWork]
        public async Task DeleteAPHeaderTransactionDetailUnit(IdInput<long> input)
        {
            var invoiceDetail = await _invoiceEntryDocumentDetailUnitRepository.GetAsync(input.Id);

            if(invoiceDetail.PoAccountingItemId.Value>0)
            await _purchaseOrderEntryDocumentAppService.PoProcessingByPayType(invoiceDetail, null);

            await _invoiceEntryDocumentDetailUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}

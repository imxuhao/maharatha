using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Payables.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users;

namespace CAPS.CORPACCOUNTING.Payables
{
    [AbpAuthorize] //This is to ensure only logged in user has access to this module.
    public class APHeaderTransactionsAppService : CORPACCOUNTINGServiceBase, IAPHeaderTransactionsAppService
    {
        private readonly APHeaderTransactionsUnitManager _apHeaderTransactionsUnitManager;
        private readonly IRepository<ApHeaderTransactions> _apHeaderTransactionsUnitRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public APHeaderTransactionsAppService(APHeaderTransactionsUnitManager apHeaderTransactionsUnitManager, IRepository<ApHeaderTransactions> apHeaderTransactionsUnitRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _apHeaderTransactionsUnitManager = apHeaderTransactionsUnitManager;
            _apHeaderTransactionsUnitRepository = apHeaderTransactionsUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;

        }
        public async Task CreateAPHeaderTransactionUnit(CreateAPHeaderTransactionsInputUnit input)
        {            
                var apHeaderTransactions = new ApHeaderTransactions(batchid: input.BatchId, vendorid: input.VendorId, typeofinvoiceid: input.TypeOfInvoiceId, pettycashaccountid: input.PettyCashAccountId,
                    paymenttermid: input.PaymentTermId, typeofcheckgroupid: input.TypeOfCheckGroupId, bankaccountid: input.BankAccountId,
                    paymentdate: input.PaymentDate, paymentnumber: input.PaymentNumber, purchaseorderreference: input.PurchaseOrderReference,
                    reversedbyuserid: input.ReversedByUserId, reversaldate: input.ReversalDate, isinvoicehistory: input.IsInvoiceHistory, isenterable: input.IsEnterable,
                    generatedaccountingdocumentid: input.GeneratedAccountingDocumentId, uploaddocumentlogid: input.UploadDocumentLogID,
                    batchinfo: input.BatchInfo, paymentselectedbyuserid: input.PaymentSelectedByUserId, description: input.Description,
                    typeofaccountingdocumentid: input.TypeOfAccountingDocumentId, typeofobjectid: input.TypeOfObjectId, recurdocid: input.RecurDocId, reversedocid: input.ReverseDocId,
                    documentdate: input.DocumentDate, transactiondate: input.TransactionDate, dateposted: input.DatePosted, originaldocumentid: input.OriginalDocumentId,
                    controltotal: input.ControlTotal, documentreference: input.DocumentReference, voucherreference: input.VoucherReference, typeofcurrencyid: input.TypeOfCurrencyId,
                    currencyadjustmentid: input.CurrencyAdjustmentId, postbatchdescription: input.PostBatchDescription, isposted: input.IsPosted, isautoposted: input.IsAutoPosted,
                    ischanged: input.IsChanged, postedbyuserid: input.PostedByUserId, bankreccontrolid: input.BankRecControlId, isselected: input.IsSelected,
                    isactive: input.IsActive, isapproved: input.IsApproved, typeofinactivestatusid: input.TypeOfInactiveStatusId, isbankrecomitted: input.IsBankRecOmitted,
                    isictjournal: input.IsICTJournal, ictcompanyid: input.ICTCompanyId, ictaccountingdocumentid: input.ICTAccountingDocumentId, currencyoverriderate: input.CurrencyOverrideRate,
                    functionalcurrencycontroltotal: input.FunctionalCurrencyControlTotal, typeofcurrencyrateid: input.TypeOfCurrencyRateId, memoline: input.MemoLine, is13period: input.Is13Period,
                   homecurrencyamount: input.HomeCurrencyAmount, customforexrate: input.CustomForexRate, isposubmitforapproval: input.IsPOSubmitForApproval, iscpastran: input.IsCPASTran,
                   cpasprojcloseid: input.CPASProjCloseId, cpasprojid: input.CPASProjId, organizationunitid: input.OrganizationUnitId);
                await _apHeaderTransactionsUnitManager.CreateAsync(apHeaderTransactions);
                await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}

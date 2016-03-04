using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Payables.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users;
using Abp.Application.Services.Dto;

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

        /// <summary>
        /// Create APHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update ApHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAPHeaderTransactionUnit(UpdateAPHeaderTransactionsInputUnit input)
        {
            var aPHeaderTransactionsInput = await _apHeaderTransactionsUnitRepository.GetAsync(input.AHTID);
            aPHeaderTransactionsInput.BatchId = input.BatchId;
            aPHeaderTransactionsInput.VendorId = input.VendorId;
            aPHeaderTransactionsInput.TypeOfInvoiceId = input.TypeOfInvoiceId;
            aPHeaderTransactionsInput.PettyCashAccountId = input.PettyCashAccountId;
            aPHeaderTransactionsInput.PaymentTermId = input.PaymentTermId;
            aPHeaderTransactionsInput.TypeOfCheckGroupId = input.TypeOfCheckGroupId;
            aPHeaderTransactionsInput.BankAccountId = input.BankAccountId;
            aPHeaderTransactionsInput.PaymentDate = input.PaymentDate;
            aPHeaderTransactionsInput.PaymentNumber = input.PaymentNumber;
            aPHeaderTransactionsInput.PurchaseOrderReference = input.PurchaseOrderReference;
            aPHeaderTransactionsInput.ReversedByUserId = input.ReversedByUserId;
            aPHeaderTransactionsInput.ReversalDate = input.ReversalDate;
            aPHeaderTransactionsInput.IsInvoiceHistory = input.IsInvoiceHistory;
            aPHeaderTransactionsInput.IsEnterable = input.IsEnterable;
            aPHeaderTransactionsInput.GeneratedAccountingDocumentId = input.GeneratedAccountingDocumentId;
            aPHeaderTransactionsInput.UploadDocumentLogID = input.UploadDocumentLogID;
            aPHeaderTransactionsInput.BatchInfo = input.BatchInfo;
            aPHeaderTransactionsInput.PaymentSelectedByUserId = input.PaymentSelectedByUserId;
            aPHeaderTransactionsInput.Description = input.Description;
            aPHeaderTransactionsInput.TypeOfAccountingDocumentId = input.TypeOfAccountingDocumentId;
            aPHeaderTransactionsInput.TypeOfObjectId = input.TypeOfObjectId;
            aPHeaderTransactionsInput.RecurDocId = input.RecurDocId;
            aPHeaderTransactionsInput.ReverseDocId = input.ReverseDocId;
            aPHeaderTransactionsInput.DocumentDate = input.DocumentDate;
            aPHeaderTransactionsInput.TransactionDate = input.TransactionDate;
            aPHeaderTransactionsInput.DatePosted = input.DatePosted;
            aPHeaderTransactionsInput.OriginalDocumentId = input.OriginalDocumentId;
            aPHeaderTransactionsInput.ControlTotal = input.ControlTotal;
            aPHeaderTransactionsInput.DocumentReference = input.DocumentReference;
            aPHeaderTransactionsInput.VoucherReference = input.VoucherReference;
            aPHeaderTransactionsInput.TypeOfCurrencyId = input.TypeOfCurrencyId;
            aPHeaderTransactionsInput.CurrencyAdjustmentId = input.CurrencyAdjustmentId;
            aPHeaderTransactionsInput.PostBatchDescription = input.PostBatchDescription;
            aPHeaderTransactionsInput.IsPosted = input.IsPosted;
            aPHeaderTransactionsInput.IsAutoPosted = input.IsAutoPosted;
            aPHeaderTransactionsInput.IsChanged = input.IsChanged;
            aPHeaderTransactionsInput.PostedByUserId = input.PostedByUserId;
            aPHeaderTransactionsInput.BankRecControlId = input.BankRecControlId;
            aPHeaderTransactionsInput.IsSelected = input.IsSelected;
            aPHeaderTransactionsInput.IsActive = input.IsActive;
            aPHeaderTransactionsInput.IsApproved = input.IsApproved;
            aPHeaderTransactionsInput.TypeOfInactiveStatusId = input.TypeOfInactiveStatusId;
            aPHeaderTransactionsInput.IsBankRecOmitted = input.IsBankRecOmitted;
            aPHeaderTransactionsInput.IsICTJournal = input.IsICTJournal;
            aPHeaderTransactionsInput.ICTCompanyId = input.ICTCompanyId;
            aPHeaderTransactionsInput.ICTAccountingDocumentId = input.ICTAccountingDocumentId;
            aPHeaderTransactionsInput.CurrencyOverrideRate = input.CurrencyOverrideRate;
            aPHeaderTransactionsInput.FunctionalCurrencyControlTotal = input.FunctionalCurrencyControlTotal;
            aPHeaderTransactionsInput.TypeOfCurrencyRateId = input.TypeOfCurrencyRateId;
            aPHeaderTransactionsInput.MemoLine = input.MemoLine;
            aPHeaderTransactionsInput.Is13Period = input.Is13Period;
            aPHeaderTransactionsInput.HomeCurrencyAmount = input.HomeCurrencyAmount;
            aPHeaderTransactionsInput.CustomForexRate = input.CustomForexRate;
            aPHeaderTransactionsInput.IsPOSubmitForApproval = input.IsPOSubmitForApproval;
            aPHeaderTransactionsInput.IsCPASTran = input.IsCPASTran;
            aPHeaderTransactionsInput.CPASProjCloseId = input.CPASProjCloseId;
            aPHeaderTransactionsInput.CPASProjId = input.CPASProjId;
            aPHeaderTransactionsInput.OrganizationUnitId = input.OrganizationUnitId;
            await _apHeaderTransactionsUnitManager.UpdateAsync(aPHeaderTransactionsInput);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete ApHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteAPHeaderTransactionUnit(IdInput input)
        {
            await _apHeaderTransactionsUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}

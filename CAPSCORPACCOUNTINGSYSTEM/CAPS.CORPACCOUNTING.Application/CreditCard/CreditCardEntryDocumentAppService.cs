using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.CreditCard.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.ChargeEntry;
using System.Linq;
using Abp.AutoMapper;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Helpers;
using AutoMapper;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Payables;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.CreditCard
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditCardEntryDocumentAppService : CORPACCOUNTINGServiceBase, ICreditCardEntryDocumentAppService
    {


        private readonly IRepository<ChargeEntryDocumentUnit, long> _creditCardUnitRepository;
        private readonly IRepository<ApHeaderTransactions, long> _apHeaderTransactionsUnitRepository;
        private readonly IRepository<ChargeEntryDocumentDetailUnit, long> _creditCardDetailUnitRepository;
        private readonly IRepository<BankAccountUnit, long> _bankAccountUnitRepository;
        private readonly IRepository<TypeOfCurrencyUnit, short> _typeOfCurrencyUnitRepository;
        private readonly IRepository<VendorUnit, int> _vendorUnitRepository;
        ChargeEntryDocumentUnitManager _chargeEntryDocumentUnitManager;
        ChargeEntryDocumentDetailUnitManager _chargeEntryDocumentDetailUnitManager;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditcarddetailunitrepository"></param>
        /// <param name="bankaccountunitrepository"></param>
        /// <param name="creditcardunitrepository"></param>
        /// <param name="typeofcurrencyunitrepository"></param>
        /// <param name="vendorunitrepository"></param>
        /// <param name="chargeentrydocumentunitmanager"></param>
        /// <param name="chargeentrydocumentdetailunitmanager"></param>

        public CreditCardEntryDocumentAppService(
            IRepository<ChargeEntryDocumentDetailUnit, long> creditcarddetailunitrepository,
            IRepository<BankAccountUnit, long> bankaccountunitrepository,
            IRepository<ChargeEntryDocumentUnit, long> creditcardunitrepository,
            IRepository<TypeOfCurrencyUnit, short> typeofcurrencyunitrepository,
            IRepository<VendorUnit, int> vendorunitrepository,
            ChargeEntryDocumentUnitManager chargeentrydocumentunitmanager,
             ChargeEntryDocumentDetailUnitManager chargeentrydocumentdetailunitmanager
            )
        {

            _creditCardDetailUnitRepository = creditcarddetailunitrepository;
            _bankAccountUnitRepository = bankaccountunitrepository;
            _creditCardUnitRepository = creditcardunitrepository;
            _typeOfCurrencyUnitRepository = typeofcurrencyunitrepository;
            _vendorUnitRepository = vendorunitrepository;
            _chargeEntryDocumentUnitManager = chargeentrydocumentunitmanager;
            _chargeEntryDocumentDetailUnitManager = chargeentrydocumentdetailunitmanager;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IdOutputDto<long>> CreateCreditCardEntryDocumentUnit(CreditCardEntryDocumentInputUnit input)
        {
            var creditEntryTransactions = input.MapTo<ChargeEntryDocumentUnit>();
            IdOutputDto<long> response = new IdOutputDto<long>
            {
                Id = await _chargeEntryDocumentUnitManager.CreateAsync(creditEntryTransactions)
            };

            //Null Checking of CreditCardEntryDetailList
            if (!ReferenceEquals(input.CreditCardEntryDetailList, null))
            {
                //Bulk Insertion of creditCardEntryDocumentDetail
                foreach (var creditCardEntryDocumentDetail in input.CreditCardEntryDetailList)
                {
                    creditCardEntryDocumentDetail.AccountingDocumentId = response.Id;
                    var invoiceEntryDocumentDetailUnit = creditCardEntryDocumentDetail.MapTo<ChargeEntryDocumentDetailUnit>();
                    await _chargeEntryDocumentDetailUnitManager.CreateAsync(invoiceEntryDocumentDetailUnit);
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
        public async Task UpdateCreditCardEntryDocumentUnit(CreditCardEntryDocumentInputUnit input)
        {
            var creditEntryTransactions = await _creditCardUnitRepository.GetAsync(input.AccountingDocumentId);
            Mapper.Map(input, creditEntryTransactions);
            await _chargeEntryDocumentUnitManager.UpdateAsync(creditEntryTransactions);

            if (!ReferenceEquals(input.CreditCardEntryDetailList, null))
            {
                //Bulk CRUD operations of CreditCardEntryDetailList
                foreach (var creditCardEntryDocumentDetail in input.CreditCardEntryDetailList)
                {
                    if (creditCardEntryDocumentDetail.AccountingItemId == 0)
                    {
                        creditCardEntryDocumentDetail.AccountingDocumentId = input.AccountingDocumentId;
                        var creditCardEntryDocumentDetailUnit =
                            creditCardEntryDocumentDetail.MapTo<ChargeEntryDocumentDetailUnit>();
                        await _chargeEntryDocumentDetailUnitManager.CreateAsync(creditCardEntryDocumentDetailUnit);
                    }
                    else if (creditCardEntryDocumentDetail.AccountingItemId > 0)
                    {
                        var creditcardDocumentDetailUnit = await _creditCardDetailUnitRepository.GetAsync(
                                    creditCardEntryDocumentDetail.AccountingItemId);
                        Mapper.Map(creditCardEntryDocumentDetail, creditcardDocumentDetailUnit);
                        await _chargeEntryDocumentDetailUnitManager.UpdateAsync(creditcardDocumentDetailUnit);
                    }
                    else
                    {
                        IdInput<long> idInput = new IdInput<long>()
                        {
                            Id = (creditCardEntryDocumentDetail.AccountingItemId * (-1))
                        };
                        await _chargeEntryDocumentDetailUnitManager.DeleteAsync(idInput);
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
        public async Task DeleteCreditCardDetailUnit(IdInput input)
        {
            await _creditCardDetailUnitRepository.DeleteAsync(p => p.AccountingDocumentId == input.Id);
            await _chargeEntryDocumentUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteCreditCardEntryDocumentUnit(IdInput input)
        {
            await _creditCardDetailUnitRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultOutput<CreditCardEntryDocumentUnitDto>> GetCreditCardDetailsByDocumentId(GetTransactionList input)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<CreditCardEntryDocumentUnitDto>> GetCreditCardDetailStatementsUnits(SearchInputDto input)
        {
            var query = from creditCard in _creditCardUnitRepository.GetAll()
                        join bankAccount in _bankAccountUnitRepository.GetAll() on creditCard.BankAccountId equals bankAccount.Id
                         into bankaccount
                        from bankaccounts in bankaccount.DefaultIfEmpty()
                        join currency in _typeOfCurrencyUnitRepository.GetAll() on creditCard.TypeOfCurrencyId equals currency.Id
                         into currency
                        from currencys in currency.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on creditCard.VendorId equals vendor.Id
                        into vendor
                        from vendors in vendor.DefaultIfEmpty()
                        join apInvoice in _apHeaderTransactionsUnitRepository.GetAll() on creditCard.ApInvoiceAccountingDocId equals apInvoice.Id
                        into apInvoice
                        from apInvoices in apInvoice.DefaultIfEmpty()
                        where creditCard.IsPosted == false && creditCard.TypeOfInvoiceId == TypeOfInvoice.CreditCard
                        select new
                        {
                            creditCard,
                            Description = bankaccounts.Description,
                            BankAccountNumber = bankaccounts.BankAccountNumber,
                            //InvoiceNumber=creditCard.DocumentReference,
                            //PostingDate=creditCard.TransactionDate,
                            //CreditCardTotal=creditCard.ControlTotal,
                            APGenerated = apInvoices != null ? apInvoices.CreationTime.ToString() : "",
                            BuildAP = apInvoices != null ? "Build AP" : "",
                            //IsPosted = creditCard.IsPosted,
                            TypeOfCurrency = currencys.Caption,
                            VendorName = vendors.LastName

                        };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                query = Helper.CreateFilters(query, mapSearchFilters);
            }

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("TransactionDate DESC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<CreditCardEntryDocumentUnitDto>(resultCount, results.Select(item =>
            {
                var cardLength = item.BankAccountNumber.Length;
                var dto = item.MapTo<CreditCardEntryDocumentUnitDto>();
                dto.BuildAP = item.BuildAP;
                dto.APGenerated = item.APGenerated;
                dto.TypeOfCurrency = item.TypeOfCurrency;
                dto.VendorName = item.VendorName;
                dto.Description = !string.IsNullOrEmpty(item.BankAccountNumber) ? item.BankAccountNumber.Substring(cardLength - 5, cardLength) : "";
                return dto;
            }).ToList());

        }

        /// <summary>
        /// get CreditCard statements group by 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<CreditCardStatementDto>> GetCreditCardStatementsUnits(SearchInputDto input)
        {
            var query = from creditCard in _creditCardUnitRepository.GetAll()
                        join bankAccount in _bankAccountUnitRepository.GetAll() on creditCard.BankAccountId equals bankAccount.Id
                        into bankaccount
                        from bankaccounts in bankaccount.DefaultIfEmpty()
                        join bankaccount1 in _bankAccountUnitRepository.GetAll() on bankaccounts.ControllingBankAccountId equals bankaccount1.Id
                        into bankaccount1
                        from bankaccounts1 in bankaccount1.DefaultIfEmpty()
                        where creditCard.IsPosted == false
                        group creditCard by
                        new
                        {
                            DocumentDate = creditCard.DocumentDate,
                            TransactionDate = creditCard.TransactionDate,
                            ControllingBankAccountID = bankaccounts.ControllingBankAccountId,
                            Description = bankaccounts1.Description + "  " + bankaccounts1.BankAccountNumber,
                            IsPosted = creditCard.IsPosted
                        } into g
                        select new
                        {
                            DocumentDate = g.Key.DocumentDate,
                            TransactionDate = g.Key.TransactionDate,
                            ControllingBankAccountID = g.Key.ControllingBankAccountID,
                            Description = g.Key.Description,
                            IsPosted = g.Key.IsPosted,
                            ControlTotal=g.Sum(q=>q.ControlTotal),
                            TrxId = g.Min(q => q.Id)
                        };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                query = Helper.CreateFilters(query, mapSearchFilters);
            }

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("TransactionDate DESC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<CreditCardStatementDto>(resultCount, results.Select(item =>
            {
                var dto = item.MapTo<CreditCardStatementDto>();
                dto.DocumentDate = item.DocumentDate;
                dto.TransactionDate = item.TransactionDate;
                dto.IsPosted = item.IsPosted;
                dto.Description = item.Description;
                dto.ControllingBankAccountId = item.ControllingBankAccountID;
                dto.ControlTotal = item.ControlTotal;
                return dto;
            }).ToList());

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PagedResultOutput<CreditCardEntryDocumentUnitDto>> GetCreditCardHistory(SearchInputDto input)
        {
            //var query = from creditcard in _chargeEntryDocumentUnitRepository.GetAll()
            //            join creditcarddetails in _chargeEntryDocumentDetailUnitRepository.GetAll()
            //            on creditcard.CreatorUserId equals usercreditcarddetails.Id into users
            //            from userunits in users.DefaultIfEmpty()


            var query = from creditCard in _creditCardUnitRepository.GetAll()
                        join bankAccount in _bankAccountUnitRepository.GetAll() on creditCard.BankAccountId equals bankAccount.Id
                        select new { CardHolderName = bankAccount.Description, CardNumber = bankAccount.BankAccountNumber, OrganizationUnitId = creditCard.OrganizationUnitId, AccountingDocumentId = creditCard.Id };


            //var query = from creditcard in _chargeEntryDocumentUnitRepository.GetAll()
            //            join creditcarddetails in _chargeEntryDocumentDetailUnitRepository.GetAll() on creditcard.Id equals creditcarddetails.AccountingDocumentId
            //            select;
            return null;
        }
    }
}

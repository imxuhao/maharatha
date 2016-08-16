using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Authorization.Users;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using CAPS.CORPACCOUNTING.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Authorization;
using Abp.Collections.Extensions;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Common;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Sessions;
using CAPS.CORPACCOUNTING.Uploads.Dto;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

namespace CAPS.CORPACCOUNTING.Accounts
{
    public class AccountUnitAppService : CORPACCOUNTINGAppServiceBase, IAccountUnitAppService
    {
        private readonly AccountUnitManager _accountUnitManager;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<TypeOfAccountUnit, int> _typeOfAccountRepository;
        private readonly IRepository<TypeOfCurrencyRateUnit, short> _typeOfCurrencyRateRepository;
        private readonly IRepository<TypeOfCurrencyUnit, short> _typeOfCurrencyRepository;
        private readonly IRepository<CoaUnit, int> _coaRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomAppSession _customAppSession;
        private readonly AccountCache _accountcache;

        public AccountUnitAppService(AccountUnitManager accountUnitManager, IRepository<AccountUnit, long> accountUnitRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager, IRepository<TypeOfAccountUnit, int> typeOfAccountRepository,
            IRepository<TypeOfCurrencyRateUnit, short> typeOfCurrencyRateRepository, IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyRepository,
            IRepository<CoaUnit, int> coaRepository, AccountCache accountcache, CustomAppSession customAppSession)
        {
            _accountUnitManager = accountUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _typeOfAccountRepository = typeOfAccountRepository;
            _typeOfCurrencyRateRepository = typeOfCurrencyRateRepository;
            _typeOfCurrencyRepository = typeOfCurrencyRepository;
            _coaRepository = coaRepository;
            _accountcache = accountcache;
            _customAppSession = customAppSession;
        }

        /// <summary>
        /// Get the Accounts by CoaId with paging sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts)]
        public async Task<PagedResultOutput<AccountUnitDto>> GetAccountUnitsByCoaId(GetAccountInput input)
        {
            var query =
                from au in _accountUnitRepository.GetAll()
                join linkAu in _accountUnitRepository.GetAll() on au.LinkAccountId equals linkAu.Id
                into linkAccount
                from linkAccounts in linkAccount.DefaultIfEmpty()
                join typeOfAccount in _typeOfAccountRepository.GetAll() on au.TypeOfAccountId equals typeOfAccount.Id
                into accounts
                from typeofaccounts in accounts.DefaultIfEmpty()
                join currencytype in _typeOfCurrencyRepository.GetAll() on au.TypeOfCurrencyId equals currencytype.Id
                into currencyt
                from currency in currencyt.DefaultIfEmpty()
                join currencyrate in _typeOfCurrencyRateRepository.GetAll() on au.TypeOfCurrencyRateId equals currencyrate.Id
                into ratecurrency
                from accountresults in ratecurrency.DefaultIfEmpty()
                select new
                {
                    Account = au,
                    TypeOfAccount = typeofaccounts.Description,
                    TypeOfAccountRate = accountresults.Description,
                    TypeOfCurrency = currency.Description,
                    LinkAccount = linkAccounts.AccountNumber

                };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item => item.Account.ChartOfAccountId == input.CoaId);


            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Account.AccountNumber ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<AccountUnitDto>(resultCount, results.Select(item =>
                 {
                     var dto = item.Account.MapTo<AccountUnitDto>();
                     dto.AccountId = item.Account.Id;
                     dto.TypeofConsolidation = item.Account.TypeofConsolidationId != null ? item.Account.TypeofConsolidationId.ToDisplayName() : "";
                     dto.TypeOfAccount = item.TypeOfAccount;
                     dto.TypeOfCurrency = item.TypeOfCurrency;
                     dto.TypeOfCurrencyRate = item.TypeOfAccountRate;
                     dto.LinkAccount = item.LinkAccount;
                     return dto;
                 }).ToList());
        }

        /// <summary>
        /// Crating the AccountUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts_Create)]
        public async Task<IdOutputDto<long>> CreateAccountUnit(CreateAccountUnitInput input)
        {
            var accountUnit = input.MapTo<AccountUnit>();
            accountUnit.ParentId = input.ParentId != 0 ? input.ParentId : null;
            IdOutputDto<long> responseDto = new IdOutputDto<long>
            {
                AccountId = await _accountUnitManager.CreateAsync(accountUnit)
            };
            await CurrentUnitOfWork.SaveChangesAsync();
            _unitOfWorkManager.Current.Completed += (sender, args) => { };
            return responseDto;
        }
        /// <summary>
        /// Updating the AccountUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts_Edit)]
        public async Task<IdOutputDto<long>> UpdateAccountUnit(UpdateAccountUnitInput input)
        {
            var accountUnit = await _accountUnitRepository.GetAsync(input.AccountId);

            #region Setting the values to be updated

            accountUnit.AccountNumber = input.AccountNumber;
            accountUnit.Caption = input.Caption;
            accountUnit.ChartOfAccountId = input.ChartOfAccountId;
            accountUnit.ParentId = input.ParentId != 0 ? input.ParentId : null;
            accountUnit.OrganizationUnitId = input.OrganizationUnitId;
            accountUnit.BalanceSheetName = input.BalanceSheetName;
            accountUnit.CashFlowName = input.CashFlowName;
            accountUnit.Description = input.Description;
            accountUnit.IsActive = input.IsActive;
            accountUnit.IsApproved = input.IsApproved;
            accountUnit.IsBalanceSheet = input.IsBalanceSheet;
            accountUnit.IsCashFlow = input.IsCashFlow;
            accountUnit.IsDescriptionLocked = input.IsDescriptionLocked;
            accountUnit.IsDocControlled = input.IsDocControlled;
            accountUnit.IsElimination = input.IsElimination;
            accountUnit.IsEnterable = input.IsEnterable;
            accountUnit.IsProfitLoss = input.IsProfitLoss;
            accountUnit.IsRollupAccount = input.IsRollupAccount;
            accountUnit.IsRollupOverridable = input.IsRollupOverridable;
            accountUnit.IsSummaryAccount = input.IsSummaryAccount;
            accountUnit.IsUs1120BalanceSheet = input.IsUs1120BalanceSheet;
            accountUnit.IsUs1120IncomeStmt = input.IsUs1120IncomeStmt;
            accountUnit.LinkAccountId = input.LinkAccountId;
            accountUnit.LinkJobId = input.LinkJobId;
            accountUnit.ProfitLossName = input.ProfitLossName;
            accountUnit.RollupAccountId = input.RollupAccountId;
            accountUnit.IsAccountRevalued = input.IsAccountRevalued;
            accountUnit.TypeOfCurrencyId = input.TypeOfCurrencyId;
            accountUnit.TypeOfCurrencyRateId = input.TypeOfCurrencyRateId;
            accountUnit.TypeofConsolidationId = input.TypeofConsolidationId;
            accountUnit.RollupJobId = input.RollupJobId;
            accountUnit.TypeOfAccountId = input.TypeOfAccountId;
            accountUnit.Us1120BalanceSheetName = input.Us1120BalanceSheetName;
            accountUnit.DisplaySequence = input.DisplaySequence;
            accountUnit.Us1120IncomeStmtName = input.Us1120IncomeStmtName;
            #endregion

            await _accountUnitManager.UpdateAsync(accountUnit);

            await CurrentUnitOfWork.SaveChangesAsync();
            IdOutputDto<long> responseDto = new IdOutputDto<long>
            {
                AccountId = accountUnit.Id
            };
            return responseDto;
        }
        /// <summary>
        /// Deleting the Account by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts_Delete)]
        public async Task DeleteAccountUnit(IdInput<long> input)
        {
            await _accountUnitManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// Get the AccountDetails by AccountId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AccountUnitDto> GetAccountUnitsById(IdInput input)
        {
            AccountUnit accountUnit = await _accountUnitRepository.GetAsync(input.Id);
            AccountUnitDto result = accountUnit.MapTo<AccountUnitDto>();
            result.AccountId = accountUnit.Id;
            return result;

        }


        /// <summary>
        /// Get TypeofConsolidation
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofConsolidationList()
        {
            return EnumList.GetTypeofConsolidationList();
        }

        /// <summary>
        /// Get TypeOfCurrency
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfCurrencyList()
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var typeOfCurrency =
                    await
                        _typeOfCurrencyRepository.GetAll()
                            .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                            .ToListAsync();
                return typeOfCurrency;
            }
        }

        /// <summary>
        /// Get TypeOfAccount List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfAccountList()
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var typeOfAccounts = await _typeOfAccountRepository.GetAll()
                            .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                            .ToListAsync();
                return typeOfAccounts;
            }
        }

        /// <summary>
        /// Get TypeOfCurrencyRate List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfCurrencyRateList()
        {

            var typeOfCurrencyRates = await _typeOfCurrencyRateRepository.GetAll()
                        .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                        .ToListAsync();
            return typeOfCurrencyRates;

        }
        /// <summary>
        /// Get LinkAccount List By CoaId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetLinkAccountListByCoaId(AutoSearchInput input)
        {
            var linkCoaId = _coaRepository.GetAll().Where(u => u.Id == input.Id).Select(u => u.LinkChartOfAccountID).FirstOrDefault();

            var linkAccountList = _accountUnitRepository.GetAll().Where(u => u.ChartOfAccountId == linkCoaId);
            if (!string.IsNullOrEmpty(input.Query))
                linkAccountList = linkAccountList.Where(u => u.Caption.Contains(input.Query));

            var result = await linkAccountList.Select(u => new NameValueDto { Name = u.Caption, Value = u.Id.ToString() })
             .ToListAsync();
            return result;
        }

        /// <summary>
        /// Get CorporateRollupAccountsList 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountCacheItem>> GetRollupAccountsList(AutoSearchInput input)
        {
            var accountList = await _accountcache.GetAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);

            return accountList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.AccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where(p => p.IsCorporate == true && p.IsRollupAccount == true).ToList();

        }

        /// <summary>
        /// Get Account by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AccountUnitDto> GetAccountById(IdInput<long> input)
        {
            AccountUnit accountUnit = await _accountUnitRepository.GetAsync(input.Id);
            AccountUnitDto result = accountUnit.MapTo<AccountUnitDto>();
            result.AccountId = accountUnit.Id;
            return result;
        }

        /// <summary>
        /// Importing the Accounts From Excel
        /// </summary>
        /// <param name="accountTable"></param>
        /// <param name="coaId"></param>
        /// <returns></returns>
        public async Task<List<UploadErrorMessagesOutputDto>> ImportAccounts(DataTable accountTable, int coaId)
        {
            var classificationlist = await GetTypeOfAccountList();
            var currencylist = await GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
            var typeOfCurrencyRateList = await GetTypeOfCurrencyRateList();
            var linkedAccountList = await GetLinkAccountListByCoaId(new AutoSearchInput() { Id = coaId });
            List<UploadErrorMessagesOutputDto> uploadErrorMessagesList = new List<UploadErrorMessagesOutputDto>();
            List<CreateAccountUnitInput> createAccountList = new List<CreateAccountUnitInput>();
            Dictionary<int, CreateAccountUnitInput> accountsList = new Dictionary<int, CreateAccountUnitInput>();
            foreach (DataRow datarow in accountTable.Rows)
            {
                int? typeofaccount = null;
                short? currencyId = null;
                TypeofConsolidation? consolidationId = null;
                short? typeOfCurrencyRateId = null;
                int? linkedAccountId = null;
                var classification = classificationlist.FirstOrDefault(p => p.Name == datarow[L("Classification")].ToString());
                if (classification != null)
                {
                    typeofaccount = Convert.ToInt32(classification.Value);
                }
                var currency = currencylist.FirstOrDefault(p => p.Name == datarow[L("Currency")].ToString());
                if (currency != null)
                {
                    currencyId = Convert.ToInt16(currency.Value);
                }
                var consolidation = consolidationList.FirstOrDefault(p => p.Name == datarow[L("Consolidation")].ToString());
                if (consolidation != null)
                {
                    consolidationId = (TypeofConsolidation)Convert.ToInt32(consolidation.Value);
                }
                var typeOfCurrencyRate = typeOfCurrencyRateList.FirstOrDefault(p => p.Name == datarow[L("RateTypeOverride")].ToString());
                if (typeOfCurrencyRate != null)
                {
                    typeOfCurrencyRateId = Convert.ToInt16(typeOfCurrencyRate.Value);
                }
                var linkedAccount = linkedAccountList.FirstOrDefault(p => p.Name == datarow[L("NewAccount")].ToString());
                if (linkedAccount != null)
                {
                    linkedAccountId = Convert.ToInt32(linkedAccount.Value);
                }
                var input = new CreateAccountUnitInput
                {
                    AccountNumber = datarow[L("AccountNumber")].ToString(),
                    Caption = datarow[L("Description")].ToString(),
                    TypeOfAccountId = typeofaccount,
                    TypeOfCurrencyId = currencyId,
                    TypeofConsolidationId = consolidationId,
                    LinkAccountId = linkedAccountId,
                    TypeOfCurrencyRateId = typeOfCurrencyRateId,
                    IsAccountRevalued = Helper.ConvertToBoolean(datarow[L("Multi-CurrencyReval")].ToString()),
                    IsElimination = Helper.ConvertToBoolean(datarow[L("EliminationAccount")].ToString()),
                    IsRollupAccount = Helper.ConvertToBoolean(datarow[L("RollUpAccount")].ToString()),
                    IsEnterable = Helper.ConvertToBoolean(datarow[L("JournalsAllowed")].ToString()),
                    ChartOfAccountId = coaId
                };
                UploadErrorMessagesOutputDto errorMessageDto = ValidateUploadedData(input, Convert.ToInt32(datarow["No"]));
                if (!ReferenceEquals(errorMessageDto, null))
                    uploadErrorMessagesList.Add(errorMessageDto);
                createAccountList.Add(input);
                accountsList.Add(Convert.ToInt32(datarow["No"]), input);
                //await CreateAccountUnit(input);
            }
            await CheckDuplicatesinExcel(createAccountList, uploadErrorMessagesList);
            if (uploadErrorMessagesList.Count > 1)
                return uploadErrorMessagesList;
            await ValidateDuplicateRecords(accountsList, uploadErrorMessagesList);
            if (uploadErrorMessagesList.Count < 1)
                await InsertUploadedAccounts(createAccountList);
            return uploadErrorMessagesList;
        }
        /// <summary>
        /// Validating Excel Duplicates
        /// </summary>
        /// <param name="accountsList"></param>
        /// <param name="uploadErrorMessagesList"></param>
        /// <returns></returns>
        private async Task CheckDuplicatesinExcel(List<CreateAccountUnitInput> accountsList, List<UploadErrorMessagesOutputDto> uploadErrorMessagesList)
        {
            UploadErrorMessagesOutputDto uploadErrorMessages;
            var duplicateaccountNumberList = (from p in accountsList
                                              group p by p.AccountNumber into g
                                              where g.Count() > 1
                                              select new { g.Key }).ToList();
            var duplicateAccountnumbers = string.Join(",", duplicateaccountNumberList.Select(p => p.Key).ToArray());
            if (duplicateaccountNumberList.Count > 0)
            {
                uploadErrorMessages = new UploadErrorMessagesOutputDto()
                {
                    ErrorMessage = L("DuplicateAccountNumbers") + duplicateAccountnumbers 
                };
                uploadErrorMessagesList.Add(uploadErrorMessages);
            }
            var duplicateDescriptionList = (from p in accountsList
                                            group p by p.Caption into g
                                            where g.Count() > 1
                                            select new { g.Key }).ToList();
            var duplicateAccountdescriptions = string.Join(",", duplicateDescriptionList.Select(p => p.Key).ToArray());
            if (duplicateDescriptionList.Count > 0)
            {
                uploadErrorMessages = new UploadErrorMessagesOutputDto()
                {
                    ErrorMessage = L("DuplicateDescriptions") + duplicateAccountdescriptions
                };
                uploadErrorMessagesList.Add(uploadErrorMessages);
            }
        }
        private async Task InsertUploadedAccounts(List<CreateAccountUnitInput> accountList)
        {
            foreach (var accountUnit in accountList)
            {
                await CreateAccountUnit(accountUnit);
            }
        }

        /// <summary>
        /// Validating Duplicate records of uploaded data
        /// </summary>
        /// <param name="accountsList"></param>
        /// <param name="uploadErrorMessagesList"></param>
        /// <returns></returns>
        private async Task ValidateDuplicateRecords(Dictionary<int, CreateAccountUnitInput> accountsList, List<UploadErrorMessagesOutputDto> uploadErrorMessagesList)
        {
            UploadErrorMessagesOutputDto uploadErrorMessages;
            var accounts = accountsList.ToList().Select(p => p.Value).ToList();
            var accountNumberList = string.Join(",", accounts.Select(p => p.AccountNumber).ToArray());
            var descriptionList = string.Join(",", accounts.Select(p => p.Caption).ToArray());

            var duplicateAccountList = await _accountUnitRepository.GetAll().Where(p => accountNumberList.Contains(p.AccountNumber)
             || descriptionList.Contains(p.Caption)).ToListAsync();

            if (duplicateAccountList.Count == 0)
                return;

            var duplicateAccounts1 = (from p in accountsList
                                      join p2 in duplicateAccountList on p.Value.Caption equals p2.Caption
                                      select new { Caption = p.Value.Caption, AccountNumber = p.Value.AccountNumber, RowNumber = p.Key }).ToList();
            var duplicateAccounts2 = (from p in accountsList
                                      join p2 in duplicateAccountList on p.Value.AccountNumber equals p2.AccountNumber
                                      select new { Caption = p.Value.Caption, AccountNumber = p.Value.AccountNumber, RowNumber = p.Key }).ToList();
           

            var duplicateAccounts = duplicateAccounts1.Union(duplicateAccounts2).ToList();

            foreach (var account in duplicateAccounts)
            {
                var error = new StringBuilder();
                error = error?.Append(!string.IsNullOrEmpty(account.AccountNumber) ? account.AccountNumber + L("DuplicateAccountNumber") : "");
                error = error?.Append(!string.IsNullOrEmpty(account.Caption) ? account.Caption + L("DuplicateDescription") : "");
                uploadErrorMessages = new UploadErrorMessagesOutputDto()
                {
                    ErrorMessage = error.ToString().TrimEnd(','),
                    RowNumber = account.RowNumber
                };
                uploadErrorMessagesList.Add(uploadErrorMessages);
            }
        }


        /// <summary>
        /// Validating Required and Length of uploaded Data
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        private UploadErrorMessagesOutputDto ValidateUploadedData(CreateAccountUnitInput input, int rowNumber)
        {
            UploadErrorMessagesOutputDto uploadErrorMessages = new UploadErrorMessagesOutputDto { RowNumber = rowNumber };
            DataValidator.CheckLength(input.AccountNumber.Length, AccountUnit.MaxCodeLength, L("AccountNumber"), uploadErrorMessages);
            DataValidator.CheckLength(input.Caption.Length, AccountUnit.MaxDesc, L("Description"), uploadErrorMessages);
            DataValidator.RequiredValidataion(input.AccountNumber, L("AccountNumber"), uploadErrorMessages);
            DataValidator.RequiredValidataion(input.Caption, L("Description"), uploadErrorMessages);
            if (string.IsNullOrEmpty(uploadErrorMessages.ErrorMessage))
                uploadErrorMessages = null;
            else
                uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage.TrimStart(',');
            return uploadErrorMessages;

        }
    }
}

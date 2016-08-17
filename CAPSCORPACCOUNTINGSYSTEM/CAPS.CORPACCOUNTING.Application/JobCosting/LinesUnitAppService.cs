using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using System.Text;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Uploads.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class LinesUnitAppService : CORPACCOUNTINGAppServiceBase, ILinesUnitAppService
    {
        private readonly AccountUnitManager _accountUnitManager;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<TypeOfAccountUnit, int> _typeOfAccountRepository;
        private readonly IRepository<TypeOfCurrencyUnit, short> _typeOfCurrencyRepository;
        private readonly IRepository<JobUnit, int> _jobRepository;
        private readonly IRepository<JobAccountUnit, long> _jobaccontRepository;
        private readonly AccountCache _accountcache;
        private readonly IAccountUnitAppService _accountUnitAppService;
        private readonly IJobUnitAppService _jobUnitAppService;
        private readonly IRepository<CoaUnit, int> _coaRepository;



        public LinesUnitAppService(AccountUnitManager accountUnitManager,
            IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<TypeOfAccountUnit, int> typeOfAccountRepository,
            IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyRepository,
            IRepository<JobUnit, int> jobRepository,
            IRepository<JobAccountUnit, long> jobaccontRepository, AccountCache accountcache,
            IAccountUnitAppService accountUnitAppService,
            IJobUnitAppService jobUnitAppService, IRepository<CoaUnit, int> coaRepository)
        {
            _accountUnitManager = accountUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _typeOfAccountRepository = typeOfAccountRepository;
            _typeOfCurrencyRepository = typeOfCurrencyRepository;
            _jobRepository = jobRepository;
            _jobaccontRepository = jobaccontRepository;
            _accountcache = accountcache;
            _accountUnitAppService = accountUnitAppService;
            _jobUnitAppService = jobUnitAppService;
            _coaRepository = coaRepository;
        }

        /// <summary>
        /// Get the Records by ProjectCoaId with paging sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<AccountUnitDto>> GetLinesByCoaId(GetAccountInput input)
        {
            var query =
                from au in _accountUnitRepository.GetAll()
                join typeOfAccount in _typeOfAccountRepository.GetAll() on au.TypeOfAccountId equals typeOfAccount.Id
                into accounts
                from accunit in accounts.DefaultIfEmpty()
                join currencytype in _typeOfCurrencyRepository.GetAll() on au.TypeOfCurrencyId equals currencytype.Id
                into currencyt
                from currency in currencyt.DefaultIfEmpty()
                join rollupacc in _accountUnitRepository.GetAll() on au.RollupAccountId equals rollupacc.Id
               into rollupaccount
                from rollupaccounts in rollupaccount.DefaultIfEmpty()
                join rollupdiv in _jobRepository.GetAll() on au.RollupJobId equals rollupdiv.Id
                into rollupAccount
                from rollupAccounts in rollupAccount.DefaultIfEmpty()
                select new
                {
                    Account = au,
                    TypeOfAccount = accunit.Description,
                    TypeOfCurrency = currency.Description,
                    RollUpAccountCaption = rollupaccounts.AccountNumber,
                    RollUpDivision = rollupAccounts.JobNumber
                };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item => item.Account.ChartOfAccountId == input.CoaId)
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.Account.OrganizationUnitId == input.OrganizationUnitId);

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
                     dto.RollUpAccountCaption = item.RollUpAccountCaption;
                     dto.RollUpDivision = item.RollUpDivision;
                     return dto;
                 }).ToList());
        }

        /// <summary>
        /// Crating the Lines
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<AccountUnitDto> CreateLineUnit(CreateAccountUnitInput input)
        {
            var accountUnit = input.MapTo<AccountUnit>();
            accountUnit.ParentId = input.ParentId != 0 ? input.ParentId : null;
            accountUnit.OrganizationUnitId = input.OrganizationUnitId;
            await _accountUnitManager.CreateAsync(accountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return accountUnit.MapTo<AccountUnitDto>();
        }
        /// <summary>
        /// Updating the Line
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AccountUnitDto> UpdateLineUnit(UpdateAccountUnitInput input)
        {
            var accountUnit = await _accountUnitRepository.GetAsync(input.AccountId);

            #region Setting the values to be updated

            accountUnit.AccountNumber = input.AccountNumber;
            accountUnit.Caption = input.Caption;
            accountUnit.ChartOfAccountId = input.ChartOfAccountId;
            accountUnit.ParentId = input.ParentId != 0 ? input.ParentId : null;
            accountUnit.RollupAccountId = input.RollupAccountId;
            accountUnit.TypeOfCurrencyId = input.TypeOfCurrencyId;
            accountUnit.TypeofConsolidationId = input.TypeofConsolidationId;
            accountUnit.RollupJobId = input.RollupJobId;
            accountUnit.TypeOfAccountId = input.TypeOfAccountId;
            #endregion

            await _accountUnitManager.UpdateAsync(accountUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return accountUnit.MapTo<AccountUnitDto>();
        }
        /// <summary>
        /// Deleting the Line by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task DeleteLineUnit(IdInput<long> input)
        {
            //deleting the jobaccounts based on accountId
            await _jobaccontRepository.DeleteAsync(p => p.AccountId == input.Id);
            await _accountUnitManager.DeleteAsync(input.Id);
        }



        /// <summary>
        /// Importing the Lines From Excel
        /// </summary>
        /// <param name="linesTable"></param>
        /// <param name="coaId"></param>
        /// <returns></returns>
        public async Task<List<UploadErrorMessagesOutputDto>> ImportLines(DataTable linesTable, int coaId)
        {
            var classificationlist = await _accountUnitAppService.GetTypeOfAccountList();
            var currencylist = await _accountUnitAppService.GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
            var rollupdivisionList = (await _jobUnitAppService.GetDivisionList(new AutoSearchInput() { })).ConvertAll(u => new NameValueDto()
            {
                Value = u.JobId.ToString(),
                Name = u.JobNumber
            });
            var rollUpAccountList = (await _accountUnitAppService.GetRollupAccountsList(new AutoSearchInput() { Id = coaId })).ConvertAll(u => new NameValueDto()
            {
                Value = u.AccountId.ToString(),
                Name = u.AccountNumber
            });
            List<UploadErrorMessagesOutputDto> uploadErrorMessagesList = new List<UploadErrorMessagesOutputDto>();
            List<CreateAccountUnitInput> createAccountList = new List<CreateAccountUnitInput>();
            Dictionary<int, CreateAccountUnitInput> linesTList = new Dictionary<int, CreateAccountUnitInput>();
            await CheckDuplicatesinExcel(uploadErrorMessagesList, linesTable);
            if (uploadErrorMessagesList.Count > 0)
                return uploadErrorMessagesList;
            var coa = _coaRepository.FirstOrDefault(p => p.Id == coaId);

            foreach (DataRow datarow in linesTable.Rows)
            {
                int? typeofaccount = null;
                short? currencyId = null;
                long? rollUpAccountId = null;
                TypeofConsolidation? consolidationId = null;
                int? rollupDivisionId = null;
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

                var rollupdivision = rollupdivisionList.FirstOrDefault(p => p.Name == datarow[L("RollUpDivision")].ToString());
                if (rollupdivision != null)
                {
                    rollupDivisionId = Convert.ToInt32(rollupdivision.Value);
                }
                var rollupAccount = rollUpAccountList.FirstOrDefault(p => p.Name == datarow[L("RollUpAccount")].ToString());
                if (rollupAccount != null)
                {
                    rollUpAccountId = Convert.ToInt32(rollupAccount.Value);
                }
                var input = new CreateAccountUnitInput
                {
                    AccountNumber = datarow[L("LineNumber")].ToString(),
                    Caption = datarow[L("Caption")].ToString(),
                    TypeOfAccountId = typeofaccount,
                    TypeOfCurrencyId = currencyId,
                    TypeofConsolidationId = consolidationId,
                    RollupJobId = rollupDivisionId,
                    RollupAccountId = rollUpAccountId,
                    IsEnterable = Helper.ConvertToBoolean(datarow[L("JournalsAllowed")].ToString()),
                    ChartOfAccountId = coaId
                };
                UploadErrorMessagesOutputDto errorMessageDto = ValidateUploadedData(input, Convert.ToInt32(datarow["No"]), coa.IsNumeric);
                if (!ReferenceEquals(errorMessageDto, null))
                    uploadErrorMessagesList.Add(errorMessageDto);
                createAccountList.Add(input);
                linesTList.Add(Convert.ToInt32(datarow["No"]), input);
            }
          
            await ValidateDuplicateRecords(linesTList, uploadErrorMessagesList);
            if (uploadErrorMessagesList.Count < 1)
                await InsertUploadedAccounts(createAccountList);
            return uploadErrorMessagesList;
        }
        /// <summary>
        /// Validating Excel Duplicates
        /// </summary>
        /// <param name="uploadErrorMessagesList"></param>
        /// <param name="linesTable"></param>
        /// <returns></returns>
        private async Task CheckDuplicatesinExcel( List<UploadErrorMessagesOutputDto> uploadErrorMessagesList, DataTable linesTable)
        {
            var duplicateAccountDescriptions = linesTable.AsEnumerable()
                .GroupBy(dr => dr.Field<string>(L("Caption")))
                .Where(g => g.Count() > 1)
                .Select(g => new
                {
                    Description = g.Key,
                    RowNumber = g.Max(x => x["No"])
                }).ToList();
            var duplicateAccountLineNumbers = linesTable.AsEnumerable()
              .GroupBy(dr => dr.Field<string>(L("LineNumber")))
              .Where(g => g.Count() > 1)
              .Select(g => new
              {
                  AccountNumber = g.Key,
                  RowNumber = g.Max(x => x["No"])
              }).ToList();
            UploadErrorMessagesOutputDto uploadErrorMessages;
            if (duplicateAccountLineNumbers.Count > 0)
            {
                foreach (var duplicateAccounts in duplicateAccountLineNumbers)
                {
                    uploadErrorMessages = new UploadErrorMessagesOutputDto()
                    {
                        RowNumber = Convert.ToInt32(duplicateAccounts.RowNumber),
                        ErrorMessage = duplicateAccounts.AccountNumber + " " + L("DuplicateLineNumber").TrimEnd(',')
                    };
                    uploadErrorMessagesList.Add(uploadErrorMessages);
                }
            }

            if (duplicateAccountDescriptions.Count > 0)
            {
                foreach (var duplicateAccounts in duplicateAccountDescriptions)
                {
                    uploadErrorMessages = new UploadErrorMessagesOutputDto()
                    {
                        RowNumber = Convert.ToInt32(duplicateAccounts.RowNumber),
                        ErrorMessage = duplicateAccounts.Description + " " + L("DuplicateCaption").TrimEnd(',')
                    };
                    uploadErrorMessagesList.Add(uploadErrorMessages);
                }
            }
        }
        private async Task InsertUploadedAccounts(List<CreateAccountUnitInput> accountList)
        {
            foreach (var accountUnit in accountList)
            {
                await CreateLineUnit(accountUnit);
            }
        }

        /// <summary>
        /// Validating Duplicate records of uploaded data
        /// </summary>
        /// <param name="linesTList"></param>
        /// <param name="uploadErrorMessagesList"></param>
        /// <returns></returns>
        private async Task ValidateDuplicateRecords(Dictionary<int, CreateAccountUnitInput> linesTList, List<UploadErrorMessagesOutputDto> uploadErrorMessagesList)
        {
            var accounts = linesTList.ToList().Select(p => p.Value).ToList();
            var lineNumberList = string.Join(",", accounts.Select(p => p.AccountNumber).ToArray());
            var captionList = string.Join(",", accounts.Select(p => p.Caption).ToArray());

            var duplicateAccountItems = await _accountcache.GetAccountCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(AbpSession.TenantId)));

            var duplicateAccountList = duplicateAccountItems.Where(p => lineNumberList.Contains(p.AccountNumber)
               || captionList.Contains(p.Caption)).ToList();

            if (duplicateAccountList.Count == 0)
                return;

            var duplicateAccounts1 = (from p in linesTList
                                      join p2 in duplicateAccountList on p.Value.Caption equals p2.Caption
                                      select new { Caption = p.Value.Caption, AccountNumber = p.Value.AccountNumber, RowNumber = p.Key }).ToList();
            var duplicateAccounts2 = (from p in linesTList
                                      join p2 in duplicateAccountList on p.Value.AccountNumber equals p2.AccountNumber
                                      select new { Caption = p.Value.Caption, AccountNumber = p.Value.AccountNumber, RowNumber = p.Key }).ToList();


            var duplicateAccounts = duplicateAccounts1.Union(duplicateAccounts2).ToList();

            foreach (var account in duplicateAccounts)
            {
                var error = new StringBuilder();
                error = error?.Append(!string.IsNullOrEmpty(account.AccountNumber) ? account.AccountNumber + L("DuplicateLineNumber") : "");
                error = error?.Append(!string.IsNullOrEmpty(account.Caption) ? account.Caption + L("DuplicateCaption") : "");
                var uploadErrorMessages = new UploadErrorMessagesOutputDto()
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
        ///  /// <param name="isNumeric"></param>
        /// <returns></returns>
        private UploadErrorMessagesOutputDto ValidateUploadedData(CreateAccountUnitInput input, int rowNumber,bool isNumeric)
        {
            UploadErrorMessagesOutputDto uploadErrorMessages = new UploadErrorMessagesOutputDto { RowNumber = rowNumber };
            DataValidator.CheckLength(input.AccountNumber.Length, AccountUnit.MaxCodeLength, L("LineNumber"), uploadErrorMessages);
            NumericValidation(input.AccountNumber, uploadErrorMessages, isNumeric);
            DataValidator.CheckLength(input.Caption.Length, AccountUnit.MaxDesc, L("Caption"), uploadErrorMessages);
            DataValidator.RequiredValidataion(input.AccountNumber, L("LineNumber"), uploadErrorMessages);
            DataValidator.RequiredValidataion(input.Caption, L("LineNumber"), uploadErrorMessages);
            if (string.IsNullOrEmpty(uploadErrorMessages.ErrorMessage))
                uploadErrorMessages = null;
            else
                uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage.TrimStart(',');
            return uploadErrorMessages;

        }
        /// <summary>
        /// Validating LineNumber as Numeric 
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="uploadErrorMessages"></param>
        /// <param name="isNumeric"></param>
        private void NumericValidation(string lineNumber, UploadErrorMessagesOutputDto uploadErrorMessages, bool isNumeric)
        {
            long accountNum;
            //In Coa IsNumberic is true, LineNumber shold be Numeric
            if (isNumeric && !long.TryParse(lineNumber, out accountNum))
                uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage + " " + lineNumber +
                                                   L("ShouldbeNumberic").TrimStart(',').TrimEnd(',');
        }
    }
}


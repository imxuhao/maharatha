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
            var uploadErrorMessagesList = new List<UploadErrorMessagesOutputDto>();
            var coa = _coaRepository.FirstOrDefault(p => p.Id == coaId);

            var lineList = (from tbl in linesTable.AsEnumerable()
                            join classification in classificationlist on tbl.Field<string>(L("Classification")) equals classification.Name
                              into classifications
                            from classificationunit in classifications.DefaultIfEmpty()
                            join currency in currencylist on tbl.Field<string>(L("Currency")) equals currency.Name
                                     into currencies
                            from currencyunit in currencies.DefaultIfEmpty()
                            join consolidation in consolidationList on tbl.Field<string>(L("Consolidation")) equals consolidation.Name
                                     into consolidations
                            from consolidationunit in consolidations.DefaultIfEmpty()

                            join rollupdivision in rollupdivisionList on tbl.Field<string>(L("RollUpDivision")) equals rollupdivision.Name
                                     into rollupdivisions
                            from rollupdivisionunit in rollupdivisions.DefaultIfEmpty()

                            join rollUpAccount in rollUpAccountList on tbl.Field<string>(L("RollUpAccount")) equals rollUpAccount.Name
                                     into rollUpAccounts
                            from rollUpAccountunit in rollUpAccounts.DefaultIfEmpty()
                            select new CreateAccountUnitInput
                            {
                                No = Convert.ToInt32(tbl.Field<string>(L("No"))),
                                Caption = string.IsNullOrEmpty(tbl.Field<string>(L("Caption"))) ? string.Empty : tbl.Field<string>(L("Caption")),
                                TypeOfCurrencyId = ReferenceEquals(currencyunit, null) ? (short?)null : Convert.ToInt16(currencyunit.Value),
                                TypeofConsolidationId = ReferenceEquals(consolidationunit, null) ? (TypeofConsolidation?)null : (TypeofConsolidation)Convert.ToInt32(consolidationunit.Value),
                                RollupJobId = ReferenceEquals(rollupdivisionunit, null) ? (int?)null : Convert.ToInt32(rollupdivisionunit.Value),
                                RollupAccountId = ReferenceEquals(rollUpAccountunit, null) ? (long?)null : Convert.ToInt64(rollUpAccountunit.Value),
                                IsEnterable = !string.IsNullOrEmpty(tbl.Field<string>(L("JournalsAllowed"))) && Convert.ToBoolean(tbl.Field<string>(L("JournalsAllowed"))),
                                ChartOfAccountId = coaId,
                                AccountNumber = string.IsNullOrEmpty(tbl.Field<string>(L("LineNumber"))) ? string.Empty : tbl.Field<string>(L("LineNumber")),
                                TypeOfAccountId = ReferenceEquals(classificationunit, null) ? (int?)null : Convert.ToInt32(classificationunit.Value)
                            }).ToList();

            CheckDuplicatesinExcel(uploadErrorMessagesList, lineList);
            //Check RequiredField and MAxLenght Validations
            ValidateUploadedData(lineList, coa.IsNumeric, uploadErrorMessagesList);
            await ValidateDuplicateRecords(lineList, uploadErrorMessagesList);
            var errorList =
                (from i in uploadErrorMessagesList
                 group i by i.RowNumber
                    into g
                 select new UploadErrorMessagesOutputDto
                 {
                     RowNumber = L("StartBold") + L("StartColor") + g.Key + L("EndColor") + L("EndBold"),
                     ErrorMessage = string.Join(",", g.Select(kvp => kvp.ErrorMessage)).TrimStart(',')
                 }).OrderBy(p => p.RowNumber).ToList();


            if (errorList.Count < 1)
                await InsertUploadedLines(lineList);
            return uploadErrorMessagesList;
        }
        /// <summary>
        /// Validating Excel Duplicates
        /// </summary>
        /// <param name="uploadErrorMessagesList"></param>
        /// <param name="linesList"></param>
        /// <returns></returns>
        private void CheckDuplicatesinExcel(List<UploadErrorMessagesOutputDto> uploadErrorMessagesList, List<CreateAccountUnitInput> linesList)
        {
            var duplicateAccountDescriptions = linesList
                .GroupBy(dr => dr.Caption)
                .Where(g => g.Count() > 1)
                .Select(g => new
                {
                    Description = g.Key
                }).ToList();
            var duplicateAccountLineNumbers = linesList
              .GroupBy(dr => dr.AccountNumber)
              .Where(g => g.Count() > 1)
              .Select(g => new
              {
                  AccountNumber = g.Key
              }).ToList();
            if (duplicateAccountLineNumbers.Count > 0 && duplicateAccountDescriptions.Count > 0)
            {

                var duliplcateAccotcaptionList = (from accdesc in duplicateAccountDescriptions
                                                  join account in linesList on accdesc.Description equals account.Caption
                                                  select new
                                                  {
                                                      AccountNumber = string.Empty,
                                                      Description = accdesc.Description,
                                                      No = account.No

                                                  }).ToList();

                var duliplcateAccotNumberList = (from accaccnum in duplicateAccountLineNumbers
                                                 join account in linesList on accaccnum.AccountNumber equals account.AccountNumber
                                                 select new
                                                 {
                                                     AccountNumber = accaccnum.AccountNumber,
                                                     Description = string.Empty,
                                                     No = account.No
                                                 }).ToList();
                var duplicates = duliplcateAccotcaptionList.Union(duliplcateAccotNumberList).ToList();

                if (duplicates.Count > 0)
                {
                    foreach (var duplicateAccounts in duplicates)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        if (!string.IsNullOrEmpty(duplicateAccounts.AccountNumber))
                            stringBuilder.Append(L("DuplicateLineNumber") + duplicateAccounts.AccountNumber +
                                                 L("InExcel"));
                        if (!string.IsNullOrEmpty(duplicateAccounts.Description))
                            stringBuilder.Append(L("DuplicateCaption") + duplicateAccounts.Description +
                                                 L("InExcel"));

                        var uploadErrorMessages = new UploadErrorMessagesOutputDto()
                        {
                            RowNumber = Convert.ToString(duplicateAccounts.No),
                            ErrorMessage = stringBuilder.ToString().TrimStart(',')
                        };
                        uploadErrorMessagesList.Add(uploadErrorMessages);
                    }
                }
            }

        }
        private async Task InsertUploadedLines(List<CreateAccountUnitInput> accountList)
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
        private async Task ValidateDuplicateRecords(List<CreateAccountUnitInput> linesTList, List<UploadErrorMessagesOutputDto> uploadErrorMessagesList)
        {
            // var accounts = linesTList.ToList().Select(p => p.Value).ToList();
            var lineNumberList = string.Join(",", linesTList.Select(p => p.AccountNumber).ToArray());
            var captionList = string.Join(",", linesTList.Select(p => p.Caption).ToArray());

            var duplicateAccountItems = await _accountcache.GetAccountCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(AbpSession.TenantId)));
            var filters = new List<Filters>();
            //constructing filter for AccountNumbers
            if (!string.IsNullOrEmpty(lineNumberList))
            {
                var accfilter = new Filters()
                {
                    Property = "AccountNumber",
                    Comparator = 6,//In Operator
                    SearchTerm = lineNumberList,
                    DataType = DataTypes.Text

                };
                filters.Add(accfilter);
            }
            //constructing filter for Caption
            if (!string.IsNullOrEmpty(captionList))
            {
                var accdescfilter = new Filters()
                {
                    Property = "Caption",
                    Comparator = 6, //In Operator
                    SearchTerm = captionList,
                    DataType = DataTypes.Text

                };
                filters.Add(accdescfilter);
            }
            var filterCondition = ExpressionBuilder.GetExpression<AccountCacheItem>(filters, SearchPattern.Or).Compile();

            //apply filetr In for AccountNumber and Description.
            var duplicateLineList = duplicateAccountItems.Where(filterCondition).ToList();



            var duplicateAccountcapstions = (from p in linesTList
                                             join p2 in duplicateLineList on p.Caption equals p2.Caption
                                             select new { Caption = p.Caption, AccountNumber = p.AccountNumber, RowNumber = p.No }).ToList();
            var duplicateAccountNumbers = (from p in linesTList
                                           join p2 in duplicateLineList on p.AccountNumber equals p2.AccountNumber
                                           select new { Caption = p.Caption, AccountNumber = p.AccountNumber, RowNumber = p.No }).ToList();


            var duplicateAccounts = duplicateAccountcapstions.Union(duplicateAccountNumbers).ToList();

            foreach (var account in duplicateAccounts)
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(account.AccountNumber))
                    stringBuilder.Append(L("DuplicateLineNumber") + L("StartBold")+ account.AccountNumber+ L("EndBold") + L("InDb"));
                if (!string.IsNullOrEmpty(account.Caption))
                    stringBuilder.Append(L("DuplicateCaption") + L("StartBold") + account.Caption + L("EndBold") + L("InDb"));
                var uploadErrorMessages = new UploadErrorMessagesOutputDto()
                {
                    ErrorMessage = stringBuilder.ToString().TrimEnd(',').TrimStart(','),
                    RowNumber = account.RowNumber.ToString()
                };
                uploadErrorMessagesList.Add(uploadErrorMessages);
            }
        }


        /// <summary>
        /// Validating Required and Length of uploaded Data
        /// </summary>
        /// <param name="inputUnits"></param>
        /// <param name="isNumeric"></param>
        /// <param name="uploadErrorMessageList"></param>
        /// <returns></returns>
        private void ValidateUploadedData(List<CreateAccountUnitInput> inputUnits, bool isNumeric, List<UploadErrorMessagesOutputDto> uploadErrorMessageList)
        {
            foreach (var input in inputUnits)
            {
                var uploadErrorMessages = new UploadErrorMessagesOutputDto { RowNumber = input.No.ToString() };
                DataValidator.CheckLength(input.AccountNumber.Length, AccountUnit.MaxCodeLength, L("LineNumber"), uploadErrorMessages);
                NumericValidation(input.AccountNumber, uploadErrorMessages, isNumeric);
                DataValidator.CheckLength(input.Caption.Length, AccountUnit.MaxDesc, L("Caption"), uploadErrorMessages);
                DataValidator.RequiredValidataion(input.AccountNumber, L("LineNumber"), uploadErrorMessages);
                DataValidator.RequiredValidataion(input.Caption, L("Caption"), uploadErrorMessages);
                if (!string.IsNullOrEmpty(uploadErrorMessages.ErrorMessage))
                {
                    uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage.TrimStart(',');
                    uploadErrorMessageList.Add(uploadErrorMessages);
                }
            }
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
            if (!string.IsNullOrEmpty(lineNumber) && isNumeric && !long.TryParse(lineNumber, out accountNum))
                uploadErrorMessages.ErrorMessage = uploadErrorMessages.ErrorMessage + " " + lineNumber +
                                                   L("ShouldbeNumberic").TrimStart(',').TrimEnd(',');
        }
    }
}


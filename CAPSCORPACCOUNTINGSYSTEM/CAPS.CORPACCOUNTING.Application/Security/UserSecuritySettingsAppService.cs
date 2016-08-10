using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.ChargeEntry;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Organization;
using CAPS.CORPACCOUNTING.Security.Dto;
using CAPS.CORPACCOUNTING.Sessions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security
{

    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
    public class UserSecuritySettingsAppService : CORPACCOUNTINGAppServiceBase, IUserSecuritySettingsAppService
    {

        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IAccountCache _accountCache;


        private readonly IRepository<JobUnit, int> _jobUnitRepository;
        private readonly IDivisionCache _divisionCache;


        private readonly IRepository<BankAccountUnit, long> _bankAccountUnitRepository;
        private readonly IBankAccountCache _bankAccountCache;


        private readonly IRepository<ChargeEntryDocumentUnit, long> _creditCardUnitRepository;

        private readonly CustomAppSession _customAppSession;
        private readonly UserManager _userManager;


        private readonly IRepository<OrganizationExtended, long> _organizationExtendedRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;

        private readonly OrganizationExtendedUnitManager _organizationExtendedUnitManager;

        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;

        private readonly OrganizationUnitManager _organizationUnitManager;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountUnitRepository"></param>
        /// <param name="accountCache"></param>
        /// <param name="customAppSession"></param>
        /// <param name="jobUnitRepository"></param>
        /// <param name="divisionCache"></param>
        /// <param name="bankAccountUnitRepository"></param>
        /// <param name="bankAccountCache"></param>
        /// <param name="creditCardUnitRepository"></param>
        /// <param name="organizationExtendedRepository"></param>
        /// <param name="userOrganizationUnitRepository"></param>
        /// <param name="organizationExtendedUnitManager"></param>
        /// <param name="userManager"></param>
        /// <param name="organizationUnitRepository"></param>
        /// <param name="organizationUnitManager"></param>
        public UserSecuritySettingsAppService(
        IRepository<AccountUnit, long> accountUnitRepository,
        IAccountCache accountCache,
        CustomAppSession customAppSession,
        IRepository<JobUnit, int> jobUnitRepository,
        IDivisionCache divisionCache,
        IRepository<BankAccountUnit, long> bankAccountUnitRepository,
        IBankAccountCache bankAccountCache,
        IRepository<ChargeEntryDocumentUnit, long> creditCardUnitRepository,
        IRepository<OrganizationExtended, long> organizationExtendedRepository,
        IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
        OrganizationExtendedUnitManager organizationExtendedUnitManager,
        UserManager userManager,
        IRepository<OrganizationUnit, long> organizationUnitRepository,
        OrganizationUnitManager organizationUnitManager
        )
        {
            _accountUnitRepository = accountUnitRepository;
            _accountCache = accountCache;
            _customAppSession = customAppSession;
            _jobUnitRepository = jobUnitRepository;
            _divisionCache = divisionCache;
            _bankAccountUnitRepository = bankAccountUnitRepository;
            _bankAccountCache = bankAccountCache;
            _creditCardUnitRepository = creditCardUnitRepository;
            _organizationExtendedRepository = organizationExtendedRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationExtendedUnitManager = organizationExtendedUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userManager = userManager;
            _organizationUnitManager = organizationUnitManager;
        }


        #region Account/Lines Access List
        /// <summary>
        /// Create or Update Accounts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateorUpdateAccountAccessList(UserSecuritySettingsInputUnit input)
        {

            var tenantId = Convert.ToInt32(_customAppSession.TenantId);
            foreach (var userId in input.UserIdList)
            {

                var user = await _userManager.GetUserByIdAsync(userId);
                var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, EntityClassification.Account);

                if (!ReferenceEquals(input.AccountAccessList, null))
                {
                    input.AccountAccessList.ForEach(u => u.UserId = userId);

                    //assign Account restrictions to User
                    foreach (var account in input.AccountAccessList)
                    {
                        var organization = await _organizationUnitRepository.FirstOrDefaultAsync(u => u.Id == account.OrganizationUnitId);
                        if (organization != null)
                        {
                            if (!await _userOrganizationUnitRepository.GetAll().AnyAsync(u => u.OrganizationUnitId == organization.Id && u.UserId == account.UserId))
                                await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, organization.Id));
                        }
                        else
                        {
                            var orgUnit = new OrganizationExtended(tenantId, account.AccountNumber, entityClassificationId: EntityClassification.Account);
                            orgUnit.Code = await _organizationUnitManager.GetNextChildCodeAsync(null);
                            var orgId = await _organizationUnitRepository.InsertAndGetIdAsync(orgUnit);
                            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, orgId));
                            var accountDetails = await _accountUnitRepository.FirstOrDefaultAsync(u => u.Id == account.AccountId);
                            accountDetails.OrganizationUnitId = orgId;
                            await _accountUnitRepository.UpdateAsync(accountDetails);
                        }
                    }

                    //delete Account restrictions from User
                    var deleteAccountList = organizationUnits.Where(p => !input.AccountAccessList.Any(p2 => p2.OrganizationUnitId == p.Id)).ToList();
                    foreach (var UserOrg in deleteAccountList)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }  //delete Account restrictions from User
                else
                {
                    foreach (var UserOrg in organizationUnits)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }
            }
        }


        /// <summary>
        /// Create or Update Lines
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateorUpdateLineAccessList(UserSecuritySettingsInputUnit input)
        {

            var tenantId = Convert.ToInt32(_customAppSession.TenantId);
            foreach (var userId in input.UserIdList)
            {

                var user = await _userManager.GetUserByIdAsync(userId);
                var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, EntityClassification.Line);

                if (!ReferenceEquals(input.AccountAccessList, null))
                {
                    input.AccountAccessList.ForEach(u => u.UserId = userId);

                    //assign Account restrictions to User
                    foreach (var account in input.AccountAccessList)
                    {
                        var organization = await _organizationUnitRepository.FirstOrDefaultAsync(u => u.Id == account.OrganizationUnitId);
                        if (organization != null)
                        {
                            if (!await _userOrganizationUnitRepository.GetAll().AnyAsync(u => u.OrganizationUnitId == organization.Id && u.UserId == account.UserId))
                                await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, organization.Id));
                        }
                        else
                        {
                            var orgUnit = new OrganizationExtended(tenantId, account.AccountNumber, entityClassificationId: EntityClassification.Line);
                            orgUnit.Code = await _organizationUnitManager.GetNextChildCodeAsync(null);
                            var orgId = await _organizationUnitRepository.InsertAndGetIdAsync(orgUnit);
                            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, orgId));
                            var accountDetails = await _accountUnitRepository.FirstOrDefaultAsync(u => u.Id == account.AccountId);
                            accountDetails.OrganizationUnitId = orgId;
                            await _accountUnitRepository.UpdateAsync(accountDetails);
                        }
                    }

                    //delete Account restrictions from User
                    var deleteAccountList = organizationUnits.Where(p => !input.AccountAccessList.Any(p2 => p2.OrganizationUnitId == p.Id)).ToList();
                    foreach (var UserOrg in deleteAccountList)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }  //delete Account restrictions from User
                else
                {
                    foreach (var UserOrg in organizationUnits)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Get Accounts/Lines AccessList By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<AccountAccessListUnitDto>> GetAccountAccessList(GetUserSecuritySettingsInputUnit input)
        {
            List<AccountCacheItem> accountCacheItems = new List<AccountCacheItem>();

            AutoSearchInput cacheInput = new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId };
            var accountCache = await _accountCache.GetAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), cacheInput);

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);
            var organizationUnitIds = organizationUnits.Select(ou => ou.Id);
            var strOrgIds = string.Join(",", organizationUnitIds.ToArray());

            if (!string.IsNullOrEmpty(strOrgIds))
            {
                if (ReferenceEquals(input.Filters, null))
                    input.Filters = new List<Filters>();
                var orgfilter = new Filters()
                {
                    Property = "OrganizationUnitId",
                    Comparator = 6,//In Operator
                    SearchTerm = strOrgIds,
                    DataType = DataTypes.Text

                };
                input.Filters.Add(orgfilter);
            }

            if (!ReferenceEquals(input.Filters, null))
            {
                var filterCondition = ExpressionBuilder.GetExpression<AccountCacheItem>(input.Filters).Compile();
                accountCacheItems = accountCache.ToList().Where(u => u.ChartOfAccountId == input.ChartOfAccountId).Where(filterCondition).ToList();
            }

            return accountCacheItems.Select(item =>
            {
                var dto = new AccountAccessListUnitDto();
                dto.AccountNumber = item.AccountNumber;
                dto.Caption = item.Caption;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                dto.AccountId = item.AccountId;
                return dto;
            }).ToList();

        }


        /// <summary>
        /// Get Accounts/Lines List which is not in AccountAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<AccountAccessListUnitDto>> GetAccountList(GetUserSecuritySettingsInputUnit input)
        {


            List<AccountCacheItem> accountCacheItems = new List<AccountCacheItem>();
            AutoSearchInput cacheInput = new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId };

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);
            var accountCache = await _accountCache.GetAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), cacheInput);


            if (!ReferenceEquals(input.Filters, null))
            {
                Func<AccountCacheItem, bool> multiRangeExp = null;
                Func<AccountCacheItem, bool> filterCondition = null;
                var multiRageFilters = input.Filters.Where(u => u.IsMultiRange == true).ToList();
                if (multiRageFilters.Count!=0)
                {
                    multiRangeExp = ExpressionBuilder.GetExpression<AccountCacheItem>(Helper.GetMultiRangeFilters(multiRageFilters), SearchPattern.Or).Compile();
                    input.Filters.RemoveAll(u => u.IsMultiRange == true);
                }
                var otherFilters = input.Filters.Where(u => u.IsMultiRange == false).ToList();
                if (otherFilters.Count!=0)
                    filterCondition = ExpressionBuilder.GetExpression<AccountCacheItem>(otherFilters).Compile();
                accountCacheItems = accountCache.ToList().WhereIf(multiRageFilters.Count != 0, multiRangeExp)
                    .WhereIf(otherFilters.Count != 0, filterCondition)
                    .Where(p => !organizationUnits.Any(p2 => p2.Id == p.OrganizationUnitId)).ToList();
            }
            else
            {
                accountCacheItems = accountCache.ToList().Where(u => u.ChartOfAccountId == input.ChartOfAccountId)
                    .Where(p => !organizationUnits.Any(p2 => p2.Id == p.OrganizationUnitId)).ToList();
            }


            return accountCacheItems.Select(item =>
            {
                var dto = new AccountAccessListUnitDto();
                dto.AccountId = item.AccountId;
                dto.AccountNumber = item.AccountNumber;
                dto.Caption = item.Caption;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                return dto;
            }).ToList();
        }

        #endregion


        #region Project/Divison Access List
        /// <summary>
        /// Create or Update Projects
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateorUpdateProjectAccessList(UserSecuritySettingsInputUnit input)
        {

            var tenantId = Convert.ToInt32(_customAppSession.TenantId);
            foreach (var userId in input.UserIdList)
            {

                var user = await _userManager.GetUserByIdAsync(userId);
                var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, EntityClassification.Project);

                if (!ReferenceEquals(input.ProjectAccessList, null))
                {
                    input.ProjectAccessList.ForEach(u => u.UserId = userId);

                    //assign Project restrictions to User
                    foreach (var project in input.ProjectAccessList)
                    {
                        var organization = await _organizationUnitRepository.FirstOrDefaultAsync(u => u.Id == project.OrganizationUnitId);
                        if (organization != null)
                        {
                            if (!await _userOrganizationUnitRepository.GetAll().AnyAsync(u => u.OrganizationUnitId == organization.Id && u.UserId == project.UserId))
                                await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, organization.Id));
                        }
                        else
                        {
                            var orgUnit = new OrganizationExtended(tenantId, project.JobNumber, entityClassificationId: EntityClassification.Project);
                            orgUnit.Code = await _organizationUnitManager.GetNextChildCodeAsync(null);
                            var orgId = await _organizationUnitRepository.InsertAndGetIdAsync(orgUnit);
                            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, orgId));
                            var projectDetails = await _jobUnitRepository.FirstOrDefaultAsync(u => u.Id == project.JobId);
                            projectDetails.OrganizationUnitId = orgId;
                            await _jobUnitRepository.UpdateAsync(projectDetails);
                        }
                    }

                    //delete Project restrictions from User
                    var deleteProjectList = organizationUnits.Where(p => !input.ProjectAccessList.Any(p2 => p2.OrganizationUnitId == p.Id)).ToList();
                    foreach (var UserOrg in deleteProjectList)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }  //delete Project restrictions from User
                else
                {
                    foreach (var UserOrg in organizationUnits)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }
            }
        }


        /// <summary>
        /// Create or Update Divisions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateorUpdateDivisionAccessList(UserSecuritySettingsInputUnit input)
        {

            var tenantId = Convert.ToInt32(_customAppSession.TenantId);
            foreach (var userId in input.UserIdList)
            {

                var user = await _userManager.GetUserByIdAsync(userId);
                var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, EntityClassification.Division);

                if (!ReferenceEquals(input.ProjectAccessList, null))
                {
                    input.ProjectAccessList.ForEach(u => u.UserId = userId);

                    //assign Division restrictions to User
                    foreach (var project in input.ProjectAccessList)
                    {
                        var organization = await _organizationUnitRepository.FirstOrDefaultAsync(u => u.Id == project.OrganizationUnitId);
                        if (organization != null)
                        {
                            if (!await _userOrganizationUnitRepository.GetAll().AnyAsync(u => u.OrganizationUnitId == organization.Id && u.UserId == project.UserId))
                                await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, organization.Id));
                        }
                        else
                        {
                            var orgUnit = new OrganizationExtended(tenantId, project.JobNumber, entityClassificationId: EntityClassification.Division);
                            orgUnit.Code = await _organizationUnitManager.GetNextChildCodeAsync(null);
                            var orgId = await _organizationUnitRepository.InsertAndGetIdAsync(orgUnit);
                            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, orgId));
                            var projectDetails = await _jobUnitRepository.FirstOrDefaultAsync(u => u.Id == project.JobId);
                            projectDetails.OrganizationUnitId = orgId;
                            await _jobUnitRepository.UpdateAsync(projectDetails);
                        }
                    }

                    //delete Divisions restrictions from User
                    var deleteProjectList = organizationUnits.Where(p => !input.ProjectAccessList.Any(p2 => p2.OrganizationUnitId == p.Id)).ToList();
                    foreach (var UserOrg in deleteProjectList)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }  //delete Divisions restrictions from User
                else
                {
                    foreach (var UserOrg in organizationUnits)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Get Project/Division AccessList By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ProjectAccessListUnitDto>> GetProjectAccessList(GetUserSecuritySettingsInputUnit input)
        {
            List<DivisionCacheItem> divisionCacheItems = new List<DivisionCacheItem>();

            AutoSearchInput cacheInput = new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId };
            var divisionCache = await _divisionCache.GetDivisionCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), cacheInput);

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);
            var organizationUnitIds = organizationUnits.Select(ou => ou.Id);
            var strOrgIds = string.Join(",", organizationUnitIds.ToArray());

            if (!string.IsNullOrEmpty(strOrgIds))
            {
                if (ReferenceEquals(input.Filters, null))
                    input.Filters = new List<Filters>();
                var orgfilter = new Filters()
                {
                    Property = "OrganizationUnitId",
                    Comparator = 6,//In Operator
                    SearchTerm = strOrgIds,
                    DataType = DataTypes.Text

                };
                input.Filters.Add(orgfilter);
            }

            if (!ReferenceEquals(input.Filters, null))
            {
                var filterCondition = ExpressionBuilder.GetExpression<DivisionCacheItem>(input.Filters).Compile();
                divisionCacheItems = divisionCache.ToList().Where(filterCondition).ToList();
            }

            return divisionCacheItems.Select(item =>
            {
                var dto = new ProjectAccessListUnitDto();
                dto.JobNumber = item.JobNumber;
                dto.Caption = item.Caption;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                dto.JobId = item.JobId;
                return dto;
            }).ToList();

        }


        /// <summary>
        /// Get Project/Division List which is not in ProjectAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ProjectAccessListUnitDto>> GetProjectList(GetUserSecuritySettingsInputUnit input)
        {
            List<DivisionCacheItem> divisionCacheItems = new List<DivisionCacheItem>();
            AutoSearchInput cacheInput = new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId };

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);

            var divisionCache = await _divisionCache.GetDivisionCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), cacheInput);

            if (!ReferenceEquals(input.Filters, null))
            {
                var filterCondition = ExpressionBuilder.GetExpression<DivisionCacheItem>(input.Filters).Compile();
                divisionCacheItems = divisionCache.ToList().Where(filterCondition).Where(p => !organizationUnits.Any(p2 => p2.Id == p.OrganizationUnitId)).ToList();
            }
            else
            {
                divisionCacheItems = divisionCache.ToList().Where(p => !organizationUnits.Any(p2 => p2.Id == p.OrganizationUnitId)).ToList();
            }


            return divisionCacheItems.Select(item =>
            {
                var dto = new ProjectAccessListUnitDto();
                dto.JobId = item.JobId;
                dto.JobNumber = item.JobNumber;
                dto.Caption = item.Caption;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                return dto;
            }).ToList();
        }

        #endregion


        #region Credit Card Access List

        /// <summary>
        /// Create or Update Credit Cards
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateorUpdateCreditCardAccessList(UserSecuritySettingsInputUnit input)
        {

            var tenantId = Convert.ToInt32(_customAppSession.TenantId);
            foreach (var userId in input.UserIdList)
            {

                var user = await _userManager.GetUserByIdAsync(userId);
                var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, EntityClassification.CreditCard);

                if (!ReferenceEquals(input.CreditCardAccessList, null))
                {
                    input.CreditCardAccessList.ForEach(u => u.UserId = userId);

                    //assign Project restrictions to User
                    foreach (var creditCard in input.CreditCardAccessList)
                    {
                        var organization = await _organizationUnitRepository.FirstOrDefaultAsync(u => u.Id == creditCard.OrganizationUnitId);
                        if (organization != null)
                        {
                            if (!await _userOrganizationUnitRepository.GetAll().AnyAsync(u => u.OrganizationUnitId == organization.Id && u.UserId == creditCard.UserId))
                                await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, organization.Id));
                        }
                        else
                        {
                            var orgUnit = new OrganizationExtended(tenantId, creditCard.CardNumber, entityClassificationId: EntityClassification.CreditCard);
                            orgUnit.Code = await _organizationUnitManager.GetNextChildCodeAsync(null);
                            var orgId = await _organizationUnitRepository.InsertAndGetIdAsync(orgUnit);
                            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, orgId));
                            var creditCardDetails = await _creditCardUnitRepository.FirstOrDefaultAsync(u => u.Id == creditCard.AccountingDocumentId);
                            creditCardDetails.OrganizationUnitId = orgId;
                            await _creditCardUnitRepository.UpdateAsync(creditCardDetails);
                        }
                    }

                    //delete Credit Card restrictions from User
                    var deleteCreditCardList = organizationUnits.Where(p => !input.ProjectAccessList.Any(p2 => p2.OrganizationUnitId == p.Id)).ToList();
                    foreach (var UserOrg in deleteCreditCardList)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }  //delete Credit Card restrictions from User
                else
                {
                    foreach (var UserOrg in organizationUnits)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }
            }
        }


        /// <summary>
        /// Get Credit Card Access List By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CreditAccessListUnitDto>> GetCreditCardAccessList(GetUserSecuritySettingsInputUnit input)
        {


            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);
            var organizationUnitIds = organizationUnits.Select(ou => ou.Id);
            var strOrgIds = string.Join(",", organizationUnitIds.ToArray());

            var typeOfbankList = Enum.GetValues(typeof(TypeOfBankAccount)).Cast<TypeOfBankAccount>().Select(x => x)
                      .ToDictionary(u => u.ToDescription(), u => (int)u).Where(u => u.Value >= 61 && u.Value <= 69)
                      .Select(u => u.Key).ToArray();

            var strTypeOfbankAC = string.Join(",", typeOfbankList);

            var query = from creditCard in _creditCardUnitRepository.GetAll().Where(u => strOrgIds.Contains(u.OrganizationUnitId.Value.ToString()))
                        join bankAccount in _bankAccountUnitRepository.GetAll().Where(u => strTypeOfbankAC.Contains(u.TypeOfBankAccountId.ToString())) on creditCard.BankAccountId equals bankAccount.Id
                        select new { CardHolderName = bankAccount.Description, CardNumber = bankAccount.BankAccountNumber, OrganizationUnitId = creditCard.OrganizationUnitId, AccountingDocumentId = creditCard.Id };

            var creditCardAccessList = await query.ToListAsync();

            return creditCardAccessList.Select(item =>
            {
                var dto = new CreditAccessListUnitDto();
                dto.CardNumber = item.CardNumber;
                dto.CardHolderName = item.CardHolderName;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                dto.AccountingDocumentId = item.AccountingDocumentId;
                return dto;
            }).ToList();

        }


        /// <summary>
        /// Get CreditCard List which is not in CreditCardAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CreditAccessListUnitDto>> GetCreditCardList(GetUserSecuritySettingsInputUnit input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);
            var organizationUnitIds = organizationUnits.Select(ou => ou.Id);
            var strOrgIds = string.Join(",", organizationUnitIds.ToArray());
            var values = Enum.GetValues(typeof(TypeOfBankAccount)).Cast<TypeOfBankAccount>().Select(x => x)
                           .ToDictionary(u => u.ToDescription(), u => (int)u).Where(u => u.Value >= 61 && u.Value <= 69)
                           .Select(u => u.Key).ToArray();

            var strTypeOfbankAC = string.Join(",", values);

            var query = from creditCard in _creditCardUnitRepository.GetAll().Where(u => !strOrgIds.Contains(u.OrganizationUnitId.Value.ToString()))
                        join bankAccount in _bankAccountUnitRepository.GetAll().Where(u => strTypeOfbankAC.Contains(u.TypeOfBankAccountId.ToString())) on creditCard.BankAccountId equals bankAccount.Id
                        select new { CardHolderName = bankAccount.Description, CardNumber = bankAccount.BankAccountNumber, OrganizationUnitId = creditCard.OrganizationUnitId, AccountingDocumentId = creditCard.Id };

            var creditCardAccessList = await query.ToListAsync();//.Where(p => p.creditCardAccess.UserId == input.UserId && p.creditCardAccess.IsActive == true).ToListAsync();

            return creditCardAccessList.Select(item =>
            {
                var dto = new CreditAccessListUnitDto();
                dto.CardNumber = item.CardNumber;
                dto.CardHolderName = item.CardHolderName;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                dto.AccountingDocumentId = item.AccountingDocumentId;
                return dto;
            }).ToList();
        }

        #endregion


        #region BankAccount Access List

        /// <summary>
        /// Create or Update Bank Accounts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateorUpdateBankAccountAccessList(UserSecuritySettingsInputUnit input)
        {

            var tenantId = Convert.ToInt32(_customAppSession.TenantId);
            foreach (var userId in input.UserIdList)
            {

                var user = await _userManager.GetUserByIdAsync(userId);
                var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, EntityClassification.BankAccount);

                if (!ReferenceEquals(input.BankAccountAccessList, null))
                {
                    input.BankAccountAccessList.ForEach(u => u.UserId = userId);

                    //assign Bank Account restrictions to User
                    foreach (var bankAccount in input.BankAccountAccessList)
                    {
                        var organization = await _organizationUnitRepository.FirstOrDefaultAsync(u => u.Id == bankAccount.OrganizationUnitId);
                        if (organization != null)
                        {
                            if (!await _userOrganizationUnitRepository.GetAll().AnyAsync(u => u.OrganizationUnitId == organization.Id && u.UserId == bankAccount.UserId))
                                await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, organization.Id));
                        }
                        else
                        {
                            var orgUnit = new OrganizationExtended(tenantId, bankAccount.BankAccountNumber, entityClassificationId: EntityClassification.BankAccount);
                            orgUnit.Code = await _organizationUnitManager.GetNextChildCodeAsync(null);
                            var orgId = await _organizationUnitRepository.InsertAndGetIdAsync(orgUnit);
                            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(tenantId, userId, orgId));
                            var bankAccountDetails = await _bankAccountUnitRepository.FirstOrDefaultAsync(u => u.Id == bankAccount.BankAccountId);
                            bankAccountDetails.OrganizationUnitId = orgId;
                            await _bankAccountUnitRepository.UpdateAsync(bankAccountDetails);
                        }
                    }

                    //delete Bank Account restrictions from User
                    var deleteBankAccountList = organizationUnits.Where(p => !input.ProjectAccessList.Any(p2 => p2.OrganizationUnitId == p.Id)).ToList();
                    foreach (var UserOrg in deleteBankAccountList)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }  //delete Bank Account restrictions from User
                else
                {
                    foreach (var UserOrg in organizationUnits)
                    {
                        await UserManager.RemoveFromOrganizationUnitAsync(userId, UserOrg.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Get Bank Accounts Access List By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<BankAccountAccessListUnitDto>> GetBankAccountAccessList(GetUserSecuritySettingsInputUnit input)
        {
            List<BankAccountCacheItem> bankAccountCacheItems = new List<BankAccountCacheItem>();

            AutoSearchInput cacheInput = new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId };
            var bankAccountCache = await _bankAccountCache.GetBankAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.BankAccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), cacheInput);

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);
            var organizationUnitIds = organizationUnits.Select(ou => ou.Id);
            var strOrgIds = string.Join(",", organizationUnitIds.ToArray());

            if (!string.IsNullOrEmpty(strOrgIds))
            {
                if (ReferenceEquals(input.Filters, null))
                    input.Filters = new List<Filters>();
                var orgfilter = new Filters()
                {
                    Property = "OrganizationUnitId",
                    Comparator = 6,//In Operator
                    SearchTerm = strOrgIds,
                    DataType = DataTypes.Text

                };
                input.Filters.Add(orgfilter);
            }

            if (!ReferenceEquals(input.Filters, null))
            {
                var filterCondition = ExpressionBuilder.GetExpression<BankAccountCacheItem>(input.Filters).Compile();
                bankAccountCacheItems = bankAccountCache.ToList().Where(filterCondition).ToList();
            }

            return bankAccountCacheItems.Select(item =>
            {
                var dto = new BankAccountAccessListUnitDto();
                dto.BankAccountNumber = item.BankAccountNumber;
                dto.BankName = item.Description;
                dto.AccountName = item.BankAccountName;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                dto.BankAccountId = item.BankAccountId;
                return dto;
            }).ToList();

        }


        /// <summary>
        /// Get Bank Accounts List which is not in BankAccountsAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<BankAccountAccessListUnitDto>> GetBankAccountList(GetUserSecuritySettingsInputUnit input)
        {
            List<BankAccountCacheItem> bankAccountCacheItems = new List<BankAccountCacheItem>();
            AutoSearchInput cacheInput = new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId };

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var organizationUnits = await _organizationExtendedUnitManager.GetExtendedOrganizationUnitsAsync(user, input.EntityClassificationId);

            var bankAccountCache = await _bankAccountCache.GetBankAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.BankAccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), cacheInput);

            if (!ReferenceEquals(input.Filters, null))
            {
                var filterCondition = ExpressionBuilder.GetExpression<BankAccountCacheItem>(input.Filters).Compile();
                bankAccountCacheItems = bankAccountCache.ToList().Where(filterCondition).Where(p => !organizationUnits.Any(p2 => p2.Id == p.OrganizationUnitId)).ToList();
            }
            else
            {
                bankAccountCacheItems = bankAccountCache.ToList().Where(p => !organizationUnits.Any(p2 => p2.Id == p.OrganizationUnitId)).ToList();
            }


            return bankAccountCacheItems.Select(item =>
            {
                var dto = new BankAccountAccessListUnitDto();
                dto.BankAccountNumber = item.BankAccountNumber;
                dto.BankName = item.Description;
                dto.AccountName = item.BankAccountName;
                dto.OrganizationUnitId = item.OrganizationUnitId;
                dto.BankAccountId = item.BankAccountId;
                return dto;
            }).ToList();
        }

        #endregion


    }
}

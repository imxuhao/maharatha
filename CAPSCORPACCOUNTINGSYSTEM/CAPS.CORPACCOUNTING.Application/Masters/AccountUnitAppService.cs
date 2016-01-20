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

namespace CAPS.CORPACCOUNTING.Accounts
{
    public class AccountUnitAppService : CORPACCOUNTINGAppServiceBase, IAccountUnitAppService
    {
        private readonly AccountUnitManager _accountUnitManager;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AccountUnitAppService(AccountUnitManager accountUnitManager, IRepository<AccountUnit, long> accountUnitRepository, UserManager userManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _accountUnitManager = accountUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task<ListResultOutput<AccountUnitDto>> GetAccountUnits()
        {
           
           
            var query =
                from au in _accountUnitRepository.GetAll()

                select new { au, memberCount = au };

            var items = await query.ToListAsync();

            return new ListResultOutput<AccountUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<AccountUnitDto>();
                    //dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
        }
        [UnitOfWork]
        public async Task<AccountUnitDto> CreateAccountUnit(CreateAccountUnitInput input)
        {
       
            //var accountUnit = new AccountUnit(accountNumber: input.AccountNumber, caption: input.Caption, chartOfAccountId: input.ChartOfAccountId, parentId: input.ParentId);
            var accountUnit = new AccountUnit(accountNumber:input.AccountNumber,caption:input.Caption,chartOfAccountId:input.ChartOfAccountId,parentId:input.ParentId,organizationunitid:1);

            
            await _accountUnitManager.CreateAsync(accountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) => { };


            return accountUnit.MapTo<AccountUnitDto>();
        }

       

    }
}

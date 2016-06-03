using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Banking
{
   public class BankAccountUnitManager:DomainService
    {

        protected IRepository<BankAccountUnit, long> BankAccountUnitRepository { get; }

        public BankAccountUnitManager(IRepository<BankAccountUnit, long> bankAccountUnitRepository)
        {
            BankAccountUnitRepository = bankAccountUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(BankAccountUnit bankAccountUnit)
        {
            await BankAccountUnitRepository.InsertAsync(bankAccountUnit);
        }

        public virtual async Task UpdateAsync(BankAccountUnit bankAccountUnit)
        {
            await BankAccountUnitRepository.UpdateAsync(bankAccountUnit);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await BankAccountUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }

    }
}

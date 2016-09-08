using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Abp.Zero;
using System.Linq;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Banking
{
    public class BankAccountUnitManager:DomainService
    {
        /// <summary>
        /// This Property set for calling from Which Service(Credit Card,Bank)
        /// </summary>
        public string ServiceFrom { get; set; }

        protected IRepository<BankAccountUnit, long> BankAccountUnitRepository { get; }

        public BankAccountUnitManager(IRepository<BankAccountUnit, long> bankAccountUnitRepository)
        {
            BankAccountUnitRepository = bankAccountUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task<long> CreateAsync(BankAccountUnit bankAccountUnit)
        {
            await ValidateBankAccountUnitAsync(bankAccountUnit);
            return  await BankAccountUnitRepository.InsertAndGetIdAsync(bankAccountUnit);
        }

        public virtual async Task UpdateAsync(BankAccountUnit bankAccountUnit)
        {
            await ValidateBankAccountUnitAsync(bankAccountUnit);
            await BankAccountUnitRepository.UpdateAsync(bankAccountUnit);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await BankAccountUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }

        /// <summary>
        /// Validating the VendorData
        /// </summary>
        /// <param name="bankAccountUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateBankAccountUnitAsync(BankAccountUnit bankAccountUnit)
        {
            string lableName = string.Empty;

            switch(ServiceFrom)
            {
                case "CreditCardCompany":
                    lableName = "Credit Card Company";
                    break;
                default:
                    lableName = "Bank Name";
                    break;
            }

            //Validating if Duplicate Banks/Credit Card Company exists
            if (BankAccountUnitRepository != null)
            {
                var bankAccount = (await BankAccountUnitRepository.GetAllListAsync(p => p.Description == bankAccountUnit.Description));

                if (bankAccountUnit.Id == 0)
                {
                    if (bankAccount.Count > 0)
                    {
                        
                        throw new UserFriendlyException(L("Duplicate "+ lableName, bankAccountUnit.Description));
                    }
                }
                else
                {
                    if (bankAccount.FirstOrDefault(p => p.Id != bankAccountUnit.Id && p.Description == bankAccountUnit.Description) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate "+ lableName, bankAccountUnit.Description));
                    }
                }
            }
        }

    }
}

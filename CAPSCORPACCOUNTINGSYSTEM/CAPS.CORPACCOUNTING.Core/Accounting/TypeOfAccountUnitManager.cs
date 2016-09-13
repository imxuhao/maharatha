using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;
using System.Linq;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Accounting
{
    public class TypeOfAccountUnitManager : DomainService
    {
        protected IRepository<TypeOfAccountUnit> TypeOfAccountUnitRepository { get; private set; }

        /// <summary>
        /// TypeOfAccountUnitManager Constructor to initializing the TypeOfAccountUnit Repository
        /// </summary>
        /// <param name="typeofaccountunitrepository"></param>
        public TypeOfAccountUnitManager(IRepository<TypeOfAccountUnit> typeofaccountunitrepository)
        {
            TypeOfAccountUnitRepository = typeofaccountunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting typeOfAccountUnit Details
        /// </summary>
        /// <param name="typeOfAccountUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<int> CreateAsync(TypeOfAccountUnit typeOfAccountUnit)
        {
            await ValidateTypeOfAccountUnitAsync(typeOfAccountUnit);
            return await TypeOfAccountUnitRepository.InsertAndGetIdAsync(typeOfAccountUnit);
        }

        /// <summary>
        /// Updating typeOfAccountUnit Details
        /// </summary>
        /// <param name="typeOfAccountUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TypeOfAccountUnit typeOfAccountUnit)
        {
            await ValidateTypeOfAccountUnitAsync(typeOfAccountUnit);
            await TypeOfAccountUnitRepository.UpdateAsync(typeOfAccountUnit);
        }

        /// <summary>
        /// Deleting typeOfAccountUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            var typeOfaccount = (await TypeOfAccountUnitRepository.GetAsync(id));
            if (!typeOfaccount.IsEditable)
                throw new UserFriendlyException(L("Predefined Classification is not deleted", typeOfaccount.Description));
            await TypeOfAccountUnitRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Validating typeOfAccountUnit 
        /// </summary>
        /// <param name="typeOfAccountUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateTypeOfAccountUnitAsync(TypeOfAccountUnit typeOfAccountUnit)
        {

            if(!typeOfAccountUnit.IsEditable)
                throw new UserFriendlyException(L("Predefined Classification is not editable", typeOfAccountUnit.Description));

            if (TypeOfAccountUnitRepository != null)
            {
                var typeOfaccount = (await TypeOfAccountUnitRepository.GetAllListAsync(p => p.Description == typeOfAccountUnit.Description));

                if (typeOfAccountUnit.Id == 0)
                {
                    if (typeOfaccount.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Classification", typeOfAccountUnit.Description));
                    }
                }
                else
                {
                    if (typeOfaccount.FirstOrDefault(p => p.Id != typeOfAccountUnit.Id && p.Description == typeOfAccountUnit.Description) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Classification", typeOfAccountUnit.Description));
                    }
                }
            }
        }
    }
}
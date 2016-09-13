using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class EmployeeUnitManager : DomainService
    {
        protected IRepository<EmployeeUnit> EmployeeUnitRepository { get;  }

        /// <summary>
        /// EmployeeUnitManager Constructor to initializing the EmployeeUnit Repository
        /// </summary>
        /// <param name="employeeunitrepository"></param>
        public EmployeeUnitManager(IRepository<EmployeeUnit> employeeunitrepository)
        {
            EmployeeUnitRepository = employeeunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting EmployeeUnit Details
        /// </summary>
        /// <param name="employeeunit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(EmployeeUnit employeeunit)
        {
            await ValidateEmployeeUnitAsync(employeeunit);
            await EmployeeUnitRepository.InsertAsync(employeeunit);
        }

        /// <summary>
        ///  Updating EmployeeUnit Details
        /// </summary>
        /// <param name="employeeunit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(EmployeeUnit employeeunit)
        {
            await ValidateEmployeeUnitAsync(employeeunit);
            await EmployeeUnitRepository.UpdateAsync(employeeunit);
        }

        /// <summary>
        /// Deleting EmployeeUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await EmployeeUnitRepository.DeleteAsync(id);
        }
        /// <summary>
        /// Validating CustomePaymentterms
        /// </summary>
        /// <param name="employeeunit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateEmployeeUnitAsync(EmployeeUnit employeeunit)
        {
            //Validating if Duplicate Employee exists
            if (EmployeeUnitRepository != null)
            {
                var employee = (await EmployeeUnitRepository.GetAllListAsync(p => p.LastName == employeeunit.LastName && p.OrganizationUnitId == employeeunit.OrganizationUnitId));

                if (employeeunit.Id == 0)
                {
                    if (employee.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", employeeunit.LastName));
                    }
                }
                else
                {
                    if (employee.FirstOrDefault(p => p.Id != employeeunit.Id && p.LastName == employeeunit.LastName) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", employeeunit.LastName));
                    }
                }
            }
        }
    }

}

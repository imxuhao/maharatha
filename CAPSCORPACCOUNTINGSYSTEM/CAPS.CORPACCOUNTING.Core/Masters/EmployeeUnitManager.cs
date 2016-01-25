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

        public EmployeeUnitManager(IRepository<EmployeeUnit> employeeunitrepository)
        {
            EmployeeUnitRepository = employeeunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(EmployeeUnit employeeunit)
        {
            await ValidateEmployeeUnitAsync(employeeunit);
            await EmployeeUnitRepository.InsertAsync(employeeunit);
        }

        public virtual async Task UpdateAsync(EmployeeUnit employeeunit)
        {
            await ValidateEmployeeUnitAsync(employeeunit);
            await EmployeeUnitRepository.UpdateAsync(employeeunit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await EmployeeUnitRepository.DeleteAsync(id);
        }
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

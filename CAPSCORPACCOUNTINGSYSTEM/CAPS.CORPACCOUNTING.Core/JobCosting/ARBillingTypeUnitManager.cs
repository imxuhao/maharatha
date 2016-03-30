using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class ARBillingTypeUnitManager : DomainService
    {
        protected IRepository<ARBillingTypeUnit> ARBillingUnitRepository { get; }

        public ARBillingTypeUnitManager(IRepository<ARBillingTypeUnit> arbillingunitrepository)
        {
            ARBillingUnitRepository = arbillingunitrepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Creating ARBillingType Entity 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(ARBillingTypeUnit input)
        {
            //await ValidateJobUnitAsync(jobUnit);
            await ARBillingUnitRepository.InsertAsync(input);
        }

        /// <summary>
        /// Updating ARBillingType
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(ARBillingTypeUnit input)
        {
            // await ValidateJobUnitAsync(job);
            await ARBillingUnitRepository.UpdateAsync(input);
        }

        /// <summary>
        /// Deleting ARBillingType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await ARBillingUnitRepository.DeleteAsync(id);
        }
        //protected virtual async Task ValidateJobUnitAsync(JobUnit jobUnit)
        //{
        //    //Validating if Duplicate Caption exists
        //    if (ARBillingUnitRepository != null)
        //    {
        //        var jobUnits = (await JobUnitRepository.GetAllListAsync(p => p.Caption == jobUnit.Caption && p.OrganizationUnitId == jobUnit.OrganizationUnitId));

        //        if (jobUnit.Id == 0)
        //        {
        //            if (jobUnits.Count > 0)
        //            {
        //                throw new UserFriendlyException(L("Duplicate Caption", jobUnit.Caption));
        //            }
        //        }
        //        else
        //        {
        //            if (jobUnits.FirstOrDefault(p => p.Id != jobUnit.Id && p.Caption == jobUnit.Caption) != null)
        //            {
        //                throw new UserFriendlyException(L("Duplicate Caption", jobUnit.Caption));
        //            }
        //        }
        //    }
        //}
    }
}

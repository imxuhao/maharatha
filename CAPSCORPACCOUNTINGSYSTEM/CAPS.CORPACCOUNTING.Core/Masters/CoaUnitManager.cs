using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class CoaUnitManager : DomainService
    {
        protected IRepository<CoaUnit> CoaUnitRepository { get; private set; }

        /// <summary>
        /// CoaUnitManager Constructor to initializing the CoaUnit Repository
        /// </summary>
        /// <param name="coaunitrepository"></param>
        public CoaUnitManager(IRepository<CoaUnit> coaunitrepository)
        {
            CoaUnitRepository = coaunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting Coaunit Details
        /// </summary>
        /// <param name="coaUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(CoaUnit coaUnit)
        {
            await ValidateCoaUnitAsync(coaUnit);
            await CoaUnitRepository.InsertAsync(coaUnit);
        }

        /// <summary>
        /// Updating CoaUnit Details
        /// </summary>
        /// <param name="coaUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(CoaUnit coaUnit)
        {
            await ValidateCoaUnitAsync(coaUnit);
            await CoaUnitRepository.UpdateAsync(coaUnit);
        }

        /// <summary>
        /// Deleting CoaUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await CoaUnitRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Validating CoaUnit 
        /// </summary>
        /// <param name="coaUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateCoaUnitAsync(CoaUnit coaUnit)
        {
            //Validating if Duplicate COA exists
            if (CoaUnitRepository != null)
            {
                if (coaUnit.IsCorporate && coaUnit.Id==0&&coaUnit.TypeOfChartId == TypeOfChart.HOMECOA)
                {
                    int count = (await CoaUnitRepository.CountAsync(p => p.TypeOfChartId ==TypeOfChart.HOMECOA&&
                                                                         p.IsCorporate == true ));
                    if (count > 0)
                    {
                        var errorMessage = L("OnlyOneHomeCoaAllowed");
                        throw new UserFriendlyException(errorMessage);
                    }
                }
                else if (coaUnit.IsCorporate && coaUnit.Id == 0 && coaUnit.TypeOfChartId == TypeOfChart.NEWCOA)
                {
                    int count = (await CoaUnitRepository.CountAsync(p => p.TypeOfChartId == TypeOfChart.NEWCOA &&
                                                                         p.IsCorporate == true));
                    if (count > 0)
                    {
                        var errorMessage = L("OnlyOneNewCoaAllowed");
                        throw new UserFriendlyException(errorMessage);
                    }
                }

                else
                {
                    var coaunit = (await CoaUnitRepository.
                        GetAllListAsync(
                            p => p.Caption == coaUnit.Caption && p.OrganizationUnitId == coaUnit.OrganizationUnitId));

                    if (coaUnit.Id == 0)
                    {
                        if (coaunit.Count > 0)
                        {
                            throw new UserFriendlyException(L("Duplicate ChartofAccount", coaUnit.Caption));
                        }
                    }
                    else
                    {
                        if (coaunit.FirstOrDefault(p => p.Id != coaUnit.Id && p.Caption == coaUnit.Caption) != null)
                        {
                            throw new UserFriendlyException(L("Duplicate ChartofAccount", coaUnit.Caption));
                        }
                    }
                }
            }
        }
    }
}
using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using Abp.Zero;
using Abp.Domain.Uow;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
   public class TerritoriesUnitManager : DomainService
    {
        protected IRepository<TerritoriesUnit> TerritoriesUnitRepository { get; }


        /// <summary>
        ///  TerritoriesUnitManager Constructor to initializing the TerritoriesUnit Repository
        /// </summary>
        /// <param name="territoriesunitrepository"></param>
        public TerritoriesUnitManager(IRepository<TerritoriesUnit> territoriesunitrepository)
        {
            TerritoriesUnitRepository = territoriesunitrepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }


        /// <summary>
        /// Inserting TerritoriesUnit Details
        /// </summary>
        /// <param name="TerritoriesUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(TerritoriesUnit territoriesunit)
        {
            await ValidateTerritorieUnitAsync(territoriesunit);
            await TerritoriesUnitRepository.InsertAsync(territoriesunit);
        }


        /// <summary>
        ///  Updating TerritoriesUnit Details
        /// </summary>
        /// <param name="territoriesunit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TerritoriesUnit territoriesunit)
        {
            await ValidateTerritorieUnitAsync(territoriesunit);
            await TerritoriesUnitRepository.UpdateAsync(territoriesunit);
        }


        /// <summary>
        /// Deleting TerritoriesUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await TerritoriesUnitRepository.DeleteAsync(id);
        }


        /// <summary>
        /// Validating TerritoriesUnit
        /// </summary>
        /// <param name="territoriesunit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateTerritorieUnitAsync(TerritoriesUnit territoriesunit)
        {
            //Validating if Duplicate Territories exists
            if (TerritoriesUnitRepository != null)
            {
                var territorie = (await TerritoriesUnitRepository.GetAllListAsync(p => p.Description == territoriesunit.Description));

                if (territoriesunit.Id == 0)
                {
                    if (territorie.Count > 0)
                    {
                        throw new UserFriendlyException(L("DuplicateTerritorie", territoriesunit.Description));
                    }
                }
                else
                {
                    if (territorie.FirstOrDefault(p => p.Id != territoriesunit.Id && p.Description == territoriesunit.Description) != null)
                    {
                        throw new UserFriendlyException(L("DuplicateTerritorie", territoriesunit.Description));
                    }
                }
            }
        }
    }
}

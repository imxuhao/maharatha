using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using Abp.Zero;
using Abp.Domain.Uow;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class TaxCreditUnitManager : DomainService
    {
        protected IRepository<TaxCreditUnit> TaxCreditUnitRepository { get; }


        /// <summary>
        ///  TaxCreditUnitManager Constructor to initializing the TaxCreditUnit Repository
        /// </summary>
        /// <param name="taxcreditunitrepository"></param>
        public TaxCreditUnitManager(IRepository<TaxCreditUnit> taxcreditunitrepository)
        {
            TaxCreditUnitRepository = taxcreditunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }


        /// <summary>
        /// Inserting taxcreditunit Details
        /// </summary>
        /// <param name="taxcreditunit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(TaxCreditUnit taxcreditunit)
        {
            await ValidateTaxCreditUnitAsync(taxcreditunit);
            await TaxCreditUnitRepository.InsertAsync(taxcreditunit);
        }


        /// <summary>
        ///  Updating TaxCreditUnit Details
        /// </summary>
        /// <param name="taxcreditunit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TaxCreditUnit taxcreditunit)
        {
            await ValidateTaxCreditUnitAsync(taxcreditunit);
            await TaxCreditUnitRepository.UpdateAsync(taxcreditunit);
        }


        /// <summary>
        /// Deleting TaxCreditUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await TaxCreditUnitRepository.DeleteAsync(id);
        }


        /// <summary>
        /// Validating TaxCreditUnit
        /// </summary>
        /// <param name="taxCreditUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateTaxCreditUnitAsync(TaxCreditUnit taxCreditUnit)
        {
            //Validating if Duplicate taxCredit exists
            if (TaxCreditUnitRepository != null)
            {
                var taxCredit = (await TaxCreditUnitRepository.GetAllListAsync(p => p.Number == taxCreditUnit.Number));

                if (taxCreditUnit.Id == 0)
                {
                    if (taxCredit.Count > 0)
                    {
                        throw new UserFriendlyException(L("DuplicateTaxCredit", taxCreditUnit.Number));
                    }
                }
                else
                {
                    if (taxCredit.FirstOrDefault(p => p.Id != taxCreditUnit.Id && p.Number == taxCreditUnit.Number) != null)
                    {
                        throw new UserFriendlyException(L("DuplicateTaxCredit", taxCreditUnit.Number));
                    }
                }
            }
        }
    }
}

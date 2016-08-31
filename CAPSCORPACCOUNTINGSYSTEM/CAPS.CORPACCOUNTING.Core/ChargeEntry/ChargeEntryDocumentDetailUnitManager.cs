using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;

namespace CAPS.CORPACCOUNTING.ChargeEntry
{
   public class ChargeEntryDocumentDetailUnitManager : DomainService
    {
        protected IRepository<ChargeEntryDocumentDetailUnit, long> ChargeEntryDocumentDetailUnitRepository { get; }
        public ChargeEntryDocumentDetailUnitManager(IRepository<ChargeEntryDocumentDetailUnit, long> chargeEntryDocumentDetailUnitDetailUnitRepository)
        {
            ChargeEntryDocumentDetailUnitRepository = chargeEntryDocumentDetailUnitDetailUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(ChargeEntryDocumentDetailUnit input)
        {

            return await ChargeEntryDocumentDetailUnitRepository.InsertAndGetIdAsync(input);
        }


        public virtual async Task UpdateAsync(ChargeEntryDocumentDetailUnit input)
        {
            await ChargeEntryDocumentDetailUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await ChargeEntryDocumentDetailUnitRepository.DeleteAsync(input.Id);
        }
    }
}

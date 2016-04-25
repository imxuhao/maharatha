using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Domain.Uow;

namespace CAPS.CORPACCOUNTING.Localization
{
    public class CustomLanguageTextsUnitManager : DomainService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        protected IRepository<CustomLanguageTextsUnit, long> _customLanguageTextsUnitrepository { get; }
        public CustomLanguageTextsUnitManager(IRepository<CustomLanguageTextsUnit, long> customLanguageTextsUnitrepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _customLanguageTextsUnitrepository = customLanguageTextsUnitrepository;
            _unitOfWorkManager = unitOfWorkManager;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }
        public virtual async Task UpdateAsync(int? tenantId,  string key, string regularexpression,
             bool isactive, bool ismandatory, long? organizationunitid)
        {                                                                                                         
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))                        
            {                                                                                                       
                var existingEntity = await _customLanguageTextsUnitrepository.FirstOrDefaultAsync(t =>
                    t.TenantId == tenantId &&                  
                    t.Key == key
                    );

                if (existingEntity != null)
                {
                    existingEntity.RegularExpression = regularexpression;
                    existingEntity.IsMandatory = ismandatory;
                    existingEntity.IsActive = isactive;
                    existingEntity.OrganizationUnitId = organizationunitid;
                     await _unitOfWorkManager.Current.SaveChangesAsync();
                   
                }
                else
                {
                    await _customLanguageTextsUnitrepository.InsertAsync(
                        new CustomLanguageTextsUnit
                        {
                            TenantId = tenantId,
                            Key= key,
                            RegularExpression= regularexpression,
                            IsActive= isactive,
                            IsMandatory= ismandatory,
                            OrganizationUnitId= organizationunitid
                        });
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                }
            }
        }
    }
}

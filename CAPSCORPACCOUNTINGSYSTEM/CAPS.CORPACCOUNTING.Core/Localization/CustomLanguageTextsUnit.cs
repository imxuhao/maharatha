using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Localization
{
    [Table("CAPS_CustomLanguageTexts")]
    public class CustomLanguageTextsUnit : AuditedEntity<long>, IMayHaveOrganizationUnit, IMayHaveTenant
    {     
        public virtual string RegularExpression { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsMandatory { get; set; }
        public virtual long? OrganizationUnitId { get; set; }       
        public virtual string Key { get; set; }
        public int? TenantId    {  get; set; }
        public CustomLanguageTextsUnit() { }
        public CustomLanguageTextsUnit(string regularexpression,
            bool isactive, bool ismandatory, long? organizationunitid, string key)
        {           
            RegularExpression = regularexpression;
            IsActive = isactive;
            IsMandatory = ismandatory;
            OrganizationUnitId = organizationunitid;
            Key = key;
        }

    }
}

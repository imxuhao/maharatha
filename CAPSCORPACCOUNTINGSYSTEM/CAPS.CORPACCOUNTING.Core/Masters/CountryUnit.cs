using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    ///  TypeOfCountry is the table name in Lajit
    /// </summary>
    [Table("CAPS_Country")]
    public class CountryUnit:Entity, IMustHaveTenant
    {
        /// <summary> the ID column with TypeOfCountryId field. </summary>
        [Column("CountryID")]
        public virtual int Id { get; set; }
        /// <summary>Gets or sets the TypeOfCountryId field. </summary>
        public virtual short? TypeOfCountryId { get; set; }
        [ForeignKey("TypeOfCountryId")]
        public virtual TypeOfCountryUnit TypeOfCountryUnit { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        public CountryUnit()
        { }

        public CountryUnit(short typeOfCountryId, int tenantId) {
            TypeOfCountryId = typeOfCountryId;
            TenantId = tenantId;
        }
    }
}

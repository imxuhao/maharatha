using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// TypeOfCategory is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfCategory")]
    public class TypeOfCategoryUnit
    {

        private const int MaxDescription = 100;

        /// <summary> Overriding the ID column with TypeOfCategoryId field. </summary>
        [Column("TypeOfCategoryId")]
        public virtual short Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescription)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;


namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("Caps_ConnectionStrings")]
    public class ConnectionStringUnit :Entity
    {
        public const int MaxConnectionStringLength = 1024;
        public const int MaxNameLength = 100;

        [Column("ConnectionStringId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Name field. </summary>
        [StringLength(MaxNameLength)]
        [Required]
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the Name field. </summary>
        [StringLength(MaxConnectionStringLength)]
        [Required]
        public virtual string ConnectionString { get; set; }

        public ConnectionStringUnit()
        {
            
        }

        public ConnectionStringUnit(string connectionString)
        {
            ConnectionString = connectionString;
            Name = "DefaultConnection";
        }

    }
}

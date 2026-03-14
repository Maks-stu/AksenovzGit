using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ClassLibrary.Classes
{
    [DataContract]
    [Table("products_sales", Schema = "app")]
    public class ProductSale
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [DataMember]
        [Column("partner_id")]
        public int PartnerId { get; set; }

        [DataMember]
        [Column("production_id")]
        public int ProductionId { get; set; }

        [DataMember]
        [Column("count")]
        public double Count { get; set; }

        [DataMember]
        [Column("date")]
        public DateOnly SaleDate { get; set; }

        [DataMember]
        [Column("total_price")]
        public double? TotalPrice { get; set; }
    }
}
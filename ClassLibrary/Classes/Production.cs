using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ClassLibrary.Classes
{
    [DataContract]
    [Table("production", Schema = "app")] // указание схемы, если нужно
    public class Production
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [DataMember]
        [Required]
        [Column("article")]
        public string Article { get; set; }

        [DataMember]
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [DataMember]
        [Column("description")]
        public string Description { get; set; }

        [DataMember]
        [Column("photo")]
        public string Photo { get; set; }

        [DataMember]
        [Required]
        [Column("min_partner_price")]
        public double MinPartnerPrice { get; set; }

        [DataMember]
        [Column("length")]
        public int? Length { get; set; }

        [DataMember]
        [Column("width")]
        public int? Width { get; set; }

        [DataMember]
        [Column("height")]
        public int? Height { get; set; }

        [DataMember]
        [Column("net_weight")]
        public double? NetWeight { get; set; }

        [DataMember]
        [Column("gross_weight")]
        public double? GrossWeight { get; set; }

        [DataMember]
        [Column("quality_certificate")]
        public string QualityCertificate { get; set; }

        [DataMember]
        [Column("standard_number")]
        public string StandardNumber { get; set; }

        [DataMember]
        [Column("cost_price")]
        public double? CostPrice { get; set; }
    }
}
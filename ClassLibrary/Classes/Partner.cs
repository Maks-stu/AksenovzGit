using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ClassLibrary.Classes
{
    [DataContract]
    [Table("partners", Schema = "app")]
    public class Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [DataMember]
        [Column("type")]
        public string Type { get; set; }

        [Required]
        [DataMember]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [DataMember]
        [Column("legal_address")]
        public string LegalAddress { get; set; }

        [Required]
        [DataMember]
        [Column("inn")]
        public string Inn { get; set; }  // Именование без заглавных букв, как принято

        [DataMember]
        [Column("director_name")]
        public string DirectorName { get; set; }

        [DataMember]
        [Column("phone")]
        public string Phone { get; set; }

        [DataMember]
        [Column("email")]
        public string Email { get; set; }

        [DataMember]
        [Column("logo")]
        public string? Logo { get; set; }

        [DataMember]
        [Column("rating")]
        public int Rating { get; set; }

        public int GetDiscount(List<ProductSale> productSales)
        {
            double totalproducts = 0;
            List<ProductSale> PartnerProductSales = productSales.FindAll(x => x.PartnerId == Id);
            foreach (ProductSale PartnerProductSalesItem in PartnerProductSales)
            {
                totalproducts += PartnerProductSalesItem.Count;
            }
            bool Discount0 = totalproducts < 10000;
            bool Discount5 = totalproducts > 10000 && totalproducts < 50000;
            bool Discount10 = totalproducts > 50000 && totalproducts < 300000;
            bool Discount15 = totalproducts > 300000;
            if (Discount0)
                return 0;
            else if (Discount5)
                return 5;
            else if (Discount10)
                return 10;
            else
                return 15;
        }
    }
}
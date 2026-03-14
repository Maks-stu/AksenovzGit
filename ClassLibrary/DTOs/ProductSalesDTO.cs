using System;

namespace ClassLibrary.DTOs
{
    public class ProductSalesDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public double Count { get; set; }
        public string Production { get; set; }
        public double TotalPrice { get; set; }
    }
}
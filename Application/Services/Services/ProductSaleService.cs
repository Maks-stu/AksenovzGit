using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services.Interfaces;
using ClassLibrary.Classes;

namespace Application.Services.Services
{
    public class ProductSaleService : IProductSaleService
    {
        private readonly ApplicationContext _context;

        public ProductSaleService()
        {
            _context = new ApplicationContext();
        }

        public ProductSale CreateProductSale(ProductSale productSale)
        {
            // Убедитесь, что Id не установлен, или установите в null
            productSale.Id = 0; // или просто не присваивайте ничего

            _context.ProductSales.Add(productSale);
            _context.SaveChanges();

            Console.WriteLine("Create ProductSale " + productSale.Id);
            return productSale;
        }

        public ProductSale? GetProductSaleById(int id)
        {
            var entity = _context.ProductSales.Find(id);
            if (entity == null)
                return null;

            Console.WriteLine("Get ProductSale by ID " + entity.Id);
            return entity;
        }

        public List<ProductSale> GetProductSalesAll()
        {
            Console.WriteLine("Get all ProductSales");
            return _context.ProductSales.ToList();
        }

        public ProductSale UpdateProductSale(int id, ProductSale productSale)
        {
            var entity = _context.ProductSales.Find(id) ?? throw new Exception("ProductSale not found");

            entity.PartnerId = productSale.PartnerId;
            entity.ProductionId = productSale.ProductionId;
            entity.Count = productSale.Count;
            entity.SaleDate = productSale.SaleDate;
            entity.TotalPrice = productSale.TotalPrice;

            _context.SaveChanges();

            Console.WriteLine("Update ProductSale " + entity.Id);
            return entity;
        }

        public bool DeleteProductSale(int id)
        {
            var entity = _context.ProductSales.Find(id);
            if (entity == null)
                return false;

            _context.ProductSales.Remove(entity);
            _context.SaveChanges();

            Console.WriteLine("Delete ProductSale " + entity.Id);
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services.Interfaces;
using ClassLibrary.Classes;

namespace Application.Services.Services
{
    public class ProductionService : IProductionService
    {
        private readonly ApplicationContext _context;

        public ProductionService()
        {
            _context = new ApplicationContext(); // Можно добавить DI, если нужно
        }

        public Production CreateProduction(Production production)
        {
            var entity = production;
            entity.Id = _context.Productions.Any() ? _context.Productions.Max(p => p.Id) + 1 : 1;

            _context.Productions.Add(entity);
            _context.SaveChanges();
            Console.WriteLine("Create Production " + entity.Id);
            return entity;
        }

        public Production? GetProductionById(int id)
        {
            var entity = _context.Productions.Find(id);
            if (entity == null)
                return null;

            Console.WriteLine("Get Production by ID " + entity.Id);
            return entity;
        }

        public List<Production> GetProductionsAll()
        {
            Console.WriteLine("Get All Productions");
            return _context.Productions.ToList();
        }

        public Production UpdateProduction(int id, Production production)
        {
            var entity = _context.Productions.Find(id) ?? throw new Exception("Production not found");

            entity.Article = production.Article;
            entity.Name = production.Name;
            entity.Description = production.Description;
            entity.Photo = production.Photo;
            entity.MinPartnerPrice = production.MinPartnerPrice;
            entity.Length = production.Length;
            entity.Width = production.Width;
            entity.Height = production.Height;
            entity.NetWeight = production.NetWeight;
            entity.GrossWeight = production.GrossWeight;
            entity.QualityCertificate = production.QualityCertificate;
            entity.StandardNumber = production.StandardNumber;
            entity.CostPrice = production.CostPrice;

            _context.SaveChanges();
            Console.WriteLine("Update Production " + entity.Id);
            return entity;
        }

        public bool DeleteProduction(int id)
        {
            var entity = _context.Productions.Find(id);
            if (entity == null)
                return false;

            _context.Productions.Remove(entity);
            _context.SaveChanges();
            Console.WriteLine("Delete Production " + entity.Id);
            return true;
        }
    }
}
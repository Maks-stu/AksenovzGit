using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using ClassLibrary;
using ClassLibrary.Classes;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class ApplicationContext : DbContext
    {
        // Связи с таблицами базы данных
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }

        // Конструктор по умолчанию
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        // Конструктор для настройки через параметры
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        // Конфигурация подключения к базе данных
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;database=aksenov_16_03;Username=app;Password=123456789");
            }
        }

        // Модель базы данных (дополнительно, если есть настройки)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Можно добавить настройки моделей здесь, если потребуется
        }
    }
}
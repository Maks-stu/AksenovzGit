using ClassLibrary.Classes;

namespace Application.Services.Interfaces
{
    public interface IProductSaleService
    {
        ProductSale CreateProductSale(ProductSale productSale);
        List<ProductSale> GetProductSalesAll();
        ProductSale GetProductSaleById(int id);
        ProductSale UpdateProductSale(int id, ProductSale productSale);
        bool DeleteProductSale(int id);
    }
}
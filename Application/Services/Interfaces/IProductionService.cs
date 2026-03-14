using ClassLibrary.Classes;

namespace Application.Services.Interfaces
{
    public interface IProductionService
    {
        Production CreateProduction(Production production);
        List<Production> GetProductionsAll();
        Production GetProductionById(int id);
        Production UpdateProduction(int id, Production production);
        bool DeleteProduction(int id);
    }
}
using Altkom.Shopper.Domain.SearchCriterias;

namespace Altkom.Shopper.Domain;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product Get(int id);
    void Add(Product product);
    void Update(Product product);
    void Remove(int id);
    IEnumerable<Product> GetByColor(string color);
    IEnumerable<Product> Get(ProductSearchCriteria searchCriteria);

    Product GetByBarcode(string barcode);
}
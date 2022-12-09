using Altkom.Shopper.Domain.SearchCriterias;

namespace Altkom.Shopper.Domain;

// Interfejs uogólniony (szablon)
public interface IEntityRepository<TEntity>
    where TEntity : BaseEntity
{    
    IEnumerable<TEntity> GetAll();
    TEntity Get(int id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(int id);
}

public interface IProductRepository : IEntityRepository<Product>
{   
    IEnumerable<Product> GetByColor(string color);
    IEnumerable<Product> Get(ProductSearchCriteria searchCriteria);

    Product GetByBarcode(string barcode);
}

public interface ICustomerRepository : IEntityRepository<Customer>
{

}
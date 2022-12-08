using Altkom.Shopper.Domain;

namespace Altkom.Shopper.Infrastructure;

// add extension C# Extensions
public class InMemoryProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> products;

    public InMemoryProductRepository(IEnumerable<Product> products)
    {
        this.products = products.ToDictionary(p => p.Id);
    }

    public void Add(Product product)
    {
        throw new NotImplementedException();
    }

    public Product Get(int id)
    {
        if (products.TryGetValue(id, out Product product))
        {
            return product;
        }
        
        return null;
    }

    public IEnumerable<Product> GetAll()
    {
        return products.Values;
    }

    public IEnumerable<Product> GetByColor(string color)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }
}

using Altkom.Shopper.Domain;
using Altkom.Shopper.Domain.SearchCriterias;

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

    public IEnumerable<Product> Get(ProductSearchCriteria searchCriteria)
    {
        IQueryable<Product> query = products.Values.AsQueryable();

        if (searchCriteria.From.HasValue)
            query = query.Where(p => p.Price >= searchCriteria.From);

        if (searchCriteria.To.HasValue)
            query = query.Where(p => p.Price <= searchCriteria.To);            

        if (!string.IsNullOrWhiteSpace(searchCriteria.Color))
            query = query.Where(p => p.Color.Equals(searchCriteria.Color, StringComparison.OrdinalIgnoreCase));            

        var results = query.ToList();

        return results;
    }

    public IEnumerable<Product> GetAll()
    {
        return products.Values;
    }

    public Product GetByBarcode(string barcode)
    {
       return products.Values.SingleOrDefault(p=>p.Barcode == barcode);
    }

    public IEnumerable<Product> GetByColor(string color)
    {
        return products.Values.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
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

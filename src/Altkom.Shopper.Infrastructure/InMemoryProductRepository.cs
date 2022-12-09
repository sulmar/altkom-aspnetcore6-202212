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
        int id = products.Max(p => p.Key);

        product.Id = ++id;

        products.Add(product.Id, product);
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
        products.Remove(id);
    }

    public void Update(Product product)
    {
        Remove(product.Id);
        products.Add(product.Id, product);
    }
}


public class InMemoryEntityRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly IDictionary<int, TEntity> entities;

    public InMemoryEntityRepository(IEnumerable<TEntity> entities)
    {
        this.entities = entities.ToDictionary(p => p.Id);
    }

    public virtual void Add(TEntity entity)
    {
        int id = entities.Max(p => p.Key);

        entity.Id = ++id;

        entities.Add(entity.Id, entity);
    }

    public virtual TEntity Get(int id)
    {
        if (entities.TryGetValue(id, out TEntity entity))
        {
            return entity;
        }
        
        return null;
    }
    

    public virtual IEnumerable<TEntity> GetAll()
    {
        return entities.Values;
    }

    public virtual void Remove(int id)
    {
        entities.Remove(id);
    }

    public virtual void Update(TEntity entity)
    {
        Remove(entity.Id);
        entities.Add(entity.Id, entity);
    }
}

public class InMemoryCustomerRepository : InMemoryEntityRepository<Customer>, ICustomerRepository
{
    public InMemoryCustomerRepository(IEnumerable<Customer> entities) : base(entities)
    {
    }

    public override void Remove(int id)
    {
        var customer = Get(id);
        customer.IsRemoved = false;
    }
}


public class EmailMessageService : IMessageService
{
    public void Send(string message)
    {
        System.Console.WriteLine($"Send email {message}");
    }
}
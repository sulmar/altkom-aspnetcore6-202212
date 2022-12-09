namespace Altkom.Shopper.Domain;

public abstract class Base
{
    
}

public abstract class BaseEntity : Base
{
    public int Id { get; set; }
}


public class Product : BaseEntity
{    
    public string Name { get; set; }
    public string Barcode { get; set; }
    public decimal Price { get; set; }
    public string? Color { get; set; }
    public ProductSize? Size { get; set; }
    public bool IsRemoved { get; set; }
}

public enum ProductSize : byte
{
    S,
    M,
    L,
    XL
}
public class Payment : BaseEntity
{
    public decimal TotalAmount { get; set; }
}

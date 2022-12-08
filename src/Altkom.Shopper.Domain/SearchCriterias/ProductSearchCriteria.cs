namespace Altkom.Shopper.Domain.SearchCriterias;

public abstract class SearchCriteria
{

}

public class ProductSearchCriteria : SearchCriteria
{
    public decimal? From { get; set; }
    public decimal? To { get; set; }
    public string? Color {get ; set; }

    public ProductSearchCriteria(decimal? from, decimal? to, string? color)
    {
        From = from;
        To = to;
        Color = color;
    }
    

   


}
using System.Reflection;

public class ProductSearchCriteria
{
    public decimal? From { get; set; }
    public decimal? To { get; set; }
    public string? Color {get ; set; }

    public static ValueTask<ProductSearchCriteria?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        decimal.TryParse(context.Request.Query["from"], out decimal from);
        decimal.TryParse(context.Request.Query["to"], out decimal to);
        string color = context.Request.Query["color"];

        var result = new ProductSearchCriteria
        {
            From = from,
            To = to,
            Color = color            
        };

        return ValueTask.FromResult<ProductSearchCriteria?>(result);
    }
}
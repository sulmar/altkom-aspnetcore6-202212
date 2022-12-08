public class ProductIds
{
    public List<int> Ids = new List<int>();

    public ProductIds(List<int> ids)
    {
        Ids = ids;
    }

    public static bool TryParse(string value, out ProductIds? result)
    {
        var segments = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (segments.Length==0)
        {
            result = default;

            return false;
        }

        List<int> ids = new List<int>();

        foreach(var segment in segments)
        {
            if (int.TryParse(segment, out var id))
                ids.Add(id);
        }
        
        result = new ProductIds(ids);

        return true;        

    }
}
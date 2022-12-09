public static class EndpointRouteBuilderExtensions
{
    private static readonly string[] PatchVerbs = new [] { "PATCH" };
    private static readonly string[] HeadVerbs = new [] { "HEAD" };

    // Extension Method (metoda rozszerzająca)
    public static IEndpointConventionBuilder MapPatch(this IEndpointRouteBuilder endpoints,
        string pattern,
        Delegate requestDelegate)
    {
        return endpoints.MapMethods(pattern, PatchVerbs, requestDelegate);
    }

     public static IEndpointConventionBuilder MapHead(this IEndpointRouteBuilder endpoints,
        string pattern,
        Delegate requestDelegate)
    {
        return endpoints.MapMethods(pattern, HeadVerbs, requestDelegate);
    }
}
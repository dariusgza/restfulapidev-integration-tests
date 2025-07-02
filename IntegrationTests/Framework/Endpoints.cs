using IntegrationTests.Models;

namespace IntegrationTests.Framework
{
    internal static class Endpoints
    {
        public static readonly Uri Objects = new("objects", UriKind.Relative);

        public static Uri AddObject(ObjectsRequest request)
        {
            var name = request.Name;
            var data = request.Data;

            return string.IsNullOrEmpty(name) || string.IsNullOrEmpty(data)
                ? throw new ArgumentException("Name and Data must be provided.")
                : new Uri($"objects/{Uri.EscapeDataString(name)}/{Uri.EscapeDataString(data)}", UriKind.Relative);
        }
    }
}
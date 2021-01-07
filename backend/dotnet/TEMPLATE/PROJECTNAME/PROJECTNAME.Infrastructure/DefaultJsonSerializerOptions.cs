using System.Text.Json;

namespace PROJECTNAME.Infrastructure
{
    public static class DefaultJsonSerializerOptions
    {
        public static JsonSerializerOptions Options => new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }
}
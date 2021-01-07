using System.Text.Json;

namespace net_core_mssql.Infrastructure
{
    public static class DefaultJsonSerializerOptions
    {
        public static JsonSerializerOptions Options => new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }
}
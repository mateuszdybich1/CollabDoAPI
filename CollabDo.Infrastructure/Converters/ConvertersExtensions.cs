using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CollabDo.Web.Converters
{
    public static class ConvertersExtensions
    {
        public static void RegisterAllConverters(this JsonSerializerSettings settings)
        {
            if (settings.Converters == null)
            {
                settings.Converters = new List<JsonConverter>();
            }

            JsonConverter[] converters = GetConverters();

            foreach (JsonConverter item in converters)
            {
                settings.Converters.Add(item);
            }
        }

        public static JsonConverter[] GetConverters()
        {
            return new JsonConverter[]
            {
                new StringEnumConverter(),
                new UnixMsDateTimeConverter()
            };
        }
    }
}

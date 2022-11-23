using System.Text.Json;

namespace FuelAccounting.DataAccess
{
    public static class JsonHelper
    {
        public static T Read<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var obj = JsonSerializer.Deserialize<T>(json);
            if (obj == null)
            {
                throw new NullReferenceException($"{nameof(T)} cannot be deserialized.");
            }
            return obj;
        }

        public static void Write<T>(T obj, string filePath)
        {
            if (obj == null)
            {
                throw new NullReferenceException($"{nameof(T)} cannot be serialized.");
            }
            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static async Task<T> ReadAsync<T>(string filePath)
        {
            var json = await File.ReadAllTextAsync(filePath);
            var obj = JsonSerializer.Deserialize<T>(json);
            if (obj == null)
            {
                throw new NullReferenceException($"{nameof(T)} cannot be deserialized.");
            }
            return obj;
        }

        public static async Task WriteAsync<T>(T obj, string filePath)
        {
            if (obj == null)
            {
                throw new NullReferenceException($"{nameof(T)} cannot be serialized.");
            }
            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
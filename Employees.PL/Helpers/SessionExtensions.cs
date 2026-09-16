using System.Text.Json;

namespace Employees.PL.Helpers
{
    public static class SessionExtensions
    {
        // Store any object in Session as JSON
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Retrieve an object from Session; returns default(T) (null) if not found
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
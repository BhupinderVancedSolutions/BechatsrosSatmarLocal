using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.Helper
{
    public static class Session
    {
        public static void SetObjectAsJson(this ISession session, string key, object value, string secret)
        {
            session.Set(key, EncryptionHelper.EncryptStringToByteArray(JsonConvert.SerializeObject(value), secret));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key, string secret)
        {
            session.TryGetValue(key, out byte[] value);
            return value == null ? default : JsonConvert.DeserializeObject<T>(EncryptionHelper.DecryptByteArrayToString(value, secret));
        }
    }
}

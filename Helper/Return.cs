using System.Collections.Generic;

namespace nitipApi.Helper
{
    public static class Return
    {
        public static Dictionary<string, object> TrueReturn(string key, object data)
        {
            var result = new Dictionary<string, object>();
            result.Add("status", true);
            result.Add(key, data);
            return result;
        }

        public static Dictionary<string, object> FalseReturn(string key, object data)
        {
            var result = new Dictionary<string, object>();
            result.Add("status", false);
            result.Add(key, data);
            return result;
        }

    }
}

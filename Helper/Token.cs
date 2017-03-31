using Jwt;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace nitipApi.Helper
{
    public static class Token
    {
        public static string ApiKey()
        {
            return "Hendrike9";
        }

        public static string jwtData(HttpRequest request)
        {

            var secret = string.Empty;
            string token = string.Empty;
            var result = new Dictionary<string, object>();

            if (!request.Headers.ContainsKey("Authorization"))
            {
                return "missing token";
            }
            else if (!request.Headers.ContainsKey("API-Key"))
            {
                return "missing api-key";
            }
            else
            {
                if (request.Headers["API-Key"].Equals(Helper.Token.ApiKey()))
                {
                    token = request.Headers["Authorization"].FirstOrDefault();
                    secret = request.Headers["API-Key"].FirstOrDefault();
                    try
                    {
                        var data = JsonWebToken.Decode(token, secret);
                        return data;
                    }
                    catch (SignatureVerificationException)
                    {
                        return "invalid token";
                    }
                }
                else
                {
                    return "wrong api-key";
                }
            }


        }

    }
}

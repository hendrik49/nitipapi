using Jwt;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using nitipApi.Models;
using Newtonsoft.Json.Linq;
using nitipApi.Repositroy;

namespace nitipApi.Helper
{
    public static class Token
    {
       public static string ApiKey()
        {
            return "Hendrike9";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Global
{
    public static class GlobalVariables
    {
        public static HttpClient WebApiClient = new();


        static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44312/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

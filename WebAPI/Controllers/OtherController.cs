using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{


    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OtherController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task<List<String>> OtherAPI()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<String>));

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

           // var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            //var streamTask = client.GetStreamAsync("http://localhost:3002/api/OtherValues");
            var streamTask = client.GetStreamAsync("/webapiother");           var results = serializer.ReadObject(await streamTask) as List<String>;
            return results;
        }

        [HttpGet]
        public IEnumerable<String> Get()
        {
            var results = OtherAPI().Result;

            return results;
        }
    }
}
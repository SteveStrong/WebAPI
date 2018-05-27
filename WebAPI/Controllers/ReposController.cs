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
    public class ReposController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task<List<Repository>> RepoAPI()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

           // var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            //var streamTask = client.GetStreamAsync("http://localhost:3002/api/OtherValues");
            
            //var streamTask = client.GetStreamAsync("http://localhost:4001/webapiother");           
            
            var streamTask = client.GetStreamAsync("https://arrows.azurewebsites.net/api/GitHub");
            var results = serializer.ReadObject(await streamTask) as List<Repository>;
            return results;
        }

        [HttpGet]
        public IEnumerable<Repository> Get()
        {
            var results = RepoAPI().Result;

            return results;
        }
    }
}
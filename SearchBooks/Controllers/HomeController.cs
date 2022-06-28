using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace SearchPokemons.Controllers
{
    public class HomeController : Controller
    {
        private readonly ElasticClient _client;
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, ElasticClient client,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _client = client;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> IndexHttp(string query)
        {
            List<Pokemon> results = new List<Pokemon>();

            var httpClient = _httpClientFactory.CreateClient("ElasticSearch");

            HttpResponseMessage? response = null;

            if (string.IsNullOrWhiteSpace(query))
            {
                response = await httpClient.GetAsync
                    ("/pokemon/_search?from=400&size=200");
            }
            else
            {
                var queryjson = "{\"query\"" +
                    ": {\"term\": " +
                    "{\"Generation\": \"" + query+ "\"}}}";

                var contentData = 
                    new StringContent(queryjson, Encoding.UTF8, "application/json");

                response = await httpClient.PostAsync
                    ("/pokemon/_search?size=200", contentData);

            }
                

            response.EnsureSuccessStatusCode();

            // Odczytanie JSON z odpowiedzi
            var content = await response.Content.ReadAsStringAsync();

            JObject jo = JObject.Parse(content);

            foreach (var item in jo["hits"]["hits"])
            {
                var pokemonjson = item["_source"];

                Pokemon pokemon = pokemonjson.ToObject<Pokemon>();

                results.Add(pokemon);
            }

            return View(results);
        }



        public IActionResult Index(string query)
        {
            ISearchResponse<Pokemon> results;
            if (!string.IsNullOrWhiteSpace(query))
            {
                results = _client.Search<Pokemon>(s => s
                    .Index("pokemon")
                    .Size(200)
                    .Query(q => q
                        .Term(t => t
                            .Field(f => f.Generation)
                            .Value(query)
                        )
                    )
                );
            }
            else
            {
                results = _client.Search<Pokemon>
                    (s => s.Index("pokemon").Size(1000));
            }

            var pokemon = results;

            return View(results);
        }
    }


}

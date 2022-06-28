//using Microsoft.AspNetCore.Mvc;


//namespace SenderAPIToElasticSearch.Controllers
//{
//    [Route("api/[controller]")]
//    public class ProductController : Controller
//    {
//        private ESClientProvider _esClientProvider;
//        public ProductController(ESClientProvider esClientProvider)
//        {
//            _esClientProvider = esClientProvider;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] Product product)
//        {
//            product.Id = Guid.NewGuid();

//            var res = await _esClientProvider.Client.IndexAsync(product);
//            if (!res.IsValid)
//            {
//                throw new InvalidOperationException(res.DebugInformation);
//            }

//            return Ok();
//        }

//        [HttpGet("find")]
//        public async Task<IActionResult> Find(string term)
//        {
//            var res = await _esClientProvider.Client.SearchAsync<Product>(x => x
//                .Query(q => q.
//                    SimpleQueryString(qs => qs.Query(term))));
//            if (!res.IsValid)
//            {
//                throw new InvalidOperationException(res.DebugInformation);
//            }

//            return Json(res.Documents);
//        }
//    }


//    public class Product
//    {
//        public Guid Id { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }
//        public string[] Tags { get; set; }
//    }
//}

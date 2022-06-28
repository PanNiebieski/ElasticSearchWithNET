using Elasticsearch.Net;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();


var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
var settings = new ConnectionSettings(pool).DefaultIndex("pokemons");
settings.DefaultFieldNameInferrer(p => p);
var client = new ElasticClient(settings);



builder.Services.AddSingleton(client);


builder.Services.AddHttpClient("ElasticSearch", client =>
{
    client.BaseAddress = new Uri("http://localhost:9200/");
    client.Timeout = new TimeSpan(0, 0, 30);
    client.DefaultRequestHeaders.Clear();
});



var app = builder.Build();













// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

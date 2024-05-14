using ArticlesApi.DAL;
using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using Asp.Versioning;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Articles
builder.Services.Configure<ArticleDbSettings>(
    builder.Configuration.GetSection(nameof(ArticleDbSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<ArticleDbSettings>>().Value);
builder.Services.AddSingleton<ArticlesContext>();
builder.Services.AddSingleton<IArticlesRepository, ArticlesRepository>();

//Swagger
builder.Services.AddSwaggerGen();

//Caching
builder.Services.AddMemoryCache();

//Controllers
builder.Services.AddControllersWithViews();

//Versioning
builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = false;
        options.ReportApiVersions = false;
    }).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    })
    .AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use Swagger and Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Articles API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

// Redirect root URL to Swagger UI
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

//TODO: Configure certificate usage using X509
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

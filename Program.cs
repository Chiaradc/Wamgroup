using Elogic.Sitefinity.Infrastructure;
using Elogic.Sitefinity.Infrastructure.Settings;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Progress.Sitefinity.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

//inserimento dei servizi Sitefinity
builder.Services.AddSitefinityServices();

//inserimento dei servizi Elogic
builder.Services.AddElogicServices(builder.Configuration);
builder.Services.AddElogicSitefinityServices(builder.Environment, builder.Configuration);

var app = builder.Build();

app.UseResponseCompression();


var cacheMaxAgeOneWeek = builder.Configuration.GetValue<int>("StaticFiles:MaxAge");
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => ctx.Context.Response.Headers.Add(HeaderNames.CacheControl, $"public, max-age={cacheMaxAgeOneWeek}"),
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", @"css")),
    RequestPath = new PathString("/css")
});
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => ctx.Context.Response.Headers.Add(HeaderNames.CacheControl, $"public, max-age={cacheMaxAgeOneWeek}"),
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", @"js")),
    RequestPath = new PathString("/js")
});
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => ctx.Context.Response.Headers.Add(HeaderNames.CacheControl, $"public, max-age={cacheMaxAgeOneWeek}"),
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", @"svg")),
    RequestPath = new PathString("/svg")
});
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => ctx.Context.Response.Headers.Add(HeaderNames.CacheControl, $"public, max-age={cacheMaxAgeOneWeek}"),
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", @"lib")),
    RequestPath = new PathString("/lib")
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

//inserimento di Sitefinity
app.AddSitefinity();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

   endpoints.MapControllerRoute(
       name: "Downolad",
       pattern: "{controller}/{action}"
   );
});

app.Run();
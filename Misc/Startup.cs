using Elogic.Wamgroup.Sito2023.Caching.Imlementation;
using Elogic.Wamgroup.Sito2023.Caching.Interfaces;
using Elogic.Wamgroup.Sito2023.Caching.Settings;
using Elogic.Wamgroup.Sito2023.NetCore.Localization;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupSearchResults;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.FormWidgets;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Facets;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;

namespace Elogic.Wamgroup.Sito2023.NetCore.Misc
{
    public static class ServicesRegistrations
    {
        public static void AddSitefinityServices(this IServiceCollection services)
        {
            services.AddSitefinity();
            services.AddViewComponentModels();
            services.AddFormViewComponentModels();
        }

        public static void AddElogicServices(this IServiceCollection services, IConfiguration conf)
        {
            services.AddHttpContextAccessor();
            services.Configure<LocalizationServiceSettings>(conf.GetSection(nameof(LocalizationServiceSettings)));
            services.Configure<CacheSettings>(conf.GetSection(nameof(CacheSettings)));
            services.AddSingleton<LocalizationHelperService, LocalizationHelperService >();
            services.AddScoped<DocumentsService, DocumentsService>();
            services.AddScoped<NewsService, NewsService>();
            services.AddScoped<RestClientService, RestClientService>();
            services.AddScoped<DynamicTypeHelper, DynamicTypeHelper>();
            services.AddScoped<IRequestPreparation, RequestPreparation>();
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddScoped<ISearchResultsModel, SearchResultsModel>();
            //services.AddScoped<IRestClient, RestClient>();
            //services.AddScoped<EventsRestClientFactory>();
        }
    }

    public static class WebApplicationRegistrations
    {
        public static void AddSitefinity(this WebApplication app)
        {
            app.UseSitefinity();
        }
    }
}

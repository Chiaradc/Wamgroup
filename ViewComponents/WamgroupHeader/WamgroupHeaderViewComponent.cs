using System.Security.Claims;
using Elogic.Sitefinity.Infrastructure.GlobalizationServices;
using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupHeader;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupHeader;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupHeader
{
    [SitefinityWidget(Title = "Wamgroup Header", Category = WidgetCategory.Layout, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Header", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/header.jpg")]
    [ScriptFile("/css/widgets/filtroSettori.min.css", true, true, true)]
    public class WamgroupHeaderViewComponent : ViewComponent
    {
        private readonly RestClientService restClientService;
        private readonly IGeolocationService geolocationService;
        private readonly ICountriesService countriesService;
        private readonly IRequestContext requestContext;

        public WamgroupHeaderViewComponent(
            RestClientService restClientService,
            CustomScriptService customScriptService,
            IGeolocationService geolocationService,
            ICountriesService countriesService,
            IRequestContext requestContext)
        {
            this.geolocationService = geolocationService;
            this.countriesService = countriesService;
            this.restClientService = restClientService;
            this.requestContext = requestContext;
            customScriptService.RegisterType(this.GetType());
        }


        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupHeaderEntity> context)
        {
            var model = new WamgroupHeaderViewModel();

            //var country = geolocationService.GetCountryCode("128.101.101.101");
            var country = geolocationService.GetCountryTwoLettersCode();
            model.Country = countriesService.GetCountryByTwoLettersCode(country).NativeName;

            if (requestContext.User != null && requestContext.User.Identity.IsAuthenticated)
            {
                model.User = requestContext.User.Identity.Name;
                var claim = (requestContext.User.Identity as ClaimsIdentity).Claims.ToList();//.Where(x => x.Type == ClaimTypes.Surname).ToList();
                model.IsAuthenticated = true;
            }
            else
            {
                var id = context.Entity.LoginPage?.ItemIdsOrdered?.FirstOrDefault();
                if (id != null)
                {
                    var nodeDto = await restClientService.GetItem<PageNodeDto>(id);
                    model.LoginPageUrl = nodeDto.ViewUrl;
                }
                model.IsAuthenticated = false;
            }

            

            return View("Default", model);
        }
    }
}

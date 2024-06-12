using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupArticoli;
using Elogic.Wamgroup.Sito2023.NetCore.Localization;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupArticoli;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using System.Globalization;


namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupArticoli
{
    //[SitefinityWidget(Title = "Wamgroup Magazzino", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/section.jpg")]
    public class WamgroupMagazzinoViewComponent : ViewComponent
   {

      private readonly IRestClient restClient;
      private readonly LocalizationHelperService localizationHelperService;
      private readonly IRequestContext requestContext;

      internal const string DetailItemPrefix = "magazzino";

      public WamgroupMagazzinoViewComponent(IRestClient restClient, LocalizationHelperService localizationHelperService, IRequestContext requestContext)
      {
         this.restClient = restClient;
         this.localizationHelperService = localizationHelperService;
         this.requestContext = requestContext;
      }


      [HttpPost]
      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupMagazzinoEntity> context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         

         var data = await restClient.GetItems<RigaMagazzino>(new GetAllArgs()
         {
            
            Culture = CultureInfo.CurrentCulture.Name
         });


         foreach (var item in data.Items)
         {

            var articoli = item.GetValue<IEnumerable<int>>("Articolo");
            var qta = item.GetValue<int>("Quantita");

         }


         var articoliMagazzino = data.Items;

         var loc = localizationHelperService.Localize("Test");

         context.Entity.RigheMagazzino = articoliMagazzino;
         WamgroupMagazzinoViewModel widgetViewModel = new WamgroupMagazzinoViewModel
         {
            RigheMagazzino = articoliMagazzino,
            Context = context
         };
         return View("Default", widgetViewModel);

      }

   }
}

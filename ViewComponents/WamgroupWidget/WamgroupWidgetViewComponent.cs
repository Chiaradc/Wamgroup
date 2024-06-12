using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupWidget;
using Elogic.Wamgroup.Sito2023.NetCore.Localization;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupWidget;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupWidget
{
    //[SitefinityWidget(Title = "Wamgroup widget", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/section.jpg")]
    public class WamgroupWidgetViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;
      private readonly LocalizationHelperService localizationHelperService;
      private readonly IRequestContext requestContext;

      internal const string DetailItemPrefix = "articoli";

      

      public WamgroupWidgetViewComponent(RestClientService restClientService, LocalizationHelperService localizationHelperService, IRequestContext requestContext)
      {
         this.restClientService = restClientService;
         this.localizationHelperService = localizationHelperService;
         this.requestContext = requestContext;
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupWidgetEntity> context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         //var str = RouteData.Values["Id"];


         //var art = await restClient.GetItem<Articolo>(new GetItemArgs()
         //{
         //   Id = "43563af9-99e0-4a05-b9fd-bb8524e6da13",
         //   Culture = "en",
         //});

         //if (art != null)
         //{
         //   art.Costo = 450;
         //   await restClient.UpdateItem(new UpdateArgs()
         //   {
         //      Culture = "en",
         //      Type = "Telerik.Sitefinity.DynamicTypes.Model.Moduloprova.Articolo",
         //      Id = art.Id,
         //      Data = art
         //   });

         //}


         //var resources = ResourceManager.GetManager();
         //var str = resources.GetResourceOrEmpty(Thread.CurrentThread.CurrentCulture, "", "TestPage").Value;

         if (context.State.TryGetValue(RequestPreparation.SelectedItem, out object articolo))
         {
            return this.View("Detail", articolo);
         }

         var orderBy = new OrderBy
         {
            Name = nameof(Articolo.Descrizione),
            Type = OrderType.Ascending
         };


         //var data = await restClient.GetItems<Articolo>(new GetAllArgs()
         //{
         //   Culture = "en",//CultureInfo.CurrentCulture.Name
         //   Filter = new CombinedFilter
         //   {
         //      Operator = CombinedFilter.LogicalOperators.And,
         //      ChildFilters = new[]
         //      {
         //         new  FilterClause
         //         {
         //            FieldName = nameof(Articolo.Codice),
         //            Operator = FilterClause.Operators.Equal,
         //            FieldValue = "100"

         //         },
         //         new  FilterClause
         //         {
         //            FieldName = nameof(Articolo.Descrizione),
         //            Operator = FilterClause.StringOperators.Contains,

         //            FieldValue = "mo"

         //         }
         //      }
         //   },
         //   OrderBy = new[] { orderBy },
         //});
         var articoli = await restClientService.GetItems<Articolo>();

         var loc = localizationHelperService.Localize("Test");

         WamgroupWidgetViewModel widgetViewModel = new WamgroupWidgetViewModel
         {
            Codice = context.Entity.Codice,
            Articoli = articoli,
            Context = context
         };
         return View("Default", widgetViewModel);

      }
   }
}

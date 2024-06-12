using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupArticoli;
using Elogic.Wamgroup.Sito2023.NetCore.Localization;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupArticoli;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Web;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Filters;
using System.Globalization;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupArticoli
{
    //[SitefinityWidget(Title = "Wamgroup Articoli", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/section.jpg")]
    
    public class WamgroupArticoliViewComponent : ViewComponent
   {
      private readonly IRestClient restClient;
      private readonly LocalizationHelperService localizationHelperService;
      private readonly IRequestContext requestContext;

      internal const string DetailItemPrefix = "articoli";

      public WamgroupArticoliViewComponent(IRestClient restClient, LocalizationHelperService localizationHelperService, IRequestContext requestContext)
      {
         this.restClient = restClient;
         this.localizationHelperService = localizationHelperService;
         this.requestContext = requestContext;
      }

      [HttpPost]
      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupArticoliEntity> context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         if (!context.State.TryGetValue(RequestPreparation.SelectedAction, out object objAction))
         {
            throw new Exception("Null action");
         }
         else
         {
            var action = (objAction as string) ?? "";
            if (action.ToLowerInvariant() == "list")
            {
               var orderBy = new OrderBy
               {
                  Name = nameof(Articolo.Descrizione),
                  Type = OrderType.Ascending
               };

               string strFiltro = "";
               if (Request.Query["CercaFiltro"] != "")
               {
                  strFiltro = Request.Query["CercaFiltro"].ToString() ?? "";
               }



               var data = await restClient.GetItems<Articolo>(new GetAllArgs()
               {
                  Culture = CultureInfo.CurrentCulture.Name,

                  Filter = new CombinedFilter
                  {
                     Operator = CombinedFilter.LogicalOperators.Or,
                     ChildFilters = new[]
                     {
                  new  FilterClause
                  {
                     FieldName = nameof(Articolo.Codice),
                     Operator = FilterClause.StringOperators.Contains,
                     FieldValue = strFiltro

                  },
                  new  FilterClause
                  {
                     FieldName = nameof(Articolo.Descrizione),
                     Operator = FilterClause.StringOperators.Contains,

                     FieldValue = strFiltro

                  }
               }
                  }
               });
               var articoli = data.Items;

               context.Entity.Articoli = articoli;
               WamgroupArticoliViewModel widgetViewModel = new WamgroupArticoliViewModel
               {
                  CercaFiltro = strFiltro,
                  Articoli = articoli,
                  Context = context
               };
               return View("Default", widgetViewModel);
            }
            else
            {
               var art = new Articolo();
               IList<VarianteArticolo> varianti = new List<VarianteArticolo>();
               if (context.State.TryGetValue(RequestPreparation.SelectedItem, out object articolo))
               {
                  art = articolo as Articolo;
                  if (art?.Id != null)
                  {
                     var dataVar = await restClient.GetItems<VarianteArticolo>(new GetAllArgs()
                     {

                        Culture = CultureInfo.CurrentCulture.Name,
                        Filter = new FilterClause
                        {
                           FieldName = nameof(VarianteArticolo.ParentId),
                           Operator = FilterClause.Operators.Equal,
                           FieldValue = art.Id

                        }
                     });
                     varianti = dataVar.Items;

                  }
               }
               WamgroupArticoliDetailViewModel widgetDetailViewModel = new WamgroupArticoliDetailViewModel
               {
                  Articolo = art,
                  VariantiArticolo = varianti,
                  Context = context
               };

               var updateType = string.Empty;
               if (context.State.TryGetValue(RequestPreparation.UpdateType, out object objUpdateType))
               {
                  updateType = (objUpdateType as string) ?? string.Empty;
               }

               if (updateType.ToLowerInvariant() == "update")
               {
                  if (art?.Id != null)
                  {
                     await restClient.UpdateItem(new UpdateArgs()
                     {

                        Culture = CultureInfo.CurrentCulture.Name,
                        Type = "Telerik.Sitefinity.DynamicTypes.Model.Moduloprova.Articolo",
                        Id = art.Id,
                        Data = art
                     });
                  }
                  else
                  {
                     art = await restClient.CreateItem(art);
                     widgetDetailViewModel.Articolo = art;
                  }
                  return this.View("Detail", widgetDetailViewModel);
               }
               else if (updateType.ToLowerInvariant() == "delete")
               {

               }

               if (action.ToString().ToLowerInvariant() == "edit") // || action.ToString().ToLowerInvariant() == "newitem")
               {

                  return this.View("Edit", widgetDetailViewModel);
               }
               

               return this.View("Detail", widgetDetailViewModel);



            }
         }





      }



   }
}

using Elogic.Wamgroup.Sito2023.Models.SdkItems.ModuloProva;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupArticoli;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupNews;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupProductDetail;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupProductList;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupWidget;
using Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupArticoli;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.Preparations;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;


namespace Elogic.Wamgroup.Sito2023.NetCore.Misc
{
    public class RequestPreparation : IRequestPreparation
   {
      public const string SelectedItem = "selected-item";
      public const string SelectedAction = "selected-action";
      public const string UpdateType = "update-type";

      private IRestClient restClient;
      public RequestPreparation(IRestClient restClient)
      {
         this.restClient = restClient;
      }


      public async Task Prepare(PageModel pageModel, IRestClient restClient, HttpContext context)
      {
         // get the category selector widget
         var contentViewWidget = pageModel.AllViewComponentsFlat.FirstOrDefault(x => typeof(IViewComponentContext<WamgroupWidgetEntity>).IsAssignableFrom(x.GetType()) || typeof(IViewComponentContext<WamgroupArticoliEntity>).IsAssignableFrom(x.GetType()));
         if (contentViewWidget != null)
         {
            if (pageModel.UrlParameters.Count > 0)
            {
               var parameter = pageModel.UrlParameters[0];

               if (parameter.Equals(WamgroupArticoliViewComponent.DetailItemPrefix, StringComparison.OrdinalIgnoreCase))
               {
                  // /Articoli/{idArticolo}|newItem/list/[Edit|Delete]

                  var articolo = pageModel.UrlParameters.Count > 1 ? pageModel.UrlParameters[1] : "";
                  IList<Articolo> itemsResult = new List<Articolo>();
                  //if (articolo == "newItem")
                  //{
                  //   contentViewWidget.State.Add(SelectedAction, "newItem");
                  //   pageModel.MarkUrlParametersResolved();
                  //}
                  //else
                  if (articolo == "list")
                  {
                     contentViewWidget.State.Add(SelectedAction, "list");
                     pageModel.MarkUrlParametersResolved();
                  }
                  else
                  {
                     Articolo itemArticolo = new Articolo();
                     if (articolo != "newItem")
                     {


                        itemsResult = await restClient.GetItemsByIds<Articolo>(

                           new GetByIdsArgs()
                           {
                              Culture = pageModel.Culture.Name,//CultureInfo.CurrentCulture.Name
                              Ids = new string[] { articolo },

                           });

                        if (itemsResult.Count >= 1)
                        {
                           itemArticolo = itemsResult[0];
                        }
                     }
                     if (context.Request.Query["update"].ToString() == "update")
                     {
                        itemArticolo.Codice = context.Request.Query["Articolo.Codice"].ToString();
                        itemArticolo.Descrizione = context.Request.Query["Articolo.Descrizione"].ToString();
                        if (decimal.TryParse(context.Request.Query["Articolo.Costo"], out decimal costo))
                           itemArticolo.Costo = costo;
                        contentViewWidget.State.Add(UpdateType, "update");
                     }
                     else if (context.Request.Query["delete"].ToString() == "delete")
                     {
                        contentViewWidget.State.Add(UpdateType, "delete");

                     }
                     //else
                     //{
                     //   var action = pageModel.UrlParameters.Count > 2 ? pageModel.UrlParameters[2] : "";
                     //   contentViewWidget.State.Add(SelectedAction, action);
                     //}

                     var action = pageModel.UrlParameters.Count > 2 ? pageModel.UrlParameters[2] : "";
                     contentViewWidget.State.Add(SelectedAction, action);

                     var item = itemArticolo;
                     contentViewWidget.State.Add(SelectedItem, item);
                     pageModel.MarkUrlParametersResolved();

                  }


               }
            }
            else
            {
               contentViewWidget.State.Add(SelectedAction, "list");
               pageModel.MarkUrlParametersResolved();
            }
         }

         contentViewWidget = pageModel.AllViewComponentsFlat.FirstOrDefault(x => typeof(IViewComponentContext<WamgroupNewsEntity>).IsAssignableFrom(x.GetType()) || typeof(IViewComponentContext<WamgroupProductDetailEntity>).IsAssignableFrom(x.GetType()));
         if (contentViewWidget != null)
         {

            if (pageModel.UrlParameters.Count == 0)
            {
               contentViewWidget.State.Add(SelectedAction, "list");
               pageModel.MarkUrlParametersResolved();
            }
            else
            {
               contentViewWidget.State.Add(SelectedItem, pageModel.UrlParameters[0]);
               contentViewWidget.State.Add(SelectedAction, "detail");
               pageModel.MarkUrlParametersResolved();
            }
         }
      }
   }
}

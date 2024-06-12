using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupDownload;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupDownload;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupDownload
{
    [SitefinityWidget(Title = "Wamgroup Download", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Download", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/download.jpg")]
   [ScriptFile("/css/widgets/documentdownload.css", true, true, true)]
   [ScriptFile("/js/widgets/documentdownload.js", false, true, true)]
   public class WamgroupDownloadViewComponent : ViewComponent
   {

      
      private readonly DocumentsService documentsService;

      public WamgroupDownloadViewComponent(CustomScriptService customScriptService, DocumentsService documentsService) 
      {
         
         customScriptService.RegisterType(this.GetType());
         this.documentsService = documentsService;
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupDownloadEntity> context)
      {
         var topN = 0;
         var mostraCorrelazione = false;
         int anno = 0;
         string categoria = "";
         if (context is null)
         {
            throw new ArgumentNullException(nameof(context));
         }
         WamgroupDownloadViewModel model = new WamgroupDownloadViewModel();

         // Retrieves the node alias paths of the selected pages from the 'PagePaths' property
         string selectedPagePaths = string.Empty;
         string selectedPageId = string.Empty;
         var id = context.Entity.PagePaths?.ItemIdsOrdered?.FirstOrDefault();
         //if (id != null)
         //{

         //   //var bb = await restClient.GetItem<LibraryDto>(new GetItemArgs
         //   //{
         //   //   Type = RestClientContentTypes.Libraries,

         //   //   Id = id,
         //   //   Fields = new[] { "Title" },
         //   //   Culture = "it",
         //   //}).ConfigureAwait(true);


            
         //   var dd = await restClient.GetItems<LibraryDto>(new GetAllArgs
         //   {
               
         //      Culture = CultureInfo.CurrentCulture.Name,
         //      Filter = new FilterClause
         //      {
         //         FieldName = nameof(LibraryDto.Title),
         //         Operator = FilterClause.StringOperators.Contains,
         //         FieldValue = " "

         //      }
         //   });
         //   var ee = dd.Items;
         //   var documentLibrary = await restClient.GetItem<LibraryDto>(new GetItemArgs
         //   {
         //      Type = RestClientContentTypes.Libraries,
         //      Id = id,
         //      Fields = new[] { "Title", "Id", "Description" },
         //      Culture = "it",
         //   }).ConfigureAwait(true);
            
         //   selectedPagePaths = documentLibrary.GetDefaultUrl();
         //   selectedPageId = documentLibrary.Id.ToString();
         //}

         

         if (context != null)
         {
            topN = context.Entity.TopN;
            mostraCorrelazione = context.Entity.MostraRelazioniConDocumentoCorrente;
            model.MostraReadMore = context.Entity.MostraReadMore;
            model.MostrFiltro = context.Entity.MostrFiltro;
            model.MostrCategorieFiltro = context.Entity.MostrCategorieFiltro;
            model.NascondiNessunRisultato = context.Entity.NascondiNessunRisultato;
            //verifico se devo impostare il filtro
            if (context.Entity.MostraReadMore)
            {
               model.Anni = await documentsService.GetAnniPerDocumenti(id);
               model.Categorie = await documentsService.GetCategoriePerDocumenti(id);
               int.TryParse(model.Anni.FirstOrDefault() ?? "", out anno);
               model.AnnoSelezionato = anno.ToString();
               model.CategoriaSelezionata = string.Empty;
               model.CategoriaSelezionataTesto = "Categoria"; //ResHelper.GetString("custom.Topic");
               //model.UrlCorrente = DocumentURLProvider.GetUrl(currentPageService.PageContext().CurrentDocument);
               model.Folder = id;

               //verifico se ci sono i parametri
               if (Request.Query["year"].Any())
               {
                  int.TryParse(Request.Query["year"].First(), out anno);
                  model.AnnoSelezionato = anno.ToString();
               }
               if (Request.Query["category"].Any())
               {
                  categoria = Request.Query["category"].First() ?? string.Empty;
                  model.CategoriaSelezionata = categoria;
                  model.CategoriaSelezionataTesto = model.Categorie[categoria];
               }
            }
         }
         model.TopN = topN;


         // TODO: Widget Download, Sistemare filtri
         //var documents = documentRepository.GetDownloadableDocuments(folder: selectedPagePaths, topN: topN, mostraCorrelazioniConDocumentoCorrente: mostraCorrelazione, anno: anno, categoria: categoria);
         var documents = await documentsService.GetDownlodableDocuments(0, topN, anno, categoria, id);
         model.Documenti = documents.ToList();
         return View("Default", model);
      }

      
   }
}

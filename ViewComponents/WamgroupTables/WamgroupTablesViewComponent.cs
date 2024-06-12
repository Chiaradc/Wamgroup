using System.Text.Json;
using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTables;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTables;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using static System.Collections.Specialized.BitVector32;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupTables
{
   //[SitefinityWidget(Title = "Wamgroup Tables", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Tables", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/section.jpg")]
   [ScriptFile("/css/widgets/tables.css", true, true, true)]
   [ScriptFile("/js/widgets/tables-be.js", false, true, true)]
   public class WamgroupTablesViewComponent : ViewComponent
   {
      
     
      public WamgroupTablesViewComponent(CustomScriptService customScriptService)
      {
         customScriptService.RegisterType(this.GetType());
      }

      public IViewComponentResult Invoke(IViewComponentContext<WamgroupTablesEntity> context)
      {
         if (context is null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         var model = new WamgroupTablesViewModel();

         if (string.IsNullOrWhiteSpace(context.Entity.TableContent))
         {
            model.Headers.Add(new WamgroupTablesRowViewModel());

            for (int colIndex = 0; colIndex < context.Entity.ColNum; colIndex++)
            {
               model.Headers[0].Cells.Add(new WamgroupTablesCellViewModel());
            }

            for (int rowIndex = 0; rowIndex < context.Entity.RowNum; rowIndex++)
            {
               var row = new WamgroupTablesRowViewModel();

               for (int columnIndex = 0; columnIndex < context.Entity.ColNum; columnIndex++)
               {
                  row.Cells.Add(new WamgroupTablesCellViewModel());
               }

               model.Rows.Add(row);
            }
         }
         else
         {
            var existingModel = JsonSerializer.Deserialize<WamgroupTablesViewModel>(context.Entity.TableContent);

            var existingHeaderRows = existingModel?.Headers;

            for (int rowIndex = 0; rowIndex < existingHeaderRows?.Count; rowIndex++)
            {
               model.Headers.Add(existingHeaderRows[rowIndex]);
            }

            var existingRows = existingModel?.Rows;

            for (int rowIndex = 0; rowIndex < existingRows?.Count; rowIndex++)
            {
               model.Rows.Add(existingRows[rowIndex]);
            }
         }

         context.Entity.TableContent = JsonSerializer.Serialize(model);

         
         return View("Default", context);
      }
   }
}

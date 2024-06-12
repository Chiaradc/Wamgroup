using System.Text.Json;
using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupTabs;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupTabs;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using static System.Collections.Specialized.BitVector32;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupTabs
{
   [SitefinityWidget(Title = "Wamgroup Tabs", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Tabs", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/tabs.jpg")]
   [ScriptFile("/css/formcomponents/tabseditor.css", true, false)]
   [ScriptFile("/js/formcomponents/tabseditor.js", false, false)]
   [ScriptFile("/css/widgets/tabs.css", true, true, true)]
   [ScriptFile("/js/widgets/tabs.js", false, true, true)]
   [ScriptFile("/lib/slick/slick.css", true, true)]
   [ScriptFile("/lib/slick/slick-theme.css", true, true)]
   [ScriptFile("/lib/slick/slick.min.js", false, true)]
   [ScriptFile("/js/slickwaifix.js", false, true, true)]
   public class WamgroupTabsViewComponent : ViewComponent
   {

      private readonly RestClientService restClientService;

      public WamgroupTabsViewComponent(RestClientService restClientService, CustomScriptService customScriptService)
      {
         this.restClientService = restClientService;
         customScriptService.RegisterType(this.GetType());
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupTabsEntity> context)
      {
         if (context is null)
         {
            throw new ArgumentNullException(nameof(context));
         }
         
         IList<WamgroupTabsViewModel> tabs = new List<WamgroupTabsViewModel>();
         
         if (context.Entity.TabsDefinition != null)
         {
            foreach (var tab in context.Entity.TabsDefinition)
            {
               var tabModel = new WamgroupTabsViewModel
               {
                  Text = tab.Text ?? ""
               };
               

               var id = tab.Image?.ItemIdsOrdered?.FirstOrDefault();
               if (id != null)
               {
                  var mediaFile = await restClientService.GetItem<ImageDto>(id);
                  tabModel.Image = mediaFile.Url;
                  tabModel.Width = mediaFile.Width;
                  tabModel.Height = mediaFile.Height;
                  tabModel.ImageAlt = mediaFile.UrlName;

               }

               id = tab.HoverImage?.ItemIdsOrdered?.FirstOrDefault();
               if (id != null)
               {
                  var mediaFile = await restClientService.GetItem<ImageDto>(id);
                  tabModel.HoverImage = mediaFile.Url;

               }

               

               tabs.Add(tabModel);
            }
         }
         return View("Default", tabs);
      }

   }
}

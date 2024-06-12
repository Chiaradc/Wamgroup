using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.Models.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupGeneralSlider;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupGeneralSlider;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using System.Drawing;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupGeneralSlider
{
    [SitefinityWidget(Title = "Wamgroup General Slider", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica General Slider", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/generalslider.jpg")]
    [ScriptFile("/lib/swiper/swiper-bundle.min.js", false, true, true)]
    [ScriptFile("/js/widgets/generalslider.min.js", false, true, true)]
   [ScriptFile("/css/widgets/generalslider.min.css", true, true, true)]
   public class WamgroupGeneralSliderViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;

      public WamgroupGeneralSliderViewComponent(RestClientService restClientService, CustomScriptService customScriptService)
      {
         this.restClientService = restClientService;
         customScriptService.RegisterType(this.GetType());
      }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupGeneralSliderEntity> context)
      {
         if (context is null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         var items = context.Entity.Elementi?.ItemIdsOrdered;
         List<WamgroupGeneraSliderItem> models = new List<WamgroupGeneraSliderItem>();
         WamgroupGeneralSliderViewModel model = new WamgroupGeneralSliderViewModel();

         if (items != null)
         {
            foreach (var item in items)
            {
               
               var generalSliderItem = await restClientService.GetItem<WamgroupGeneralSliderItem>(item,
                  new List<string> { "TipoContenuto","DesktopImage", "TabletImage", "MobileImage", "DeskTopDetailImage", "CodiceVideo", "TipoVideo", "LocalVideo", "Title", "Text", "CTALink", "VisualizzaControlli", "VisualizzaModale", "BackgroundColor", "BackgroundOpacity", "TextColor", "FullWidthText", "TextAlignment" },
                  Constants.DynamicTypeGeneralSliderItem).ConfigureAwait(true);
               var generalSliderModel = new WamgroupGeneraSliderItem
               {

                  CodiceVideo = generalSliderItem.CodiceVideo ?? string.Empty,
                  TipoContenuto = generalSliderItem.TipoContenuto ?? "1",
                  TipoVideo = generalSliderItem.TipoVideo ?? string.Empty,
                  Title = generalSliderItem.Title ?? string.Empty,
                  Text = generalSliderItem.Text ?? string.Empty,
                  FullWidthText = generalSliderItem.FullWidthText ?? false,
                  TextAlignment = generalSliderItem.TextAlignment ?? "1",
                  VisualizzaControlli = generalSliderItem.VisualizzaControlli ?? false,
                  VisualizzaModale = generalSliderItem.VisualizzaModale ?? false,
                  BackgroundColor = string.IsNullOrEmpty( generalSliderItem.BackgroundColor) ? "" : generalSliderItem.BackgroundColor,
                  TextColor = string.IsNullOrEmpty(generalSliderItem.TextColor) ? "inherit" : generalSliderItem.TextColor,
                  BackgroundOpacity = generalSliderItem.BackgroundOpacity ?? 100
               };
               var strs = JsonConvert.DeserializeObject<dynamic>(generalSliderItem.CTALink ?? string.Empty);
               generalSliderModel.CTALink = strs?[0]["href"] ?? "";
               generalSliderModel.CTAText = strs?[0]["text"] ?? "";
               generalSliderModel.CTATargetBlank = strs?[0]["target"] == "_blank";

               if (generalSliderItem.DesktopImage?.Length > 0)
               {
                  var mediaFile = generalSliderItem.DesktopImage[0];
                  generalSliderModel.Width = mediaFile.Width;
                  generalSliderModel.Height = mediaFile.Height;
                  if (string.IsNullOrWhiteSpace(generalSliderModel.ImageAlt))
                     generalSliderModel.ImageAlt = mediaFile.UrlName;
                  generalSliderModel.DesktopImageUrl = mediaFile.Url;
                  generalSliderModel.TabletImageUrl = mediaFile.Url;
                  generalSliderModel.MobileImageUrl = mediaFile.Url;
               }

               if (generalSliderItem.TabletImage?.Length > 0)
               {
                  var mediaFile = generalSliderItem.TabletImage[0];
                  generalSliderModel.TabletImageUrl = mediaFile.Url;
                  generalSliderModel.MobileImageUrl = mediaFile.Url;
               }

               if (generalSliderItem.MobileImage?.Length > 0)
               {
                  var mediaFile = generalSliderItem.MobileImage[0];
                  generalSliderModel.MobileImageUrl = mediaFile.Url;
               }

               if (generalSliderItem.LocalVideo?.Length > 0)
               {
                  var mediaFile = generalSliderItem.LocalVideo[0];
                  generalSliderModel.VideoLocaleUrl = mediaFile.Url;
                    }

                   /* if (entity.ColumnBackgrounds[item].BackgroundType != Enums.SectionBackgroundType.None)
                    {
                        var bgColor = string.Empty;
                        if (entity.ColumnBackgrounds[item].Opacity != 100 || !colorsHelper.ParseBackgroundColorFromHEX(entity.ColumnBackgrounds[item].BackgroundColor, out bgColor))
                            retValue[item].ColumnStyles += $" background-color: {GenerateRgba(entity.ColumnBackgrounds[item].BackgroundColor, entity.ColumnBackgrounds[item].Opacity)};";
                    }*/

               if(generalSliderItem.BackgroundColor != null)
                    {
                        generalSliderModel.BackgroundColor = GenerateRgba(generalSliderItem.BackgroundColor, generalSliderItem.BackgroundOpacity ?? 100);
                    }
                    
                    
               models.Add(generalSliderModel);
                }
            }
            model.Items = models;
         
         
         return View($"Default", model);
      }


        private string GenerateRgba(string backgroundColor, int backgroundOpacity)
        {
            Color color = ColorTranslator.FromHtml(backgroundColor);
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);
            return string.Format("rgba({0}, {1}, {2}, {3})", r, g, b, (((decimal)backgroundOpacity) / 100).ToString().Replace(",", "."));
        }
    }

  
}

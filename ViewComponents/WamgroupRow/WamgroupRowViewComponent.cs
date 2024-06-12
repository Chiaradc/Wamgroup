using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupRow;
using Elogic.Wamgroup.Sito2023.NetCore.Misc;
using Elogic.Wamgroup.Sito2023.NetCore.Models.WamgroupRow;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupRow;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using System.Drawing;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupRow
{
   [SitefinityWidget(Title = "Wamgroup row", Category = WidgetCategory.Layout, Section = "Wamgroup", EmptyIconText = "Modifica Row", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/row.jpg")]

   public class WamgroupRowViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;

      public WamgroupRowViewComponent(RestClientService restClientService)
      {
         this.restClientService = restClientService;
      }

      public async Task<IViewComponentResult> InvokeAsync(ICompositeViewComponentContext<WamgroupRowEntity> context)
      {
         await Task.CompletedTask;

         WamgroupRowViewModel model = new WamgroupRowViewModel
         {
            PresetRowClass = $" {context.Entity.PresetRowClass} {(context.Entity.InvertiColonneSuMobile ? "col-reverse-xs" : "")} {(context.Entity.CentraColonne ? "justify-content-center" : "")}",
            ColumnsNumber = context.Entity.ColumnsNumber,
            ColumnDefinitions = await PrepareColumnsDefinitions(context.Entity),
            ColumnGap = context.Entity.ColumnsGap,
            Context = context
         };

         return View("Default", model);
      }

      private async Task<Dictionary<string, WamgroupRowColumnViewModel>> PrepareColumnsDefinitions(WamgroupRowEntity entity)
      {
         ColorsHelper colorsHelper = new ColorsHelper();
         Dictionary<string, WamgroupRowColumnViewModel> retValue = new Dictionary<string, WamgroupRowColumnViewModel>();

         //creazione del dato
         for (int i = 1; i <= entity.ColumnsNumber; i++)
         {
            var item = $"Column{i}";
            retValue.Add(item, new WamgroupRowColumnViewModel());

            var columnSpaces = entity.ColumnSpaces.ContainsKey(item) ? entity.ColumnSpaces[item] : new Models.WamgroupRow.ColumnSpaces();
            var columnAlignments = entity.ColumnAlignments.ContainsKey(item) ? entity.ColumnAlignments[item] : new Models.WamgroupRow.ColumnAlignments();
            var columnBackgrounds = entity.ColumnBackgrounds.ContainsKey(item) ? entity.ColumnBackgrounds[item] : new Models.WamgroupRow.ColumnBackground();
            var columnColors = entity.ColumnColors.ContainsKey(item) ? entity.ColumnColors[item] : new Models.WamgroupRow.ColumnColor();
            var columnBackgroundImage = entity.ColumnBackgroundImage.ContainsKey(item) ? entity.ColumnBackgroundImage[item] : new Models.WamgroupRow.ColumnBackgroundImage();
            var columnCustomClass = entity.ColumnCustomClass.ContainsKey(item) ? entity.ColumnCustomClass[item] : new Models.WamgroupRow.ColumnCustomClass();

            //creazione delle larghezze colonne /visibilità
            if (columnSpaces.DesktopSpacesCount > 0)
               retValue[item].ColumnOuterClasses += $"col-xl-{(int)columnSpaces.DesktopSpacesCount} d-xl-flex";
            else
               retValue[item].ColumnOuterClasses += $"d-xl-none";
            if (columnSpaces.TabletSpacesCount > 0)
               retValue[item].ColumnOuterClasses += $" col-md-{(int)columnSpaces.TabletSpacesCount} d-md-flex";
            else
               retValue[item].ColumnOuterClasses += $" d-md-none";
            if (columnSpaces.MobileSpacesCount > 0)
               retValue[item].ColumnOuterClasses += $" col-{(int)columnSpaces.MobileSpacesCount} d-flex";
            else
               retValue[item].ColumnOuterClasses += $" d-none";

            //creazione dell'allineamneto
            if (columnAlignments.VerticalAlignment == Enums.VerticalAlignment.Top)
               retValue[item].ColumnClasses += $" flex-start";
            else if (columnAlignments.VerticalAlignment == Enums.VerticalAlignment.Center)
               retValue[item].ColumnClasses += $" flex-center";
            else if (columnAlignments.VerticalAlignment == Enums.VerticalAlignment.Bottom)
               retValue[item].ColumnClasses += $" flex-end";

            if (columnAlignments.HorizontalAlignment == Enums.HorizontalAlignment.Left)
               retValue[item].ColumnClasses += $" content-left";
            else if (columnAlignments.HorizontalAlignment == Enums.HorizontalAlignment.Center)
               retValue[item].ColumnClasses += $" content-center";
            else if (columnAlignments.HorizontalAlignment == Enums.HorizontalAlignment.Right)
               retValue[item].ColumnClasses += $" content-right";

            //creazione colore di sfondo
            if (columnBackgrounds.BackgroundType != Enums.SectionBackgroundType.None)
            {
               var bgColor = string.Empty;
               if (columnBackgrounds.Opacity == 100 && colorsHelper.ParseBackgroundColorFromHEX(columnBackgrounds.BackgroundColor, out bgColor))
                  retValue[item].ColumnClasses += $" {bgColor}";
            }

            //creazione del colore del testo
            if (columnColors.TextColorType != 0)
            {
               var txtColor = string.Empty;
               if (colorsHelper.ParseTextColorFromHEX(columnColors.TextColor, out txtColor))
                  retValue[item].ColumnClasses += $" {txtColor}";
            }

            //creazione colore di sfondo personalizzato
            if (columnBackgrounds.BackgroundType != Enums.SectionBackgroundType.None)
            {
               var bgColor = string.Empty;
               if (columnBackgrounds.Opacity != 100 || !colorsHelper.ParseBackgroundColorFromHEX(columnBackgrounds.BackgroundColor, out bgColor))
                  retValue[item].ColumnStyles += $" background-color: {GenerateRgba(columnBackgrounds.BackgroundColor, columnBackgrounds.Opacity)};";
            }


            //creazione colore del testo personalizzato
            if (columnColors.TextColorType != 0)
            {
               var txtColor = string.Empty;
               if (!colorsHelper.ParseTextColorFromHEX(columnColors.TextColor, out txtColor))
                  retValue[item].ColumnStyles += $" color: {columnColors.TextColor};";
            }


            //creazione immagine di sfondo
            if (columnBackgroundImage.BackgroundImage == Enums.SectionBackgroundType.Image)
            {

               var id = columnBackgroundImage.BackgroundDesktopImage?.ItemIdsOrdered?.FirstOrDefault();
               if (id != null)
               {
                  var mediaFile = await restClientService.GetItem<ImageDto>(id);
                  //if (string.IsNullOrWhiteSpace(sectionViewModel.BackgroundImageAlternativeText))
                  //    sectionViewModel.BackgroundImageAlternativeText = mediaFile.UrlName;
                  retValue[item].ColumnDesktopImageUrl = mediaFile.Url;
                  retValue[item].ColumnTabletImageUrl = $"{mediaFile.Url}?size=768";
                  retValue[item].ColumnMobileImageUrl = $"{mediaFile.Url}?size=600";
               }
            }


            //if (entity.ColumnCustomClass[item].ColCustomClass != string.Empty)
            //{
            //   var colStyle = entity.ColumnCustomClass[item].ColCustomClass;
            //   retValue[item].ColumnClasses += $" {colStyle}";
            //}
            var colStyle = string.Empty;
            if (columnCustomClass.ClassSquare)
               colStyle += " square";
            if (colStyle != string.Empty)
               retValue[item].ColumnClasses += $"{colStyle}";

         }

         //foreach (var item in entity.ColumnSpaces.Keys)
         //{
         //   if (!retValue.ContainsKey(item))
         //      retValue.Add(item, new WamgroupRowColumnViewModel());
         //}


         ////creazione delle larghezze colonne /visibilità
         //foreach (var item in entity.ColumnSpaces.Keys)
         //{
         //   if (entity.ColumnSpaces[item].DesktopSpacesCount > 0)
         //      retValue[item].ColumnOuterClasses += $"col-xl-{entity.ColumnSpaces[item].DesktopSpacesCount} d-xl-flex";
         //   else
         //      retValue[item].ColumnOuterClasses += $"d-xl-none";
         //   if (entity.ColumnSpaces[item].TabletSpacesCount > 0)
         //      retValue[item].ColumnOuterClasses += $" col-md-{entity.ColumnSpaces[item].TabletSpacesCount} d-md-flex";
         //   else
         //      retValue[item].ColumnOuterClasses += $" d-md-none";
         //   if (entity.ColumnSpaces[item].MobileSpacesCount > 0)
         //      retValue[item].ColumnOuterClasses += $" col-{entity.ColumnSpaces[item].MobileSpacesCount} d-flex";
         //   else
         //      retValue[item].ColumnOuterClasses += $" d-none";
         //}

         ////creazione dell'allineamneto
         //foreach (var item in entity.ColumnAlignments.Keys)
         //{
         //   if (entity.ColumnAlignments[item].VerticalAlignment == Enums.VerticalAlignment.Top)
         //      retValue[item].ColumnClasses += $" flex-start";
         //   else if (entity.ColumnAlignments[item].VerticalAlignment == Enums.VerticalAlignment.Center)
         //      retValue[item].ColumnClasses += $" flex-center";
         //   else if (entity.ColumnAlignments[item].VerticalAlignment == Enums.VerticalAlignment.Bottom)
         //      retValue[item].ColumnClasses += $" flex-end";

         //   if (entity.ColumnAlignments[item].HorizontalAlignment == Enums.HorizontalAlignment.Left)
         //      retValue[item].ColumnClasses += $" content-left";
         //   else if (entity.ColumnAlignments[item].HorizontalAlignment == Enums.HorizontalAlignment.Center)
         //      retValue[item].ColumnClasses += $" content-center";
         //   else if (entity.ColumnAlignments[item].HorizontalAlignment == Enums.HorizontalAlignment.Right)
         //      retValue[item].ColumnClasses += $" content-right";
         //}

         ////creazione colore di sfondo
         //foreach (var item in entity.ColumnBackgrounds.Keys)
         //{
         //   if (entity.ColumnBackgrounds[item].BackgroundType != Enums.SectionBackgroundType.None)
         //   {
         //      var bgColor = string.Empty;
         //      if (entity.ColumnBackgrounds[item].Opacity == 100 && colorsHelper.ParseBackgroundColorFromHEX(entity.ColumnBackgrounds[item].BackgroundColor, out bgColor))
         //         retValue[item].ColumnClasses += $" {bgColor}";
         //   }
         //}

         ////creazione del colore del testo
         //foreach (var item in entity.ColumnColors.Keys)
         //{
         //   if (entity.ColumnColors[item].TextColorType != 0)
         //   {
         //      var txtColor = string.Empty;
         //      if (colorsHelper.ParseTextColorFromHEX(entity.ColumnColors[item].TextColor, out txtColor))
         //         retValue[item].ColumnClasses += $" {txtColor}";
         //   }
         //}

         ////creazione colore di sfondo personalizzato
         //foreach (var item in entity.ColumnBackgrounds.Keys)
         //{
         //   if (entity.ColumnBackgrounds[item].BackgroundType != Enums.SectionBackgroundType.None)
         //   {
         //      var bgColor = string.Empty;
         //      if (entity.ColumnBackgrounds[item].Opacity != 100 || !colorsHelper.ParseBackgroundColorFromHEX(entity.ColumnBackgrounds[item].BackgroundColor, out bgColor))
         //         retValue[item].ColumnStyles += $" background-color: {GenerateRgba(entity.ColumnBackgrounds[item].BackgroundColor, entity.ColumnBackgrounds[item].Opacity)};";
         //   }
         //}

         ////creazione colore del testo personalizzato
         //foreach (var item in entity.ColumnColors.Keys)
         //{
         //   if (entity.ColumnColors[item].TextColorType != 0)
         //   {
         //      var txtColor = string.Empty;
         //      if (!colorsHelper.ParseTextColorFromHEX(entity.ColumnColors[item].TextColor, out txtColor))
         //         retValue[item].ColumnStyles += $" color: {entity.ColumnColors[item].TextColor};";
         //   }
         //}

         ////creazione immagine di sfondo
         //foreach (var item in entity.ColumnBackgroundImage.Keys)
         //{
         //   if (entity.ColumnBackgroundImage[item].BackgroundImage == Enums.SectionBackgroundType.Image)
         //   {

         //      var id = entity.ColumnBackgroundImage[item].BackgroundDesktopImage?.ItemIdsOrdered?.FirstOrDefault();
         //      if (id != null)
         //      {
         //         var mediaFile = await restClientService.GetItem<ImageDto>(id);
         //         //if (string.IsNullOrWhiteSpace(sectionViewModel.BackgroundImageAlternativeText))
         //         //    sectionViewModel.BackgroundImageAlternativeText = mediaFile.UrlName;
         //         retValue[item].ColumnDesktopImageUrl = mediaFile.Url;
         //         retValue[item].ColumnTabletImageUrl = $"{mediaFile.Url}?size=768";
         //         retValue[item].ColumnMobileImageUrl = $"{mediaFile.Url}?size=600";
         //      }
         //   }
         //}

         //foreach (var item in entity.ColumnCustomClass.Keys)
         //{
         //   //if (entity.ColumnCustomClass[item].ColCustomClass != string.Empty)
         //   //{
         //   //   var colStyle = entity.ColumnCustomClass[item].ColCustomClass;
         //   //   retValue[item].ColumnClasses += $" {colStyle}";
         //   //}
         //   var colStyle = string.Empty;
         //   if (entity.ColumnCustomClass[item].ClassSquare)
         //      colStyle += " square";
         //   if (colStyle != string.Empty)
         //      retValue[item].ColumnClasses += $"{colStyle}";
         //}
         return retValue;
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

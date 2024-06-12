using Elogic.Sitefinity.Infrastructure.Images;
using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupProductList;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupProductList;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Filters;
using Telerik.Sitefinity.RelatedData;
using static System.Collections.Specialized.BitVector32;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupProductList
{
   [SitefinityWidget(Title = "Wamgroup Product List", Category = WidgetCategory.Content, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica Product List", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/images.jpg")]
   [ScriptFile("/css/widgets/productListDetail.min.css", true, true, true)]
    public class WamgroupProductListViewComponent : ViewComponent
   {
      private readonly RestClientService restClientService;
      private readonly IImagesService imagesService;

      public WamgroupProductListViewComponent(RestClientService restClientService, IImagesService imagesService, CustomScriptService customScriptService)
      {
         this.restClientService = restClientService;
         this.imagesService = imagesService;
         customScriptService.RegisterType(this.GetType());
        }

      public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupProductListEntity> context)
      {
         if (context == null)
         {
            throw new ArgumentNullException(nameof(context));
         }

         var model = new WamgroupProductListViewModel();
         var productsModel = new List<WamgroupProductListItemModel>();

         var tecnologies = context.Entity.Tecnologies?.ItemIdsOrdered;
         var solutions = context.Entity.Solutions?.ItemIdsOrdered;

         var args = new GetAllArgs();
         IList<object> filters = new List<object>();

         if (tecnologies != null)
         {
            filters.Add(new FilterClause
            {
               FieldName = nameof(WamgroupProduct.tecnologies),
               Operator = FilterClause.Operators.ContainsOr,
               FieldValue = tecnologies

            });
            
         }
         if (solutions != null)
         {
            filters.Add(new FilterClause
            {
               FieldName = nameof(WamgroupProduct.solutions),
               Operator = FilterClause.Operators.ContainsOr,
               FieldValue = solutions
            });
         }

         if (filters.Count > 0)
         {
            args.Filter = new CombinedFilter
            {
                ChildFilters = filters,
                Operator = CombinedFilter.LogicalOperators.And
            };
         }


         var products = await restClientService.GetItems<WamgroupProduct>(args);

         foreach (var product in products)
         {
            var p = new WamgroupProductListItemModel
            {
               Title = product.Title,
               Description = product.Description,
               Url = string.IsNullOrEmpty(context.Entity.LinkProductDetail?.Href) ? "#" : Path.Combine(context.Entity.LinkProductDetail?.Href ?? "", product.UrlName ?? "")
            };

            var productImages = await restClientService.GetItems<WamgroupVisualSlide>(new List<string> { "Id", "Image" }, 0, null, product.Id);
            p.ImageUrl = productImages.FirstOrDefault()?.Image?.FirstOrDefault()?.Url;

            productsModel.Add(p);
         }

         model.Title = context.Entity.Title;
         model.MaxNumber = context.Entity.MaxNumber;
         model.Items = productsModel;

         return View("Default", model);
      }

   }
}

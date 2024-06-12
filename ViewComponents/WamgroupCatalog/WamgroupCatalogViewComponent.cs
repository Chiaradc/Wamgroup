using Elogic.Sitefinity.Infrastructure.GlobalizationServices;
using Elogic.Sitefinity.Infrastructure.PageBuilder;
using Elogic.Wamgroup.Sito2023.NetCore.Entities.WamgroupCatalog;
using Elogic.Wamgroup.Sito2023.NetCore.Services;
using Elogic.Wamgroup.Sito2023.NetCore.ViewModels.WamgroupCatalog;
using Elogic.Wamgroup.Sito2023_Models.SdkItems;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Telerik.Sitefinity.GeoLocations;

namespace Elogic.Wamgroup.Sito2023.NetCore.ViewComponents.WamgroupCatalogue
{
    [SitefinityWidget(Title = "Wamgroup Catalog", Category = WidgetCategory.Layout, Section = "Wamgroup", EmptyIcon = "pencil-square-o", EmptyIconText = "Modifica", EmptyIconAction = "Edit", ThumbnailUrl = "/thumbnails/header.jpg")]
    [ScriptFile("/css/widgets/catalog.min.css", true, true, false)]
    [ScriptFile("/js/widgets/catalog.js", false, true, true)]

    public class WamgroupCatalogViewComponent : ViewComponent
    {
        private readonly RestClientService restClientService;

        public WamgroupCatalogViewComponent(CustomScriptService customScriptService, RestClientService restClientService)
        {
            customScriptService.RegisterType(this.GetType());
            this.restClientService = restClientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<WamgroupCatalogEntity> context)
        {

            var viewModel = new WamgroupCatalogViewModel();

            viewModel.SolutionsByIndustry = new List<WamgroupCatalogSolutionItem>();
            var solutionsByIndustry = await restClientService.GetItems<ClassificationSolutionByIndustry>();
            foreach (var solution in solutionsByIndustry)
            {
                viewModel.SolutionsByIndustry.Add(new WamgroupCatalogSolutionItem
                {
                    Id = solution.Id,
                    Title = solution.Title,
                    IsSelected = false
                });
            }

            viewModel.SolutionsByNeed = new List<WamgroupCatalogSolutionItem>();
            var solutionsByNeed = await restClientService.GetItems<ClassificationSolutionByNeed>();
            foreach (var solution in solutionsByNeed)
            {
                viewModel.SolutionsByNeed.Add(new WamgroupCatalogSolutionItem
                {
                    Id = solution.Id,
                    Title = solution.Title,
                    IsSelected = false
                });
            }
            
            viewModel.Brands = new List<WamgroupCatalogBrandItem>();
            var brands = await restClientService.GetItems<ClassificationBrand>();
            foreach (var brand in brands)
            {
                viewModel.Brands.Add(new WamgroupCatalogBrandItem
                {
                    Id = brand.Id,
                    Title = brand.Title,
                    IsSelected = false
                });
            }

            var macroFamilies = await restClientService.GetItems<ClassificationMacroFamily>();
            var families = await restClientService.GetItems<ClassificationFamily>();
            var familyModules = await restClientService.GetItems<WamgroupFamily>(new List<string> { "*" });
            foreach (var macroFamily in macroFamilies)
            {

                var macroFamilyItem = new WamgroupCatalogMacrofamilyItem
                {
                    Id = macroFamily.Id,
                    Title = macroFamily.Title,
                    IsSelected = false
                };

                var selectedFamilies = familyModules.Where(x => x.macrofamilies.Contains(macroFamilyItem.Id)).ToList();
                foreach (var selectedFamily in selectedFamilies)
                {
                    
                    var family = families.FirstOrDefault(x => x.Id == selectedFamily.families.FirstOrDefault());
                    if (family != null)
                    {
                        var familyItem = new WamgroupCatalogFamilyItem
                        {
                            Id = family.Id,
                            Title = family.Title,
                            IsSelected = false
                        };

                        macroFamilyItem.Families.Add(familyItem);
                    }
                }

                viewModel.Macrofamilies.Add(macroFamilyItem);
            }


            var products = await restClientService.GetItems<WamgroupProduct>();
            foreach (var product in products)
            {
                var p = new WamgroupCatalogProductItem
                {
                    Title = product.Title,
                    Description = product.Description,
                    //Url = string.IsNullOrEmpty(context.Entity.LinkProductDetail?.Href) ? "#" : Path.Combine(context.Entity.LinkProductDetail?.Href ?? "", product.UrlName ?? "")
                };

                var productImages = await restClientService.GetItems<WamgroupVisualSlide>(new List<string> { "Id", "Image" }, 0, null, product.Id);
                p.ImageUrl = productImages.FirstOrDefault()?.Image?.FirstOrDefault()?.Url;

                viewModel.Products.Add(p);
            }

            viewModel.ProductURL = context.Entity.LinkProductDetail?.Href;

            return View("Default", viewModel);
        }

    }
}

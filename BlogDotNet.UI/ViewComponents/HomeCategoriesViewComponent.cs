using BlogDotNet.ServiceLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogDotNet.UI.ViewComponents
{
    public class HomeCategoriesViewComponent:ViewComponent
    {
        private readonly ICategoryService categoryService;
        public HomeCategoriesViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await categoryService.GetAllCategoriesNonDeletedTake24();
            return View(categories);
        }
    }
}

using BlogDotNet.ServiceLayer.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogDotNet.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IDashboardService dashboardService;
        public HomeController(IArticleService articleService, IDashboardService dashboardService)
        {
            this.articleService = articleService;
            this.dashboardService = dashboardService;
        }
        public async Task<IActionResult> Index()
        {
            var articles=await articleService.GetAllArticlesWithCategoryNonDeletedAsync();
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> YearlyArticleCount()
        {
            var count = await dashboardService.GetYearlyArticleCount();
            return Json(JsonConvert.SerializeObject(count));
        }
        [HttpGet]
        public async Task<IActionResult> TotalArticleCount()
        {
            var count=await dashboardService.GetTotalArticleCount();
            return Json(count);
        }
        [HttpGet]
        public async Task<IActionResult> TotalCategoryCount()
        {
            var count= await dashboardService.GetTotalCategoryCount();
            return Json(count);
        }
    }
}

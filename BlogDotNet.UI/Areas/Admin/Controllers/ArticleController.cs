using AutoMapper;
using BlogDotNet.EntityLayer.DTOs.Articles;
using BlogDotNet.EntityLayer.Entities;
using BlogDotNet.ServiceLayer.Extensions;
using BlogDotNet.ServiceLayer.Services.Abstractions;
using BlogDotNet.UI.Consts;
using BlogDotNet.UI.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BlogDotNet.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toastNotification;

        public ArticleController(IArticleService articleService, ICategoryService categoryService,IMapper mapper,IValidator<Article> validator, IToastNotification toastNotification)
        {
            this.articleService = articleService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.validator = validator;
            this.toastNotification = toastNotification;
        }
        [HttpGet]
        [Authorize(Roles =$"{RoleConsts.Superadmin}, {RoleConsts.Admin}, {RoleConsts.User}")]
        public async Task<IActionResult> Index()
        {
            var articles=await articleService.GetAllArticlesWithCategoryNonDeletedAsync();
            return View(articles);
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        [HttpGet] 
        public async Task<IActionResult> DeletedArticle()
        {
            var articles = await articleService.GetAllDeletedArticlesWithCategory();
            return View(articles);
        }
        
        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddDto
            {
                Categories = categories
            });
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            var mappedData=mapper.Map<Article>(articleAddDto);
            var result = await validator.ValidateAsync(mappedData);

            if (result.IsValid)
            {
                await articleService.CreateArticleAsync(articleAddDto);
                toastNotification.AddSuccessToastMessage(Messages.Article.Add(articleAddDto.Title),new ToastrOptions { Title="Başarılı!"});
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            else
            {
                result.AddToModelState(this.ModelState);
                var categories = await categoryService.GetAllCategoriesNonDeleted();
                return View(new ArticleAddDto
                {
                    Categories = categories
                });
            }           
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Update(Guid articleId)
        {
            var article=await articleService.GetArticleByGuidWithCategoryNonDeletedAsync(articleId);
            var categories=await categoryService.GetAllCategoriesNonDeleted();

            var articleUpdateDto=mapper.Map<ArticleUpdateDto>(article);
            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
        {
            var mappedData=mapper.Map<Article>(articleUpdateDto);
            var result= await validator.ValidateAsync(mappedData);
            if (result.IsValid)
            {
                string oldTitle=await articleService.UpdateArticleAsync(articleUpdateDto);
                toastNotification.AddSuccessToastMessage(Messages.Article.Update(oldTitle), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            else {
                result.AddToModelState(this.ModelState);
                var categories = await categoryService.GetAllCategoriesNonDeleted();
                articleUpdateDto.Categories= categories;
                return View(articleUpdateDto);
            }
            
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid articleId)
        {
            string deletedArticleTitle=await articleService.SafeDeleteArticleAsync(articleId);
            toastNotification.AddSuccessToastMessage(Messages.Article.Delete(deletedArticleTitle), new ToastrOptions { Title = "Başarılı!" });
            return RedirectToAction("Index", "Article", new { Area = "Admin" }); 
        }
        [Authorize(Roles = $"{RoleConsts.Superadmin}, {RoleConsts.Admin}")]
        public async Task<IActionResult> UndoDelete(Guid articleId)
        {
            string undoDeletedArticleTitle = await articleService.UndoDeleteArticleAsync(articleId);
            toastNotification.AddSuccessToastMessage(Messages.Article.UndoDelete(undoDeletedArticleTitle), new ToastrOptions { Title = "Başarılı!" });
            return RedirectToAction("DeletedArticle", "Article", new { Area = "Admin" });
        }
    }
}

using AutoMapper;
using BlogDotNet.EntityLayer.DTOs.Articles;
using BlogDotNet.EntityLayer.DTOs.Categories;
using BlogDotNet.EntityLayer.Entities;
using BlogDotNet.ServiceLayer.Extensions;
using BlogDotNet.ServiceLayer.Services.Abstractions;
using BlogDotNet.ServiceLayer.Services.Concrete;
using BlogDotNet.UI.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace BlogDotNet.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<Category> validator;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public CategoryController(ICategoryService categoryService,IValidator<Category> validator,IMapper mapper,IToastNotification toast) 
        {
            this.categoryService = categoryService;
            this.validator = validator;
            this.mapper = mapper;
            this.toastNotification = toast;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories= await categoryService.GetAllCategoriesNonDeleted();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> DeletedCategory()
        {
            var categories = await categoryService.GetAllDeletedCategories();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {

            var mappedData = mapper.Map<Category>(categoryAddDto);
            var result = await validator.ValidateAsync(mappedData);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDto);
                toastNotification.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody]CategoryAddDto categoryAddDto)
        {
            if (categoryAddDto == null)
            {
                return BadRequest("Gönderilen veri boş.");
            }
            var mappedData=mapper.Map<Category>(categoryAddDto);
            var result=await validator.ValidateAsync(mappedData);
            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddDto);
                toastNotification.AddSuccessToastMessage(Messages.Category.Add(categoryAddDto.Name), new ToastrOptions { Title = "Başarılı!" });
                return Json(Messages.Category.Add(categoryAddDto.Name));
            }
            else
            {
                toastNotification.AddErrorToastMessage(result.Errors.First().ErrorMessage, new ToastrOptions { Title = "İşlem Başarısız" });
                return Json(result.Errors.First().ErrorMessage);

            }
            return Json("Kategori Ekleme İşleminde Bir Hata Oluştu");
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            var category=await categoryService.GetCategoryByGuid(categoryId);
            var mappedData = mapper.Map<Category, CategoryUpdateDto>(category);
            return View(mappedData);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var map=mapper.Map<Category>(categoryUpdateDto);
            var result=await validator.ValidateAsync(map);
            if (result.IsValid)
            {
                var name = await categoryService.UpdateCategoryAsync(categoryUpdateDto);
                toastNotification.AddSuccessToastMessage(Messages.Category.Update(name), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View();
            }
        }

        public async Task<IActionResult> Delete(Guid categoryId)
        {
            string deletedCategoryName = await categoryService.SafeDeleteCategoryAsync(categoryId);
            toastNotification.AddSuccessToastMessage(Messages.Category.Delete(deletedCategoryName), new ToastrOptions { Title = "Başarılı!" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }
        public async Task<IActionResult> UndoDelete(Guid categoryId)
        {
            string deletedCategoryName = await categoryService.UndoDeleteCategoryAsync(categoryId);
            toastNotification.AddSuccessToastMessage(Messages.Category.Delete(deletedCategoryName), new ToastrOptions { Title = "Başarılı!" });
            return RedirectToAction("DeletedCategory", "Category", new { Area = "Admin" });
        }
    }
}

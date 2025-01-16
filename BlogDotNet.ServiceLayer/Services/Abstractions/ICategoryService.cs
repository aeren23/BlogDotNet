using BlogDotNet.EntityLayer.DTOs.Categories;
using BlogDotNet.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Services.Abstractions
{
    public interface ICategoryService
    {
         Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
         Task<List<CategoryDto>> GetAllCategoriesNonDeletedTake24();
         Task<List<CategoryDto>> GetAllDeletedCategories();
         Task CreateCategoryAsync(CategoryAddDto categoryAddDto);
         Task<Category> GetCategoryByGuid(Guid id);
         Task<string> UpdateCategoryAsync(CategoryUpdateDto categoryAddDto);
         Task<string> SafeDeleteCategoryAsync(Guid categoryId);
         Task<string> UndoDeleteCategoryAsync(Guid categoryId);
    }
}

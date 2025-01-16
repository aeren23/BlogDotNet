using BlogDotNet.EntityLayer.DTOs.Articles;
using BlogDotNet.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Services.Abstractions
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync();

        Task<List<ArticleDto>> GetAllDeletedArticlesWithCategory();

        Task CreateArticleAsync(ArticleAddDto articleAddDto);

        Task<ArticleDto> GetArticleByGuidWithCategoryNonDeletedAsync(Guid articleId);

        Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);

        Task<string> SafeDeleteArticleAsync(Guid articleId);

        Task<string> UndoDeleteArticleAsync(Guid articleId);

        Task<ArticleListDto> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false);

        Task<ArticleListDto> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false);
    }
}

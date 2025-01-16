using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Services.Abstractions
{
    public interface IDashboardService
    {
        Task<List<int>> GetYearlyArticleCount();
        Task<int> GetTotalArticleCount();
        Task<int> GetTotalCategoryCount();
    }
}

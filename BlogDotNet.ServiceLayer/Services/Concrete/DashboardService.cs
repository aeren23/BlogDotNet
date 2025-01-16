using BlogDotNet.DataAccessLayer.UnitOfWorks;
using BlogDotNet.EntityLayer.Entities;
using BlogDotNet.ServiceLayer.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Services.Concrete
{
    public class DashboardService :IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<int>> GetYearlyArticleCount()
        {
            var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted);

            var startDate=DateTime.Now.Date;
            startDate=new DateTime(startDate.Year,1,1);

            List<int> datas = new();

            for(int i = 1; i <= 12; i++)
            {
                var startedDate=new DateTime(startDate.Year,i,1);
                var endedDate=startedDate.AddMonths(1);
                var data =articles.Where(x=>x.CreatedDate>=startedDate && x.CreatedDate<endedDate).Count();
                datas.Add(data);
            }
            return datas;
        }

        public async Task<int> GetTotalArticleCount()
        {
            var articleCount=await _unitOfWork.GetRepository<Article>().CountAsync();
            return articleCount;
        }
        public async Task<int> GetTotalCategoryCount()
        {
            var categoryCount = await _unitOfWork.GetRepository<Category>().CountAsync();
            return categoryCount;
        }

    }
}

using BlogDotNet.DataAccessLayer.UnitOfWorks;
using BlogDotNet.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogDotNet.UI.Filters.ArticleVisitors
{
    public class ArticleVisitorFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArticleVisitorFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            List<Visitor> visitors = _unitOfWork.GetRepository<Visitor>().GetAllAsync().Result;
            
            string getIP=context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string getUserAgent = context.HttpContext.Request.Headers["User-Agent"];

            Visitor visitor=new(getIP, getUserAgent);
            if(visitors.Any(x=>x.IpAddress==visitor.IpAddress) )
                return next();
            else
            {
                _unitOfWork.GetRepository<Visitor>().AddAsync(visitor);
                _unitOfWork.Save();
            }
            return next();
        }
    }
}

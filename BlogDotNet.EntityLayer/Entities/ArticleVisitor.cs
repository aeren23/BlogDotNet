using BlogDotNet.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.EntityLayer.Entities
{
    public class ArticleVisitor:IEntityBase
    {
        public ArticleVisitor() { }
        public ArticleVisitor(Guid articleId,int visitorId) 
        {
            ArticleId = articleId;
            VisitorId = visitorId;  
        }

        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public int VisitorId    { get; set; }
        public Visitor Visitor { get; set; }
    }
}

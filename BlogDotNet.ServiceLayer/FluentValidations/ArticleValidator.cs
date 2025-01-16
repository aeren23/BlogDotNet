using BlogDotNet.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.FluentValidations
{
    public class ArticleValidator: AbstractValidator<Article>
    {
        public ArticleValidator() 
        {
            RuleFor(x => x.Title).NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .MaximumLength(150)
                .WithName("Başlık");

            RuleFor(x => x.Content).NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .MaximumLength(250)
                .WithName("İçerik");
        }
    }
}

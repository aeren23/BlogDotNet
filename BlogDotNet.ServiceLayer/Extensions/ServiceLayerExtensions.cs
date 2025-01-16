using BlogDotNet.DataAccessLayer.Context;
using BlogDotNet.DataAccessLayer.Repositories.Abstractions;
using BlogDotNet.DataAccessLayer.Repositories.Concretes;
using BlogDotNet.DataAccessLayer.UnitOfWorks;
using BlogDotNet.ServiceLayer.FluentValidations;
using BlogDotNet.ServiceLayer.Services.Abstractions;
using BlogDotNet.ServiceLayer.Services.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using BlogDotNet.ServiceLayer.Helpers.Images;

namespace BlogDotNet.ServiceLayer.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly=Assembly.GetExecutingAssembly();

            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDashboardService, DashboardService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(assembly);

            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                opt.DisableDataAnnotationsValidation=true;
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            });
             
            return services;
        }
    }
}

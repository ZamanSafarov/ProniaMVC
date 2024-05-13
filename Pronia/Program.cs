using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.RepositoryAbstracts;
using Data.DAL;
using Data.RepositoryConcretes;
using Microsoft.EntityFrameworkCore;
using System;

namespace Pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>

            options.UseSqlServer(builder.Configuration.GetConnectionString("cString"))

            );
            builder.Services.AddScoped<IFeatureService,FeatureService>();
            builder.Services.AddScoped<IFeatureRepository,FeatureRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ISliderService, SliderService>();
            builder.Services.AddScoped<ISliderRepository, SliderRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();


           
            app.MapAreaControllerRoute("defaultAdmin", "admin", "admin/{controller=dashboard}/{action=index}/{id?}");

            app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");


            app.Run();
        }
    }
}

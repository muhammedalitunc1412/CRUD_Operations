using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Service.Abstract;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<CRUD_Context>();
            //serviceCollection.AddIdentity<User, Role>(options =>
            //{
            //    // User Password Options
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequiredUniqueChars = 0;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    // User Username and Email Options
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
            //    options.User.RequireUniqueEmail = true;
            //}).AddEntityFrameworkStores<CRUD_Context>();

            serviceCollection.AddScoped<IPersonalRepository, EfCorePersonalRepository>();
            serviceCollection.AddScoped<IPersonalService, PersonalManager>();

            return serviceCollection;
        }

    }
}

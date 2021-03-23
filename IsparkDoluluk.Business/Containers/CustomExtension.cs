using FluentValidation;
using IsparkDoluluk.Business.Abstract;
using IsparkDoluluk.Business.Concrete;
using IsparkDoluluk.Business.ValidationRules;
using IsparkDoluluk.DataAccess.Abstract;
using IsparkDoluluk.DataAccess.Concrete.Repository;
using IsparkDoluluk.Dto.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.Business.Containers
{
    public static class CustomExtension
    {

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));

            services.AddScoped<ILiveCapacityDal, EfLiveCapacityRepository>();
            services.AddScoped<ILiveCapacityService, LiveCapacityManager>();

            services.AddScoped<IParkPlaceDal, EfParkPlaceRepository>();
            services.AddScoped<IParkPlaceService, ParkPlaceManager>();

            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();

            services.AddScoped<IAppRoleDal, EfAppRoleRepository>();
            services.AddScoped<IAppRoleService, AppRoleManager>();

            services.AddScoped<IAppUserRoleDal, EfAppUserRoleRepository>();
            services.AddScoped<IAppUserRoleService, AppUserRoleManager>();

            services.AddScoped<IJwtService, JwtManager>();

            services.AddTransient<IValidator<ParkPlaceAddDto>, ParkPlaceAddDtoValidator>();
            services.AddTransient<IValidator<ParkPlaceUpdateDto>, ParkPlaceUpdateDtoValidator>();
            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();
            services.AddTransient<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
            services.AddTransient<IValidator<UpdateCapacityDto>, UpdateCapacityDtoValidator>();
        }
    }
}

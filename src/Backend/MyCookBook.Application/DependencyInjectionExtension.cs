using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Application.Services.AutoMapper;
using MyCookBook.Application.Services.Cryptography;
using MyCookBook.Application.UseCases.User.Register;

namespace MyCookBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordEncrypter(services, configuration);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(o => new AutoMapper.MapperConfiguration(o =>
            {
                o.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        public static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        public static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped(o => new PasswordEncrypter(additionalKey!));
        }
    }
}

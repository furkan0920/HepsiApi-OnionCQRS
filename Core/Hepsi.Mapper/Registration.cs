using Hepsi.Application.Interface.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Hepsi.Application.Interface.AutoMapper;

namespace Hepsi.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}

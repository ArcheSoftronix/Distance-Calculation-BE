using BussinessLayer;
using Helper.CommonHelper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace WebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
            //Helpers
            services.AddScoped<CommonHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //BLL
            services.AddScoped<DistanceBLL>();

            //Services
            services.AddScoped<IDistance, DistanceImpl>();
        }
    }
}

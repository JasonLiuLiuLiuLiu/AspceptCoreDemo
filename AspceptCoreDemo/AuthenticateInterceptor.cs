using System;
using System.Linq;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AspceptCoreDemo
{
    [NonAspect]
    public class AuthenticateInterceptor : AbstractInterceptor
    {

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var logger = context.ServiceProvider.GetService<ILogger<AuthenticateInterceptor>>();
            try
            {
                var apiRequest = (ApiRequest) context.Parameters.FirstOrDefault();
                if (apiRequest == null || apiRequest.Name != "admin")
                {
                    logger.LogWarning("unauthorized");
                    return;
                }
                logger.LogInformation(apiRequest.Message);
                await next(context);
            }
            catch (Exception e)
            {
                logger?.LogWarning("Service threw an exception!");
                throw;
            }

            finally
            {
                logger.LogInformation("Finished service call");
            }
        }
    }
}

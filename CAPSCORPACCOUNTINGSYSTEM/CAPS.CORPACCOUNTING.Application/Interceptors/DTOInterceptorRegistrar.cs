using System;
using Abp.Dependency;
using Abp.Application.Services;
using Castle.MicroKernel;
using Castle.Core;

namespace CAPS.CORPACCOUNTING.Interceptors
{
   internal static class DTOInterceptorRegistrar
    {

        public static void Initialize(IIocManager iocManager)
        {
            
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(DTOInterceptors)));
            }
        }
    }
}

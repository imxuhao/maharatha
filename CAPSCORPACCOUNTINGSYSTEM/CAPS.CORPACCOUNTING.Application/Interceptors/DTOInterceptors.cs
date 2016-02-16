using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.Json;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Timing;
using Castle.Core.Logging;
using Castle.DynamicProxy;


namespace CAPS.CORPACCOUNTING.Interceptors
{
    internal class DTOInterceptors : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                if (AsyncHelper.IsAsyncMethod(invocation.Method))
                {
                    PerformASyncDTOOperation(invocation);
                }
                else
                {
                    PerformSyncDTOOperation(invocation);
                }
            }
            catch (Exception ex)
            {
                // OnException(invocation, ex);
            }
        }


       


        private void PerformSyncDTOOperation(IInvocation invocation)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                stopwatch.Stop();

            }
        }
        private void PerformASyncDTOOperation(IInvocation invocation)
        {
            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    exception => SaveAuditInfo(stopwatch, exception)
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    exception => SaveAuditInfo(stopwatch, exception)
                    );
            }
        }

        private void SaveAuditInfo(Stopwatch stopwatch, Exception exception)
        {
            stopwatch.Stop();



        }

    }
}

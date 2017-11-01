using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Realize.Distributer.Models.Filters
{
    public class ErrorHandlerFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var ex = actionExecutedContext.Exception;
            var response =
                new
                {
                    State = "NG",
                    Message = ex.Message
                };

            actionExecutedContext.Response =
                new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new StringContent(response.Serialize(), Encoding.UTF8, "application/json")
                };

            return Task.Delay(0);
        }
    }
}

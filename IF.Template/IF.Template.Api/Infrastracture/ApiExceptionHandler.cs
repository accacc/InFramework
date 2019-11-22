using IF.Core.Data;
using IF.Core.Exception;
using IF.Core.Log;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using Newtonsoft.Json;

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IF.Template.Api.Infrastracture
{
    public class WebApiExceptionHandler
    {
        private readonly RequestDelegate request;
        private readonly ILogService logger;
        public WebApiExceptionHandler(RequestDelegate request, ILogService logger)
        {
            this.request = request;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if ContentLength exceeds 2mb.
                if(context.Request.Headers.ContentLength != null &&
                   context.Request.Headers.ContentLength / 1024m > 2000m)
                    await HandleExceptionAsync(
                        context,
                        new BusinessException($"İstek boyutu çok büyük"));
                else
                    await request(context);
            }
            catch (Autofac.Core.Registration.ComponentNotRegisteredException)
            {
                await HandleExceptionAsync(
                    context,
                    new ApplicationException("Handler çözülemedi,bunun bir kaç nedeni olabilir,Örnegin QueryAsync yerine Query methodu çağrılmış olabilir ya da ilgili request,response,command base tipden türetilmemiş olabilir."));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }



        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var responseBody = String.Empty;

            var statusCode = HttpStatusCode.InternalServerError;

            if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                BaseResponseContract baseResponse = new BaseResponseContract();
                baseResponse.IsSuccess = false;
                baseResponse.ErrorMessage = "Unauthorized Access";
                baseResponse.ExceptionType = ExceptionTypes.UnauthorizedAccess;

                await LogError(context, exception);
            }
            else if (exception is DataAnnotationValidationException dataAnnotationValidationException)
            {
                BaseResponseContract baseResponse = new BaseResponseContract();
                baseResponse.FromDataAnnotationValidationException(exception as DataAnnotationValidationException);
                baseResponse.ExceptionType = ExceptionTypes.Validation;
                responseBody = responseBody = JsonConvert.SerializeObject(baseResponse);

                await LogError(context, exception);
            }

            else if (exception is BusinessException businessException)
            {
                BaseResponseContract baseResponse = new BaseResponseContract();
                baseResponse.FromBusinessException(exception as BusinessException);
                baseResponse.ExceptionType = ExceptionTypes.Business;
                responseBody = responseBody = JsonConvert.SerializeObject(baseResponse);

                await LogError(context, exception);
            }
            else
            {
                BaseResponseContract baseResponse = new BaseResponseContract();
                baseResponse.FromException(exception);
                baseResponse.ExceptionType = ExceptionTypes.Unhandled;

                responseBody = responseBody = JsonConvert.SerializeObject(baseResponse);

                await LogError(context, exception);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;


            await context.Response.WriteAsync(responseBody);
        }

        private async Task LogError(HttpContext context, Exception exception)
        {
            if (exception.Data["IsHandled"] == null)
            {
                context.Request.Headers.TryGetValue("X-Device-OS", out StringValues channel);

                string channelId = "unknown";

                if (channel.Count > 0)
                {
                    channelId = channel.First().ToString();
                }

                await logger.ErrorAsync(exception, "core api", "error", "1", Guid.NewGuid(), context.Connection.RemoteIpAddress.MapToIPv4().ToString(), channelId);
            }
        }
    }
}

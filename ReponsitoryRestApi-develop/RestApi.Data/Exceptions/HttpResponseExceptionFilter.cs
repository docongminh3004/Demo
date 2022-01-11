using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Exceptions
{
    public class HttpResponseExceptionFilter: IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }
        public void OnActionExecuted(ActionExecutedContext context)
        {

            if (context.Exception is HttpResponseException httpResponseException)
            {
                var result = new
                {
                    devMsg = "Đầu vào dữ liệu không hợp lệ",
                    useMsg = "Có lỗi xảy ra,vui lòng liên hệ Công ty ABC để được hỗ trợ.",
                    data = httpResponseException.Value,
                    moreInfo = ""
                };

                context.Result = new ObjectResult(result)
                {
                    StatusCode = 400
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                var result = new
                {
                    devMsg = "Có lỗi phía sever",
                    useMsg = "Có lỗi xảy ra,vui lòng liên hệ Công ty ABC để được hỗ trợ.",
                    data = DBNull.Value,
                    moreInfo = ""
                };

                context.Result = new ObjectResult(result)
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }
        }
    }
}

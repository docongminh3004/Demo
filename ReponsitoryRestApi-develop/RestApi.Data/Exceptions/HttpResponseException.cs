using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Exceptions
{
    public class HttpResponseException:Exception
    {
        // bắt lỗi sử dụng Exception Handling Middleware .
        public HttpResponseException(object value = null) => (Value) = (value);
        public object Value { get; }
    }
}

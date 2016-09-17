using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Models
{
    public class ApiResponseBase
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Reason { get; private set; }
        [JsonIgnore]
        public object Value { get; protected set; }

        public ApiResponseBase(HttpStatusCode statusCode, string reason = null)
        {
            StatusCode = statusCode;
            Reason = reason;
        }
    }

    public class ApiResponse<T> : ApiResponseBase where T: class
    {
        public T Entity { get { return (T)Value; } }

        public ApiResponse(T entity)
            : base(HttpStatusCode.OK)
        {
            Value = entity;
        }

        public ApiResponse(T entity, HttpStatusCode statusCode)
            : base(statusCode)
        {
            Value = entity;
        }

        public ApiResponse(T entity, HttpStatusCode statusCode, string reason)
            : base(statusCode, reason)
        {
            Value = entity;
        }
    }
}

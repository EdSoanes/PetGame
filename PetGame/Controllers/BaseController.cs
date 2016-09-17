using PetGame.Models;
using PetGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PetGame.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly IGameService GameService;

        public BaseController(IGameService gameService)
        {
            GameService = gameService;
        }

        protected async Task<HttpResponseMessage> Execute<T>(Func<Task<T>> func) where T : class
        {
            try
            {
                var funcResult = await func.Invoke();
                if (funcResult == null)
                    return this.Request.CreateResponse(HttpStatusCode.NoContent);

                if (funcResult is ApiResponseBase)
                {
                    var apiResponse = funcResult as ApiResponseBase;
                    return this.Request.CreateResponse(apiResponse.StatusCode, apiResponse.Value);
                }

                return this.Request.CreateResponse<T>(funcResult);
            }
            catch (Exception ex)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
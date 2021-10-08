using API.Extensions;
using Contracts.Paging;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IServiceManager _serviceManager;
        protected IServiceManager ServiceManager => _serviceManager;
        public BaseApiController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        
        protected ActionResult HandlePagedResult<T>(PagedList<T> result)
        {
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize,
                result.TotalCount, result.TotalPages);
            return Ok(result);
           
        }
    }
}
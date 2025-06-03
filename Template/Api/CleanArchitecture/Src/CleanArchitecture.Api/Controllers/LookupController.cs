using CleanArchitecture.Api.Abstraction;
using CleanArchitecture.Api.Helpers;
using CleanArchitecture.Api.Settings;
using CleanArchitecture.Application.Features.Queries.GetLookUp;
using CleanArchitecture.Core.Abstractions; 
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Extensions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(Constants.MyAllowSpecificOrigins)]
    public class LookupController(IDispatcher dispatcher, IOptions<AppSettings> appSettings) : BaseController(dispatcher, appSettings)
    {
        [HttpGet("Get/{name}")]
        [ProducesResponseType(typeof(Result<IEnumerable<LookUp>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string name)
        {
            GetLookUpQuery query = new(name);
            //var res = await _mediator.QueryAsync<GetLookUpQuery, IEnumerable<LookUp>>(query);
            Result<IEnumerable<LookUp>> result = await _mediator.QueryAsync(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}

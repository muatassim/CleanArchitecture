using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CleanArchitecture.Api.Abstraction;
using CleanArchitecture.Api.Settings;
using CleanArchitecture.Application.Features.Commands.Categories.Add;
using CleanArchitecture.Application.Features.Commands.Categories.Delete;
using CleanArchitecture.Application.Features.Commands.Categories.Update;
using CleanArchitecture.Application.Features.Queries.Categories.Get;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using CleanArchitecture.Core.Shared;
using CleanArchitecture.Core.Extensions;
using CleanArchitecture.Application.Features.Queries.Categories.GetById;
namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CategoriesController(IDispatcher dispatcher, IOptions<AppSettings> appSettings) : BaseController(dispatcher, appSettings)
    {
        // GET: api/Categories
        /// <summary>
        /// Returns default values of Categories 
        /// </summary>
        /// <returns></returns>  
        [HttpGet]
        [HttpGet("Get")] 
        [ProducesResponseType(typeof(Result<Page<CategoriesModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Error>), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [EndpointSummary("Get all Categories")]
        [EndpointDescription("Get all Categories")]       
        public async Task<IActionResult> Get([FromQuery] SearchRequest searchRequest)
        {
            if (searchRequest == null)
            {
                return BadRequest("Search Request is required!");
            }
            IQuery<Page<CategoriesModel>> request = new GetCategoriesSearchRequest(searchRequest);
            var result = await _mediator.QueryAsync(request);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("Get/{Id}")]
        [ProducesResponseType(typeof(Result<CategoriesModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Error>), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(int Id)
        {
            GetCategoriesByIdQueryRequest query = new(Id);
            var result = await _mediator.QueryAsync(query);
            return result == null ? NotFound() : Ok(result);


        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        [ProducesResponseType(typeof(Result<Error>), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int Id)
        {
            DeleteCategoriesCommandRequest command = new(Id);
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }
        [HttpPost]
        [Route("POST")]
        [ProducesResponseType(typeof(CategoriesModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<Error>), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Post( CategoriesModel model)
        {
            AddCategoriesCommandRequest command = new(model);
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }
        [HttpPut]
        [Route("PUT")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Error>), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Put(CategoriesModel model)
        {
            UpdateCategoriesCommandRequest command = new(model);
            var result = await _mediator.SendAsync(command);
            return Ok(result);
        }
    }
}

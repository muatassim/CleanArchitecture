using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CleanArchitecture.Api.Settings;
using CleanArchitecture.Core.Interfaces;
namespace CleanArchitecture.Api.Abstraction
{
    public abstract class BaseController(IDispatcher dispatcher, IOptions<AppSettings> appSettings) : ControllerBase
    {
        protected readonly IDispatcher _mediator = dispatcher;
        protected readonly AppSettings _appSettings = appSettings.Value;
    }
}

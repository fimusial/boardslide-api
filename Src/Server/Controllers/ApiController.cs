using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using BoardSlide.API.Server.Contracts;
using MediatR;

namespace BoardSlide.API.Server.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected readonly IUriService UriService;

        public ApiController(IUriService uriService)
        {
            UriService = uriService;
        }
    }
}
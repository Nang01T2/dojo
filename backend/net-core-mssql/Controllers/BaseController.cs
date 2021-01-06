using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace net_core_mssql.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BaseController : ControllerBase
  {
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
  }
}
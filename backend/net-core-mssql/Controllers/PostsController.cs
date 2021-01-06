using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_core_mssql.Application.Posts.Queries.GetAllPosts;

namespace net_core_mssql.Controllers
{
  public class PostsController : BaseController
  {
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllPostsDto>>> GetAllPosts()
    {
      var response = await Mediator.Send(new GetAllPostsQuery());

      return Ok(response);
    }
  }
}
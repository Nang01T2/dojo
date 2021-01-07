using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROJECTNAME.Core.Posts.Queries.GetAllPosts;

namespace PROJECTNAME.Api.Controllers
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
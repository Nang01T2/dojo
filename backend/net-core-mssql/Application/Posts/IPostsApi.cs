using System.Collections.Generic;
using System.Threading.Tasks;
using net_core_mssql.Application.Posts.Queries.GetAllPosts;

namespace net_core_mssql.Application.Posts
{
  public interface IPostsApi
  {
    Task<IEnumerable<GetAllPostsResponse>> GetAllPosts();
  }
}
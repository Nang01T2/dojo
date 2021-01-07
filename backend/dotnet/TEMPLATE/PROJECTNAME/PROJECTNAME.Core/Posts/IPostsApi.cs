using System.Collections.Generic;
using System.Threading.Tasks;
using PROJECTNAME.Core.Posts.Queries.GetAllPosts;

namespace PROJECTNAME.Core.Posts
{
  public interface IPostsApi
  {
    Task<IEnumerable<GetAllPostsResponse>> GetAllPosts();
  }
}
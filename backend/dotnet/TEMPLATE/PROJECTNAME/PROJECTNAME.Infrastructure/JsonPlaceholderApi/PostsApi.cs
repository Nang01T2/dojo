using System.Collections.Generic;
using System.Threading.Tasks;
using PROJECTNAME.Core.Posts;
using PROJECTNAME.Core.Posts.Queries.GetAllPosts;

namespace PROJECTNAME.Infrastructure.JsonPlaceholderApi
{
  public class PostsApi : IPostsApi
  {
    private readonly JsonPlaceholderClient client;
    public PostsApi(JsonPlaceholderClient client)
    {
      this.client = client;
    }

    public async Task<IEnumerable<GetAllPostsResponse>> GetAllPosts()
    {
      return await client.GetAllPosts();
    }
  }
}
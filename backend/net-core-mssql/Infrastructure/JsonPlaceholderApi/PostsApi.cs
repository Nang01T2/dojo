using System.Collections.Generic;
using System.Threading.Tasks;
using net_core_mssql.Application.Posts;
using net_core_mssql.Application.Posts.Queries.GetAllPosts;

namespace net_core_mssql.Infrastructure.JsonPlaceholderApi
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
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PROJECTNAME.Core.Posts.Queries.GetAllPosts;

namespace PROJECTNAME.Infrastructure.JsonPlaceholderApi
{
  public class JsonPlaceholderClient : BaseHttpClient
  {
    public JsonPlaceholderClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<GetAllPostsResponse>> GetAllPosts()
    {
      return await Get<IEnumerable<GetAllPostsResponse>>(Endpoints.Posts.GetAllPosts);
    }
  }
}
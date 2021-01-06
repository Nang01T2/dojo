using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using net_core_mssql.Application.Posts.Queries.GetAllPosts;

namespace net_core_mssql.Infrastructure.JsonPlaceholderApi
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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using net_core_mssql.Services;

namespace net_core_mssql.Application.Posts.Queries.GetAllPosts
{
  public class GetAllPostsQuery : IRequest<ServiceResponse<IEnumerable<GetAllPostsDto>>>
  {
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, ServiceResponse<IEnumerable<GetAllPostsDto>>>
    {
      private readonly IPostsApi _postsApi;
      private readonly IMapper _mapper;

      public GetAllPostsQueryHandler(IPostsApi postsApi, IMapper mapper)
      {
        _postsApi = postsApi;
        _mapper = mapper;
      }

      public async Task<ServiceResponse<IEnumerable<GetAllPostsDto>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
      {
        ServiceResponse<IEnumerable<GetAllPostsDto>> serviceResponse = new ServiceResponse<IEnumerable<GetAllPostsDto>>();
        var posts = await _postsApi.GetAllPosts();

        serviceResponse.Data = _mapper.Map<IEnumerable<GetAllPostsDto>>(posts);
        return serviceResponse;
      }

    }

  }
}
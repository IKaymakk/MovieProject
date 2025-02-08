using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Comment.Queries;
using MovieProject.Application.Features.Comment.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Handlers
{
    public class GetCommentsByUserIdQueryHandler : IRequestHandler<GetCommentsByUserIdQuery, List<GetCommentsByUserIdQueryResult>>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Comment> _repository;
        private readonly IMapper _mapper;

        public GetCommentsByUserIdQueryHandler(IRepository<MovieProject_Domain.Entities.Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCommentsByUserIdQueryResult>> Handle(GetCommentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetAllWithIncludesAsync(
                x => x.AppUserId == request.userId,
                x => x.Movie
                );
            return _mapper.Map<List<GetCommentsByUserIdQueryResult>>(comments);
        }
    }
}

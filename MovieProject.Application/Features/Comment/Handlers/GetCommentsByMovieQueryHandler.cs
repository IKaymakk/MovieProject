using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Comment.Queries;
using MovieProject.Application.Features.Comment.Results;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.Movie.Results;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Handlers
{
    public class GetCommentsByMovieQueryHandler : IRequestHandler<GetCommentsByMovieQuery, PaginatedCommentResult>
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public GetCommentsByMovieQueryHandler(IMapper mapper, ICommentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PaginatedCommentResult> Handle(GetCommentsByMovieQuery request, CancellationToken cancellationToken)
        {
            var (comments, totalCount) = await _repository.GetCommentsByMovieId(request.id, request.SortBy, request.Page, request.PageSize);

            var mappedComments = _mapper.Map<List<GetCommentsByMovieQueryResult>>(comments);

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new PaginatedCommentResult
            {
                Comments = mappedComments,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = request.Page
            };
        }
    }
}

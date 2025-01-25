using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Comment.Queries;
using MovieProject.Application.Features.Comment.Results;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Handlers
{
    public class GetCommentsByMovieQueryHandler : IRequestHandler<GetCommentsByMovieQuery, List<GetCommentsByMovieQueryResult>>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Comment> _repository;
        private readonly IMapper _mapper;

        public GetCommentsByMovieQueryHandler(IRepository<MovieProject_Domain.Entities.Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCommentsByMovieQueryResult>> Handle(GetCommentsByMovieQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetAllWithIncludesAsync(
                x => x.MovieId == request.id, // Filtreleme: sadece istenen MovieId'ye göre
                x => x.AppUser               // Include: Yorum yazan kullanıcı bilgisi
            );

            var mappedComments = _mapper.Map<List<GetCommentsByMovieQueryResult>>(comments);

            return mappedComments;

        }
    }
}

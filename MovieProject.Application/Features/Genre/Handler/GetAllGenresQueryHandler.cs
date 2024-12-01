using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Genre.Queries;
using MovieProject.Application.Features.Genre.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Genre.Handler
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, List<GetAllGenresQueryResult>>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Genre> _genreRepository;
        private readonly IMapper _mapper;

        public GetAllGenresQueryHandler(IMapper mapper, IRepository<MovieProject_Domain.Entities.Genre> genreRepository)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task<List<GetAllGenresQueryResult>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.GetAllAsync();
            var mappedgenres = _mapper.Map<List<GetAllGenresQueryResult>>(genres);
            return mappedgenres;
        }
    }
}

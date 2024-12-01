using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Actor.Queries;
using MovieProject.Application.Features.Actor.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Actor.Handlers
{
    public class GetMovieActorsQueryHandler : IRequestHandler<GetMovieActorsQuery, List<GetMovieActorsQueryResult>>
    {
        private readonly IActorRepository _repository;
        private readonly IMapper _mapper;

        public GetMovieActorsQueryHandler(IActorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetMovieActorsQueryResult>> Handle(GetMovieActorsQuery request, CancellationToken cancellationToken)
        {
            var actors = await _repository.GetMovieActors(request.Id);
            var mappedactors = _mapper.Map<List<GetMovieActorsQueryResult>>(actors);
            return mappedactors;
        }
    }
}

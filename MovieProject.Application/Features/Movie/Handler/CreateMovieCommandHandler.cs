using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Movie.Commands;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand>
{
    private readonly IRepository<MovieProject_Domain.Entities.Movie> _repository;
    private readonly IMapper _mapper;

    public CreateMovieCommandHandler(IRepository<MovieProject_Domain.Entities.Movie> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var newmovie = _mapper.Map<MovieProject_Domain.Entities.Movie>(request);
        newmovie.ReleaseDate = DateTime.Now;
        await _repository.CreateAsync(newmovie);
    }

}

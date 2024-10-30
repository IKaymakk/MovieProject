using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Movie.Commands;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
{
    private readonly IRepository<MovieProject_Domain.Entities.Movie> _repository;
    private readonly IMapper _mapper;

    public UpdateMovieCommandHandler(IRepository<MovieProject_Domain.Entities.Movie> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _repository.GetByIdAsync(request.Id);
        _mapper.Map(request, movie);
        await _repository.UpdateAsync(movie);
    }
}

using AutoMapper;
using MediatR;
using MovieProject.Application.Features.FavoriteMovie.Command;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.FavoriteMovie.Handler;

public class CreateFavoriteMovieCommandHandler : IRequestHandler<CreateFavoriteMovieCommand, bool>
{
    private readonly IFavoriteMovieRepository _repository;
    private readonly IMapper _mapper;

    public CreateFavoriteMovieCommandHandler(IFavoriteMovieRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateFavoriteMovieCommand request, CancellationToken cancellationToken)
    {
        var isFavorited = await _repository.ChangeFavoritedStatus(request.AppUserId, request.MovieId);
        return isFavorited;
    }


}

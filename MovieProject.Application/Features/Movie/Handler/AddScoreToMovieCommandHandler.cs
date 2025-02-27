using MediatR;
using MovieProject.Application.Features.Movie.Commands;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler
{
    public class AddScoreToMovieCommandHandler : IRequestHandler<AddScoreToMovieCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Movie> _repository;
        private readonly IRepository<MovieProject_Domain.Entities.AppUser> _apprepository;

        public AddScoreToMovieCommandHandler(IRepository<MovieProject_Domain.Entities.Movie> repository, IRepository<MovieProject_Domain.Entities.AppUser> apprepository)
        {
            _repository = repository;
            _apprepository = apprepository;
        }

        public async Task Handle(AddScoreToMovieCommand request, CancellationToken cancellationToken)
        {
            var user = await _apprepository.GetByIdAsync(request.AppUserId);
            var movie = await _repository.GetByIdAsync(request.MovieId);

            if (user == null || movie == null)
                throw new Exception("Film ya da kullanıcı bulunamadı");

            movie.Score = request.Score; 

            await _repository.UpdateAsync(movie); 
        }

    }
}

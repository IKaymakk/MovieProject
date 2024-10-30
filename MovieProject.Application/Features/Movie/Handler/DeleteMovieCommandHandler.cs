using MediatR;
using MovieProject.Application.Features.Movie.Commands;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Movie> _repository;

        public DeleteMovieCommandHandler(IRepository<MovieProject_Domain.Entities.Movie> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _repository.GetByIdAsync(request.id);
            await _repository.RemoveAsync(movie);
        }
    }
}

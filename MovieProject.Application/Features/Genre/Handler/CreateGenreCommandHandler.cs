using MediatR;
using MovieProject.Application.Features.Genre.Commands;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Genre.Handler
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Genre> _repository;

        public CreateGenreCommandHandler(IRepository<MovieProject_Domain.Entities.Genre> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var newgenre = new MovieProject_Domain.Entities.Genre
            {
                Image = request.Image,
                Name = request.Name,
            };
            await _repository.CreateAsync(newgenre);
        }
    }
}
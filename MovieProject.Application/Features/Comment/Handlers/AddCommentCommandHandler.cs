using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Comment.Commands;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Handlers
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Comment> _repository;
        private readonly IMapper _mapper;

        public AddCommentCommandHandler(IRepository<MovieProject_Domain.Entities.Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var newComment = _mapper.Map<MovieProject_Domain.Entities.Comment>(request);
            newComment.CreatedDate = DateTime.Now;
            await _repository.CreateAsync(newComment);

        }
    }
}

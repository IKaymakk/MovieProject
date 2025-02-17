using AutoMapper;
using MediatR;
using MovieProject.Application.Features.AppUser.Commands;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Handlers
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.AppUser> _repository;
        private readonly IMapper _mapper;

        public UpdateUserDetailsCommandHandler(IRepository<MovieProject_Domain.Entities.AppUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.id);
            _mapper.Map(request, user);
            await _repository.UpdateAsync(user);

        }
    }
}

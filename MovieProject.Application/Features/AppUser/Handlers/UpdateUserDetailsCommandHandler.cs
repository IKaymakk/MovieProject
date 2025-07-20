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
    public class UpdateUserDetailsCommandHandler(
        IRepository<MovieProject_Domain.Entities.AppUser> repository,
        IMapper mapper)
        : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.id);
            mapper.Map(request, user);
            await repository.UpdateAsync(user);

        }
    }
}

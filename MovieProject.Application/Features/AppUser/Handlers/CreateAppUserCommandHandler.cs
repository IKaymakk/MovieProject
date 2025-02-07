using AutoMapper;
using MediatR;
using MovieProject.Application.Features.AppUser.Commands;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Handlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.AppUser> _repository;
        private readonly IMapper _mapper;
        public CreateAppUserCommandHandler(IRepository<MovieProject_Domain.Entities.AppUser> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new MovieProject_Domain.Entities.AppUser
            {
                FirstName = request.FirstName,
                UserName = request.UserName,
                LastName = request.LastName,
                MailAddress = request.MailAddress,
                Image = request.Image,
                Password = request.Password,
                AppRoleId = 1
            });
        }
    }
}

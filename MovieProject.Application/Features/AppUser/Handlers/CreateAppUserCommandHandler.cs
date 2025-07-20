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
        private readonly IAppUserRepository _appuserrepository;

        public CreateAppUserCommandHandler(IRepository<MovieProject_Domain.Entities.AppUser> repository, IMapper mapper, IAppUserRepository _appuserrepository)
        {
            _mapper = mapper;
            this._appuserrepository = _appuserrepository;
            _repository = repository;
        }
        public async Task Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.AnyAsync(x => x.UserName == request.UserName))
                throw new Exception("Bu kullanıcı adı zaten kayıtlı.");

            var user = new MovieProject_Domain.Entities.AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                MailAddress = request.MailAddress,
                Image = request.Image,
                Password = _appuserrepository.Hash(request.Password),
                AppRoleId = 1
            };

            await _repository.CreateAsync(user);
        }
            
    }
}

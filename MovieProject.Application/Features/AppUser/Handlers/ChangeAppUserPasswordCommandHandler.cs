using AutoMapper;
using MediatR;
using MovieProject.Application.Features.AppUser.Commands;
using MovieProject.Application.Interfaces;
using MovieProject.Application.Registrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Handlers
{
    public class ChangeAppUserPasswordCommandHandler : IRequestHandler<ChangeAppUserPasswordCommand, Result>
    {
        private readonly IRepository<MovieProject_Domain.Entities.AppUser> _repository;
        private readonly IAppUserRepository _appUserRepository;

        public ChangeAppUserPasswordCommandHandler(IRepository<MovieProject_Domain.Entities.AppUser> repository, IAppUserRepository _appUserRepository)
        {
            _repository = repository;
            this._appUserRepository = _appUserRepository;
        }

        public async Task<Result> Handle(ChangeAppUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.id);
            if (user == null)
                return Result.Failure("Kullanıcı bulunamadı.");

            bool isOldPasswordCorrect = await _appUserRepository.VerifyAndFixHashIfNeeded(user, request.oldPassword);
            if (!isOldPasswordCorrect)
                return Result.Failure("Eski şifreniz hatalı.");

            // Yeni şifreyi hashle ve kaydet
            user.Password = _appUserRepository.Hash(request.newPassword);
            await _repository.UpdateAsync(user);

            return Result.Success();
        }



    }
}

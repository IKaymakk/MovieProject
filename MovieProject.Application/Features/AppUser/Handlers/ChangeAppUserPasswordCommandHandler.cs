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

        public ChangeAppUserPasswordCommandHandler(IRepository<MovieProject_Domain.Entities.AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(ChangeAppUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.id);

            if (user.Password != request.oldPassword)
            {
                return Result.Failure("Eski şifreniz hatalı.");
            }

            user.Password = request.newPassword;

            await _repository.UpdateAsync(user);

            return Result.Success();
        }

    }
}

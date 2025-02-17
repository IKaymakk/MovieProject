using MediatR;
using MovieProject.Application.Registrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Commands
{
    public class ChangeAppUserPasswordCommand : IRequest<Result>  
    {
        public int id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}

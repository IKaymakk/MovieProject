using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Commands
{
    public class CreateAppUserCommand:IRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public int AppRoleId { get; set; } = 1;
    }
}

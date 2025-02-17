using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Commands
{
    public class UpdateUserDetailsCommand:IRequest
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mailAddress { get; set; }
    }
}

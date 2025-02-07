using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Results
{
    public class GetAllAppUsersQueryResult
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public int AppRoleId { get; set; }
    }
}

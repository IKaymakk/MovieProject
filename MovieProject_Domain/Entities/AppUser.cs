using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject_Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }

        public AppRole AppRole { get; set; }
        [ForeignKey(nameof(AppRole))]
        public int AppRoleId { get; set; }

        public ICollection<FavoriteMovie> FavoriteMovies { get; set; } // Ara tabloya referans


    }
}

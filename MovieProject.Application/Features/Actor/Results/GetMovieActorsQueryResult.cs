using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Actor.Results
{
    public class GetMovieActorsQueryResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CastName { get; set; }
        public int MovieId { get; set; }
    }
}

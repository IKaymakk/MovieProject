using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Commands
{
    public class AddScoreToMovieCommand : IRequest
    {
        public int MovieId { get; set; }
        public int AppUserId { get; set; }
        public decimal Score { get; set; }
    }
}

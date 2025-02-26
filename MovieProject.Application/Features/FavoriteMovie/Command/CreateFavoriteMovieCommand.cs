using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.FavoriteMovie.Command
{
    public class CreateFavoriteMovieCommand : IRequest<bool>
    {
        public int AppUserId { get; set; }
        public int MovieId { get; set; }
    }
}

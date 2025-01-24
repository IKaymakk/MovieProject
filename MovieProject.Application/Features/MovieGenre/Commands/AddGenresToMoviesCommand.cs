using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.MovieGenre.Commands
{
    public class AddGenresToMoviesCommand : IRequest
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}

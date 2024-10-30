using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Commands
{
    public class DeleteMovieCommand:IRequest
    {
        public int id { get; set; }

        public DeleteMovieCommand(int id)
        {
            this.id = id;
        }
    }
}

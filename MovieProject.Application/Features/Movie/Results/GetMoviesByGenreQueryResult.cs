using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Results;

public class GetMoviesByGenreQueryResult
{
    public int Id { get; set; }
    public int score { get; set; }

    public string Name { get; set; }
    public string Image { get; set; }
   
}

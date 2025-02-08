using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Results;

public class GetFavoritedMoviesByUserQueryResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Score { get; set; }
    public DateTime ReleaseDate { get; set; }
}

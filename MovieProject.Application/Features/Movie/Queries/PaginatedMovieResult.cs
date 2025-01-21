using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries;

public class PaginatedMovieResult
{
    public List<GetMoviesByFilterQueryResult> Movies { get; set; } = new List<GetMoviesByFilterQueryResult>();
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}


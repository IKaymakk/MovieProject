using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos;

public class PaginatedMovieDto
{
    public List<MovieDto> Movies { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}

public class MovieDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Score { get; set; }
    public DateTime ReleaseDate { get; set; }
}

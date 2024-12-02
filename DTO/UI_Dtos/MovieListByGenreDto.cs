using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UI_Dtos;

public class MovieListByGenreDto
{
    public int id { get; set; }
    public decimal score { get; set; }
    public string name { get; set; }
    public string image { get; set; }
}
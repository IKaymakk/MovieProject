using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.DTOS;

public class MovieDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Score { get; set; }
    public int RunTime { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Image { get; set; }
    public List<string> Genres { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.DTOS
{
    public class FilterListDto
    {
        //public int? GenreId { get; set; }
        //public decimal? Score { get; set; }
        //public string? Name { get; set; }
        //public DateTime? ReleaseDate { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

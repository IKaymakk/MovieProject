using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Results
{
    public class PaginatedCommentResult
    {
            public List<GetCommentsByMovieQueryResult> Comments { get; set; } = new();
            public int TotalCount { get; set; } // Toplam kayıt sayısı
            public int TotalPages { get; set; } // Toplam sayfa sayısı
            public int CurrentPage { get; set; } // Mevcut sayfa
            public int id { get; set; } // Mevcut sayfa
    }
}

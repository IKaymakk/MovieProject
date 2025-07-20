using MovieProject.Application.DTOS;
using MovieProject.Application.Features.Movie.Results;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Interfaces;
public interface IMovieRepository : IRepository<Movie>
{
    Task<List<Movie>> GetLast24Movie();
    Task<Movie> GetMovieDetails(int id);
    Task<List<Movie>> GetMoviesByCategory(int id);
    Task<(List<Movie>, int)> GetMoviesByCategoryWithPaging(FilterListDto options, int id);
    Task<List<Movie>> GetFilterMoviesList(FilterListDto options);
    Task<(List<Movie>, int)> GetFilterMoviesListWithCount(FilterListDto options);
    Task<List<Movie>> GetSimilarMovies(string Hashtag);
    Task<(List<Movie>, int)> SearchMoviesWithSortingAndCount(string? searchTerm, string? sortBy, int page, int pageSize, int? categoryId);
    Task<(List<GetMoviesByFilterQueryResult>, int)> GetFavoritedMoviesByUser(int userId, string? sortBy, int page, int pageSize);


}

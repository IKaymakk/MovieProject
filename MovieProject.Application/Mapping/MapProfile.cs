using AutoMapper;
using MovieProject.Application.Features.Actor.Results;
using MovieProject.Application.Features.Genre.Results;
using MovieProject.Application.Features.Movie.Commands;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.Movie.Results;
using MovieProject.Application.Features.MovieGenre.Commands;
using MovieProject.Application.Features.MovieGenre.Results;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            #region Movie
            CreateMap<Movie, GetAllMoviesWithGenresQueryResult>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre.Name).ToList()));
            CreateMap<Movie, GetTop24MoviesQueryResult>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => mg.Genre.Name).ToList()));
            CreateMap<CreateMovieCommand, Movie>().ReverseMap();
            CreateMap<GetMovieByIdQueryResult, Movie>().ReverseMap();
            CreateMap<UpdateMovieCommand, Movie>().ReverseMap();
            CreateMap<GetAllMoviesQueryResult, Movie>().ReverseMap();
            CreateMap<GetLast15MoviesQueryResult, Movie>().ReverseMap();
            CreateMap<GetMoviesByGenreQueryResult, Movie>().ReverseMap();
            CreateMap<GetMoviesByFilterQueryResult, Movie>().ReverseMap();
            #endregion

            #region Genre
            CreateMap<GetAllGenresQueryResult, Genre>().ReverseMap();
            #endregion

            #region Actor
            CreateMap<GetMovieActorsQueryResult, Actor>().ReverseMap();
            #endregion

            #region MovieGenre

            CreateMap<MovieGenre, AddGenresToMoviesCommand>().ReverseMap();

            #endregion
        }
    }
}

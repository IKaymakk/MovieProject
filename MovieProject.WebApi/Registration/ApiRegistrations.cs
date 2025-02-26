﻿using MovieProject.Application.Features.Genre.Handler;
using MovieProject.Application.Features.Movie.Handler;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject.Persistance.Repositories;

namespace MovieProject.WebApi.Registration
{
    public static class ApiRegistrations
    {
        public static void AddApiApplicationServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<MovieContext>();
            Services.AddScoped<CreateMovieCommandHandler>();
            Services.AddScoped<GetMovieByIdQueryHandler>();
            Services.AddScoped<GetAllGenresQueryHandler>();


            Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            Services.AddScoped<IMovieRepository, MovieRepository>();
            Services.AddScoped<IMovieGenreRepository, MovieGenreRepository>();
            Services.AddScoped<IGenreRepository, GenreRepository>();
            Services.AddScoped<IActorRepository, ActorRepository>();
            Services.AddScoped<IAppUserRepository, AppUserRepository>();
            Services.AddScoped<ICommentRepository, CommentRepository>();
            Services.AddScoped<IFavoriteMovieRepository, FavoriteMovieRepository>();



        }
    }
}

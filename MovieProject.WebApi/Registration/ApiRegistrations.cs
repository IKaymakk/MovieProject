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


            Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            Services.AddScoped<IMovieRepository, MovieRepository>();

        }
    }
}

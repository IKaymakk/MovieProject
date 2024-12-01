using Microsoft.EntityFrameworkCore;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Context;

public class MovieContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=KAYMAK\\SQLEXPRESS;database=MovieProjectDB; integrated security=true;TrustServerCertificate=True;");
    }

    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<FavoriteMovie> FavoriteMovies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // FavoriteMovie için ilişki tanımlaması
        modelBuilder.Entity<FavoriteMovie>()
            .HasKey(fm => new { fm.AppUserId, fm.MovieId }); // Birleştirilmiş anahtar

        modelBuilder.Entity<FavoriteMovie>()
            .HasOne(fm => fm.AppUser)
            .WithMany(u => u.FavoriteMovies)
            .HasForeignKey(fm => fm.AppUserId);

        modelBuilder.Entity<FavoriteMovie>()
            .HasOne(fm => fm.Movie)
            .WithMany(m => m.FavoriteMovies)
            .HasForeignKey(fm => fm.MovieId);

        // MovieGenre için ilişki tanımlaması
        modelBuilder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId }); // Birleştirilmiş anahtar

        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.MovieGenres)
            .HasForeignKey(mg => mg.MovieId);

        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(mg => mg.GenreId);
    }
}
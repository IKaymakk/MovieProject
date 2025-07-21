using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Features.Comment.Results;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieProject.Persistance.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly MovieContext _context;

    public CommentRepository(MovieContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetCommentByUserId(int id)
    {
        return await _context.Comments
            .AsNoTracking()
            .Where(x => x.AppUserId == id)
            .ToListAsync();
    }

    public async Task<(List<GetCommentsByMovieQueryResult>, int)> GetCommentsByMovieId(int movieId, string? sortBy, int page, int pageSize)
    {
        var query = _context.Comments
            .AsNoTracking()
            .Where(x => x.MovieId == movieId)
            .Include(x => x.AppUser)
            .Select(cm => new GetCommentsByMovieQueryResult
            {
                AppUserId = cm.AppUserId,
                CommentTitle = cm.CommentTitle,
                CommentText = cm.CommentText,
                CreatedDate = cm.CreatedDate,
                MovieId = cm.MovieId,
                AppUserImage = cm.AppUser.Image,
                FirstName = cm.AppUser.FirstName,
                LastName = cm.AppUser.LastName,
                UserName = cm.AppUser.UserName
            });

        if (!string.IsNullOrEmpty(sortBy))
        {
            query = sortBy switch
            {
                "new" => query.OrderBy(x => x.CreatedDate),
                "old" => query.OrderByDescending(x => x.CreatedDate),
                _ => query
            };
        }

        // Toplam kayıt sayısı
        var totalCount = await query.CountAsync();

        // Sayfalama
        if (page > 0 && pageSize > 0)
        {
            var skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);
        }

        var comments = await query.ToListAsync();

        return (comments, totalCount);
    }
}
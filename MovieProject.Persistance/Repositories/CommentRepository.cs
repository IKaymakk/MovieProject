using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories
{
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
    }
}

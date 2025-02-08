using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentByUserId(int id);
    }
}

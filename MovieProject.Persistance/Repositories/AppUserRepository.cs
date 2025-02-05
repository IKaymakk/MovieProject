using Microsoft.EntityFrameworkCore;
using MovieProject.Application.DTOS;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly MovieContext _context;

        public AppUserRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<UserDataDto> CheckUser(CheckUserDto checkUserDto)
        {
            UserDataDto responseDto = new();
            var user = await _context.AppUsers
                .AsNoTracking()
                .Where(x => x.UserName == checkUserDto.UserName && x.Password == checkUserDto.Password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                responseDto.IsExist = false;
            }

            else
            {
                responseDto.IsExist = true;
                responseDto.UserName = user.UserName;
                responseDto.Role = await GetAppUserRoleAsync(user.Id);
                responseDto.AppUserId = user.Id;
            }
            return responseDto;
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUser>> GetAllWithIncludesAsync(Expression<Func<AppUser, bool>> filter = null, params Expression<Func<AppUser, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetAppUserRoleAsync(int id)
        {
            var appUser = _context.AppUsers
                .AsNoTracking()
                .Include(x => x.AppRole)
                .Where(x => x.Id == id)
                .Select(x => x.AppRole.Name)
                .FirstOrDefault();

            if (appUser == null)
            {
                // Hata durumunda uygun bir işlem yapılabilir (örn. özel bir exception fırlatılabilir)
                throw new KeyNotFoundException("Rol bulunamadı.");
            }

            return appUser;

        }

        public Task<AppUser> GetByFilterAsync(Expression<Func<AppUser, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser entity)
        {
            throw new NotImplementedException();
        }
    }
}

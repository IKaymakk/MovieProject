﻿using MovieProject.Application.DTOS;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Interfaces
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<string> GetAppUserRoleAsync(int id);
        Task<UserDataDto> CheckUser(CheckUserDto checkUserDto);
        string Hash(string password);
        bool Verify(string hashedPassword, string plainPassword);
        bool IsHashed(string password);
        Task<bool> VerifyAndFixHashIfNeeded(AppUser user, string plainPassword);


    }
}

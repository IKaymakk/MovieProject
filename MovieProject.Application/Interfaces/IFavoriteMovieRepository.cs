﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Interfaces;

public interface IFavoriteMovieRepository
{
    Task<bool> ChangeFavoritedStatus(int userId,int movieId);
}

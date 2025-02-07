﻿using MediatR;
using MovieProject.Application.Features.AppUser.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Queries
{
    public class GetAllAppUsersQuery : IRequest<List<GetAllAppUsersQueryResult>>
    {
    }
}

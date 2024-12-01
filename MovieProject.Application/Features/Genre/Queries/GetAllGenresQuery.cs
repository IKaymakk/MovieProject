using MediatR;
using MovieProject.Application.Features.Genre.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Genre.Queries
{
    public class GetAllGenresQuery : IRequest<List<GetAllGenresQueryResult>>
    {
    }
}

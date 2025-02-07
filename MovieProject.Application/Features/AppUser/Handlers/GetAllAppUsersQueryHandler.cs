using AutoMapper;
using MediatR;
using MovieProject.Application.Features.AppUser.Queries;
using MovieProject.Application.Features.AppUser.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Handlers
{
    public class GetAllAppUsersQueryHandler : IRequestHandler<GetAllAppUsersQuery, List<GetAllAppUsersQueryResult>>
    {
        private readonly IRepository<MovieProject_Domain.Entities.AppUser> _repository;
        private readonly IMapper _mapper;

        public GetAllAppUsersQueryHandler(IRepository<MovieProject_Domain.Entities.AppUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllAppUsersQueryResult>> Handle(GetAllAppUsersQuery request, CancellationToken cancellationToken)
        {
            var appUsers = await _repository.GetAllAsync();
            return _mapper.Map<List<GetAllAppUsersQueryResult>>(appUsers);
        }
    }
}

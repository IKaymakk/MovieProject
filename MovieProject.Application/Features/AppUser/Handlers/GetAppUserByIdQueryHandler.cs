using AutoMapper;
using MediatR;
using MovieProject.Application.Features.AppUser.Queries;
using MovieProject.Application.Features.AppUser.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.AppUser.Handlers
{
    public class GetAppUserByIdQueryHandler : IRequestHandler<GetAppUserByIdQuery, GetAppUserByIdQueryResult>
    {
        private readonly IRepository<MovieProject_Domain.Entities.AppUser> _repository;
        private readonly IMapper _mapper;

        public GetAppUserByIdQueryHandler(IRepository<MovieProject_Domain.Entities.AppUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAppUserByIdQueryResult> Handle(GetAppUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.id);
            var mappedUser = _mapper.Map<GetAppUserByIdQueryResult>(user);
            return mappedUser;
        }
    }
}

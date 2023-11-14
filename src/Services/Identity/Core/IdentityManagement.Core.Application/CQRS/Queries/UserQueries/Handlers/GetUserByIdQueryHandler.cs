using AutoMapper;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Requests;
using IdentityManagement.Core.Application.DTOs.UserDTOs;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Exceptions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Handlers;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public GetUserByIdQueryHandler(IMapper mapper, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _mapper = mapper;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = Optional<User>.Of(await _userReadOnlyRepository.GetByIdAsync(request.UserId))
            .ThrowIfNotExist(new UserNotFoundException(request.UserId)).Get();

        return _mapper.Map<UserDto>(user);
    }
}
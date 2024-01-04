using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.Users.DTOs;

namespace IdentityManagement.Core.Application.Users.CQRS.Queries.Requests;

public record GetCurrentUserQuery : IQuery<UserSummaryDto>;
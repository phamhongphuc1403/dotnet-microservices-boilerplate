using BuildingBlock.Core.Application;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Services;
using NotificationManagement.Infrastructure.Firebase.CQRS.Commands.Requests;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.DomainServices;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Entities;
using NotificationManagement.Infrastructure.Firebase.DeviceTokenAggregate.Specifications;

namespace NotificationManagement.Infrastructure.Firebase.CQRS.Commands.Handlers;

public class RegisterDeviceTokenCommandHandler : ICommandHandler<RegisterDeviceTokenCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IDeviceTokenDomainService _deviceTokenDomainService;
    private readonly IOperationRepository<DeviceToken> _deviceTokenOperationRepository;
    private readonly IReadOnlyRepository<DeviceToken> _deviceTokenReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDeviceTokenCommandHandler(IReadOnlyRepository<DeviceToken> deviceTokenReadOnlyRepository,
        IOperationRepository<DeviceToken> deviceTokenOperationRepository, IUnitOfWork unitOfWork,
        ICurrentUser currentUser, IDeviceTokenDomainService deviceTokenDomainService)
    {
        _deviceTokenReadOnlyRepository = deviceTokenReadOnlyRepository;
        _deviceTokenOperationRepository = deviceTokenOperationRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _deviceTokenDomainService = deviceTokenDomainService;
    }

    public async Task Handle(RegisterDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var deviceTokenTokenSpecification = new DeviceTokenTokenSpecification(request.Dto.Token);

        var deviceToken = await _deviceTokenReadOnlyRepository.GetAnyAsync(deviceTokenTokenSpecification);

        if (deviceToken is null)
        {
            var newDeviceToken = _deviceTokenDomainService.Create(request.Dto.Token, _currentUser.Id);

            await _deviceTokenOperationRepository.AddAsync(newDeviceToken);
        }
        else
        {
            _deviceTokenDomainService.Update(deviceToken);

            _deviceTokenOperationRepository.Update(deviceToken);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
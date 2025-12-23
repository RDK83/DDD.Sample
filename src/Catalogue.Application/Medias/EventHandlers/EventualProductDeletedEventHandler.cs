using Catalogue.Domain.Medias.Repositories;
using Catalogue.Domain.Products.Events;
using Mediator;
using Shared.Application.UnitOfWork;

namespace Catalogue.Application.Medias.EventHandlers;

public class EventualProductDeletedEventHandler : INotificationHandler<DeferredProductDeletedEvent>
{
    private readonly IMediaRepository _mediaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EventualProductDeletedEventHandler(IMediaRepository mediaRepository, IUnitOfWork unitOfWork)
    {
        _mediaRepository = mediaRepository;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask Handle(DeferredProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var mediaId in notification.MediaIds)
        {
            _mediaRepository.Remove(mediaId);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
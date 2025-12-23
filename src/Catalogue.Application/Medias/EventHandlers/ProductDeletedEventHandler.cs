using Catalogue.Domain.Medias.Repositories;
using Catalogue.Domain.Products.Events;
using Mediator;

namespace Catalogue.Application.Medias.EventHandlers;

public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
{
    private readonly IMediaRepository _mediaRepository;

    public ProductDeletedEventHandler(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }


    public ValueTask Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var mediaId in notification.MediaIds)
        {
            _mediaRepository.Remove(mediaId);
        }

        //Raise integration event for HSS to actually delete the physical files?

        return ValueTask.CompletedTask;
    }
}
using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Domain.Products.ValueObjects;
using Shared.Domain.Events;

namespace Catalogue.Domain.Products.Events;

public record ProductDeletedEvent(ProductCode ProductCode, IReadOnlyCollection<MediaId> MediaIds) : AtomicDomainEvent;
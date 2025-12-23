using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Lookups.ManufacturerClassifications;
using Catalogue.Domain.Lookups.ManufacturerClassifications.ValueObjects;
using Catalogue.Domain.Medias.ValueObjects;
using Catalogue.Domain.Merchants.ValueObjects;
using Catalogue.Domain.Products.Contexts;
using Catalogue.Domain.Products.Entities;
using Catalogue.Domain.Products.Mutations;
using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.ProductTypes.ValueObjects;
using Catalogue.Domain.WarehouseMerchants.Policies;
using Shared.Domain.Aggregates;
using Shared.Domain.Aggregates.Entities;
using Shared.Domain.Exceptions;
using Shared.Domain.Guard;

namespace Catalogue.Domain.Products;

public class Product : BaseAggregate<ProductCode>, IHasUpdatedTimeStamp
{
    public PromotedOfferId PromotedOfferId { get; private set; }

    public TaxClass TaxClass { get; private set; }

    public ProductTypeId ProductTypeId { get; private set; }

    public ProductSubTypeId ProductSubTypeId { get; private set; }

    public ProductTitle ProductTitle { get; private set; }

    public ProductDescription Description { get; private set; }

    public ManufacturerClassificationId ManufacturerClassificationId { get; private set; }

    public ManufacturerClassification ManufacturerClassification { get; private set; } = null!;

    public DateTime UpdatedAt => _updatedAt;

    DateTime IHasUpdatedTimeStamp.UpdatedAt
    {
        get => _updatedAt;
        set => _updatedAt = value;
    }

    public IReadOnlyCollection<ProductMedia> ProductMedias => _productMedias;


    public IReadOnlyCollection<Offer> Offers => _offers;

    private DateTime _updatedAt;

    private readonly List<ProductMedia> _productMedias;
    private readonly List<Offer> _offers;

    private Product()
    {
        //EF required
    }

    private Product(
        ProductCode id,
        ProductTitle productTitle,
        ProductDescription description,
        TaxClass taxClass,
        ProductTypeId productTypeId,
        ProductSubTypeId productSubTypeId,
        ManufacturerClassificationId manufacturerClassificationId
    )
    {
        Id = id;
        ProductTitle = productTitle;
        Description = description;
        TaxClass = taxClass;
        ProductTypeId = productTypeId;
        ProductSubTypeId = productSubTypeId;
        ManufacturerClassificationId = manufacturerClassificationId;

        _productMedias = [];
        _offers = [];
    }

    public static Product Create(
        ProductCode id,
        ProductTitle productTitle,
        ProductDescription description,
        TaxClass taxClass,
        ProductTypeId productTypeId,
        ProductSubTypeId productSubTypeId,
        ManufacturerClassificationId manufacturerClassificationId
    )
    {
        return new Product(
            id: id,
            productTitle: productTitle,
            description: description,
            taxClass: taxClass,
            productTypeId: productTypeId,
            productSubTypeId: productSubTypeId,
            manufacturerClassificationId: manufacturerClassificationId
        );
    }

    public void UpdateDetails(EditProductMutation mutation)
    {
        Guard.Against.Null(mutation);

        // strings
        ProductTitle = mutation.ProductTitle;
        Description = mutation.Description;

        // integers
        ManufacturerClassificationId = mutation.ManufacturerClassificationId;
        ProductSubTypeId = mutation.ProductSubTypeId;
        ProductTypeId = mutation.ProductTypeId;

        // enums
        TaxClass = mutation.TaxClass;
    }

    public HashSet<OfferId> GetViableOfferIds(ViableOfferCalculationContext context)
    {
        var returnValue = new HashSet<OfferId>();

        foreach (var offer in _offers)
        {
            if (offer.IsViable(context))
                returnValue.Add(offer.Id);
        }

        return returnValue;
    }

    public HashSet<OfferId> GetPreferredOfferIds(ViableOfferCalculationContext context,
        PreferredWarehouseMerchantPolicy preferredWarehouseMerchantPolicy)
    {
        var viableOfferIds = GetViableOfferIds(context);

        if (viableOfferIds.Count.Equals(0))
            return viableOfferIds;

        var viableOffers = _offers.Where(o => viableOfferIds.Contains(o.Id));

        var preferredWarehouseMerchants =
            preferredWarehouseMerchantPolicy.DeterminePreferredWarehouseMerchants(context.EligibleWarehouseMerchants);

        var preferredOfferIds = viableOffers
            .Where(offer => preferredWarehouseMerchants.Any(b =>
                b.MerchantId.Equals(offer.MerchantId) && b.WarehouseId.Equals(offer.WarehouseId)))
            .Select(offer => offer.Id).ToHashSet();

        return preferredOfferIds;
    }


    public void SetPromotedOffer(ViableOfferCalculationContext context,
        PreferredWarehouseMerchantPolicy preferredWarehouseMerchantPolicy)
    {
        var preferredOfferIds = GetPreferredOfferIds(context, preferredWarehouseMerchantPolicy);

        if (preferredOfferIds.Count.Equals(0))
            return;

        var preferredOffers = _offers.Where(o => preferredOfferIds.Contains(o.Id));

        var promotedWarehouseMerchant =
            preferredWarehouseMerchantPolicy
                .DetermineMostPreferredWarehouseMerchant(context.EligibleWarehouseMerchants);

        var promotedOffer = preferredOffers.First(o =>
            o.MerchantId.Equals(promotedWarehouseMerchant.MerchantId) &&
            o.WarehouseId.Equals(promotedWarehouseMerchant.WarehouseId));

        PromotedOfferId = PromotedOfferId.Create(promotedOffer.Id);
    }


    public IEnumerable<MerchantId> GetOfferMerchants()
    {
        var offerMerchants = _offers.Select(o => o.MerchantId).Distinct();

        return offerMerchants;
    }

    public void AddOffer(NewOfferMutation values)
    {
        Guard.Against.Null(values);

        var existingOffer = _offers
            .FirstOrDefault(o => o.MerchantId.Equals(values.MerchantId) && o.WarehouseId.Equals(values.WarehouseId));

        if (existingOffer is not null)
            throw new ChildEntityAlreadyExistsException(nameof(Product), nameof(Offer), Id,
                existingOffer.Id.ToString());

        var offer = Offer.Create(
            Id,
            values.WarehouseId,
            values.MerchantId,
            values.GrossPrice,
            values.StockLevel
        );

        _offers.Add(offer);
    }

    public void UpdateOffer(OfferId offerId, EditOfferMutation values)
    {
        var offer = _offers.FirstOrDefault(o => o.Id.Equals(offerId));

        if (offer is null)
            throw new ChildEntityNotFoundException(nameof(Product), nameof(Offer), Id, offerId.ToString());

        offer.Update(values);
    }

    public void RemoveOffer(OfferId offerId)
    {
        var offer = _offers.FirstOrDefault(o => o.Id.Equals(offerId));

        if (offer is null)
            throw new ChildEntityNotFoundException(nameof(Product), nameof(Offer), Id, offerId.ToString());

        _offers.Remove(offer);
    }

    public void AddMedia(MediaId mediaId)
    {
        var existingMedia = _productMedias.FirstOrDefault(pm => pm.MediaId.Equals(mediaId));

        if (existingMedia is not null)
            throw new ChildEntityAlreadyExistsException(nameof(Product), nameof(ProductMedia), Id,
                existingMedia.MediaId.ToString());

        var productMedia = ProductMedia.Create(
            Id,
            mediaId
        );

        _productMedias.Add(productMedia);
    }
}
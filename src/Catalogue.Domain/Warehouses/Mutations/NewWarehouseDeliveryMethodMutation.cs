using Catalogue.Domain.Common.Enums;
using Catalogue.Domain.Common.ValueObjects;
using Catalogue.Domain.Lookups.DeliveryMethods.ValueObjects;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.Warehouses.Mutations;

public record NewWarehouseDeliveryMethodMutation(
    WarehouseId WarehouseId,
    DeliveryMethodId DeliveryMethodId,
    Country DeliveryCountryIsoCode,
    Money GrossCost,
    WarehouseDeliveryLeadTime LeadTimes,
    bool Active
);
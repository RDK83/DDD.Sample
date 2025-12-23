using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.Warehouses.ValueObjects;

namespace Catalogue.Domain.Warehouses.Mutations;

public record EditWarehouseMutation(WarehouseName WarehouseName, Address Address, bool Active);
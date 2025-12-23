using Catalogue.Domain.WarehouseMerchants.Policies;
using Catalogue.Domain.WarehouseMerchants.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.WarehouseMerchants.Policies;

public class WarehouseMerchantUniquePreferenceOrderPolicyTests
{
    [Test]
    public void ValidateForUniqueness_WithSamePreferenceOrder_ReturnsTrue()
    {
        var preferenceOrder = PreferenceOrder.Create(1);
        var isValid =
            new WarehouseMerchantUniquePreferenceOrderPolicy([preferenceOrder]).ValidateForUniqueness(preferenceOrder);

        Assert.That(isValid, Is.False);
    }

    [Test]
    public void ValidateForUniqueness_WithDifferentPreferenceOrder_ReturnsTrue()
    {
        var existingOrders = new[] { PreferenceOrder.Create(1), PreferenceOrder.Create(2) };
        var newPreferenceOrder = PreferenceOrder.Create(3);
        var isValid =
            new WarehouseMerchantUniquePreferenceOrderPolicy(existingOrders).ValidateForUniqueness(newPreferenceOrder);
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void ValidateForUniqueness_WithNullData_Throws()
    {
        Assert.Throws<DomainValidationException>(() => new WarehouseMerchantUniquePreferenceOrderPolicy(null!));
    }

    [Test]
    public void ValidateForUniqueness_WithNullNewPreference_Throws()
    {
        Assert.Throws<DomainValidationException>(() =>
            new WarehouseMerchantUniquePreferenceOrderPolicy([PreferenceOrder.Create(1)]).ValidateForUniqueness(null!));
    }
}
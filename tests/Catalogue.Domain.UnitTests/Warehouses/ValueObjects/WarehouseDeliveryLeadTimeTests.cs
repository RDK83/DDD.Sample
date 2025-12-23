using Catalogue.Domain.Warehouses.ValueObjects;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Warehouses.ValueObjects;

internal class WarehouseDeliveryLeadTimeTests
{
    private const int MaxLeadTime = 2;

    [Test]
    public void Create_WithValidInput_ShouldCreateSuccessfully()
    {
        var vo = WarehouseDeliveryLeadTime.Create(1, MaxLeadTime);

        Assert.Multiple(() =>
        {
            Assert.That(vo, Is.Not.Null);
            Assert.That(vo.MinimumLeadTime, Is.EqualTo(1));
            Assert.That(vo.MaximumLeadTime, Is.EqualTo(MaxLeadTime));
        });
    }

    [Test]
    public void Create_WhenMinLeadTimeIsZero_ShouldThrow()
    {
        Assert.Throws<DomainValidationException>(() => WarehouseDeliveryLeadTime.Create(0, MaxLeadTime));
    }

    [Test]
    public void Create_WhenMinLeadTimeExceedsMaxLeadTime_ShouldThrow()
    {
        Assert.Throws<DomainValidationException>(() => WarehouseDeliveryLeadTime.Create(3, MaxLeadTime));
    }
}
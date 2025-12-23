using Catalogue.Domain.Products.ValueObjects;
using Catalogue.Domain.UnitTests.Common.Abstract;
using Shared.Domain.Exceptions;

namespace Catalogue.Domain.UnitTests.Products.ValueObjects;

internal class StockLevelTests : NonNegativeIntegerValueObjectTests<StockLevel>
{
    protected override int ValidInput => 100;

    [Test]
    public void Create_WithStockLevelTooHigh_ShouldThrow()
    {
        Assert.Throws<DomainValidationException>(() => StockLevel.Create(int.MaxValue));
    }

    public override void ImplicitOperatorInteger_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        int value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}
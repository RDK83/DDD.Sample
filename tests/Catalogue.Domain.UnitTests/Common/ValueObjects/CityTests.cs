using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Common.ValueObjects;

internal class CityTests : NonNullableStringValueObjectTests<City>
{
    protected override string ValidInput => "Birmingham";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = City.Create(ValidInput);

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}
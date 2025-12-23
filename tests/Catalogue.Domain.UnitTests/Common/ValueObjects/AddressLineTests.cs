using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Common.ValueObjects;

internal class AddressLineTests : NullableStringValueObjectTests<AddressLine>
{
    protected override string ValidInput => "Orion Park";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string? value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}
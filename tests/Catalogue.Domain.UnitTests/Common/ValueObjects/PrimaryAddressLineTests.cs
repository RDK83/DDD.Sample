using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Common.ValueObjects;

internal class PrimaryAddressLineTests : NonNullableStringValueObjectTests<PrimaryAddressLine>
{
    protected override string ValidInput => "Unit 13";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}
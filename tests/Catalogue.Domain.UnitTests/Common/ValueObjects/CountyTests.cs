using Catalogue.Domain.Common.ValueObjects.Address;
using Catalogue.Domain.UnitTests.Common.Abstract;

namespace Catalogue.Domain.UnitTests.Common.ValueObjects;

internal class CountyTests : NullableStringValueObjectTests<County>
{
    protected override string ValidInput => "West Midlands";

    [Test]
    public override void ImplicitOperatorString_ReturnsValue()
    {
        var vo = CreateValueObjectWithValidInput();

        string? value = vo;

        Assert.That(value, Is.EqualTo(ValidOutput));
    }
}
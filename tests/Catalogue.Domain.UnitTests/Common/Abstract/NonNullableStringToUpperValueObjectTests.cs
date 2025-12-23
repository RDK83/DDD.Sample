using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class NonNullableStringToUpperValueObjectTests<T> : NonNullableStringValueObjectTests<T> where T : IStringValueObject<T>
{
    [Test]
    public void Create_LowerCaseInput_ShouldCapitaliseOutput()
    {
        var vo = T.Create(ValidInput);

        Assert.That(vo.Value, Is.EqualTo(ValidOutput));
    }
}
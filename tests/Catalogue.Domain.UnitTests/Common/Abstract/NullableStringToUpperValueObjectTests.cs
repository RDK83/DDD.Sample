using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Domain.UnitTests.Common.Abstract;

internal abstract class NullableStringToUpperValueObjectTests<T> : NullableStringValueObjectTests<T> where T : INullableStringValueObject<T>
{
    [Test]
    public void Create_LowerCaseInput_ShouldCapitaliseOutput()
    {
        var vo = T.Create(ValidInput);

        Assert.That(vo.Value, Is.EqualTo(ValidOutput));
    }
}
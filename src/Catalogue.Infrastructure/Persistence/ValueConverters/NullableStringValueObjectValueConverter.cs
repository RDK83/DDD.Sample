using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Domain.Aggregates.ValueObjects;

namespace Catalogue.Infrastructure.Persistence.ValueConverters;

internal class NullableStringValueObjectValueConverter<T> : ValueConverter<T, string?> where T : class, INullableStringValueObject<T>
{
    internal ValueComparer<T> ValueComparer { get; }

    internal NullableStringValueObjectValueConverter(Func<string?, T> factory)
        : base(
            value => value.Value,
            value => factory(value),
            convertsNulls: true)
    {
        ValueComparer = new ValueComparer<T>(
            (a, b) =>
                a == null && b == null ||
                a != null && b != null && a.Value == b.Value,
            v => (v.Value == null ? 0 : v.Value.GetHashCode()),
            v => factory(v.Value)
        );
    }
}
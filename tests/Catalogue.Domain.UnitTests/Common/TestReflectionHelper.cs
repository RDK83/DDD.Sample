using System.Reflection;

namespace Catalogue.Domain.UnitTests.Common;

public static class TestReflectionHelper
{
    public static void SetPrivateField<TObject, TValue>(TObject target, string fieldName, TValue value)
    {
        var field = typeof(TObject).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field == null)
            throw new InvalidOperationException($"Field '{fieldName}' not found on type '{typeof(TObject).Name}'");

        field.SetValue(target, value!);
    }

    public static void SetPrivateProperty<TObject, TValue>(TObject target, string propertyName, TValue value)
    {
        var property = typeof(TObject).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (property == null)
            throw new InvalidOperationException($"Property '{propertyName}' not found on type '{typeof(TObject).Name}'");

        var setMethod = property.GetSetMethod(true); // true = allow non-public setter
        if (setMethod == null)
            throw new InvalidOperationException($"Property '{propertyName}' does not have a setter.");

        setMethod.Invoke(target, new object[] {value!});
    }

    public static void SetAutoPropertyBackingField<TObject, TValue>(TObject target, string propertyName, TValue value)
    {
        var type = typeof(TObject);
        FieldInfo? field = null;
        string backingFieldName = $"<{propertyName}>k__BackingField";

        // Walk up the inheritance hierarchy to find the field
        while (type != null)
        {
            field = type.GetField(backingFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
                break;

            type = type.BaseType;
        }

        if (field == null)
        {
            throw new InvalidOperationException(
                $"Backing field for property '{propertyName}' not found on type or base types.");
        }

        field.SetValue(target, value!);
    }
}
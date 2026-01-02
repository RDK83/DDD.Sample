using Shared.Domain.Exceptions;

namespace Shared.Domain.Guard;

public static class DomainGuard
{
    //TODO improve this either by abstracting with interface/extensions like Ardalis or make domain concept explicit guards ie MerchantGuard, PriceGuard etc
    public static void AgainstNull(object? input, string fieldName)
    {
        if (input is null)
            throw new DomainValidationException("Input cannot be null.", fieldName);
    }

    public static T AgainstNull<T>(T? input)
    {
        if (input is null)
            throw new DomainValidationException("Input cannot be null.");

        return input;
    }

    public static IEnumerable<T> AgainstNull<T>(IEnumerable<T>? input)
    {
        if (input is null)
            throw new DomainValidationException("Input cannot be null.");

        return input;
    }

    public static void AgainstEmpty<T>(IEnumerable<T> input)
    {
        if (!input.Any())
            throw new DomainValidationException("Input cannot be empty");
    }

    public static void AgainstNullOrEmpty<T>(IEnumerable<T>? input)
    {
        AgainstNull(input);

        AgainstEmpty(input!);
    }

    public static void AgainstOutOfRange(int input, int minimum, int maximum)
    {
        if (input < minimum || input > maximum)
            throw new DomainValidationException($"Input out of range. Must be between {minimum} and {maximum}.");
    }

    public static void AgainstStringTooShort(string input, int minLength)
    {
        if (input.Length < minLength)
            throw new DomainValidationException(
                $"Input with length: {input.Length} is too short. Minimum length is {minLength}.");
    }

    public static void AgainstStringTooLong(string input, int maxLength)
    {
        if (input.Length > maxLength)
            throw new DomainValidationException(
                $"Input with length: {input.Length} is too long. Maximum length is {maxLength}.");
    }

    public static string AgainstLengthOutOfRange(string input, int minLength, int maxLength)
    {
        AgainstStringTooLong(input, maxLength);
        AgainstStringTooShort(input, minLength);

        return input;
    }

    public static void AgainstZero(int input)
    {
        if (input is 0)
            throw new DomainValidationException("Input cannot be zero.");
    }

    public static void AgainstNegative(int input)
    {
        if (input < 0)
            throw new DomainValidationException("Input cannot be negative.");
    }

    public static void AgainstNegative(decimal input)
    {
        if (input < 0)
            throw new DomainValidationException("Input cannot be negative.");
    }

    public static void AgainstNullOrWhiteSpace(string input)
    {
        var nullCheck = AgainstNull(input);

        if (nullCheck.IsWhiteSpace())
            throw new DomainValidationException("Input cannot be empty.");
    }
}
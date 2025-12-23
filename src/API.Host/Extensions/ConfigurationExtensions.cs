using SharedKernel.ConfigSections;

namespace API.Host.Extensions;

public static class ConfigurationExtensions
{
    internal static T GetSection<T>(this IConfiguration configuration) where T : IOptionsBase
    {
        var option = configuration.GetSection(T.SectionName).Get<T>();
        return option ?? throw new InvalidOperationException($"Configuration section '{T.SectionName}' is missing.");
    }
}
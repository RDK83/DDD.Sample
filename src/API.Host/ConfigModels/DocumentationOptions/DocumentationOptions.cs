using SharedKernel.ConfigSections;

namespace API.Host.ConfigModels.DocumentationOptions;

public record DocumentationOptions : IOptionsBase
{
    public static string SectionName => "Documentation";

    public bool Enabled { get; init; }
}
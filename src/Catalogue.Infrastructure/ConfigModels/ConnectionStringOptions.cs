using SharedKernel.ConfigSections;

namespace Catalogue.Infrastructure.ConfigModels;

public record ConnectionStringOptions : IOptionsBase
{
    public static string SectionName => "ConnectionStrings";
    public required string Application { get; init; }
}
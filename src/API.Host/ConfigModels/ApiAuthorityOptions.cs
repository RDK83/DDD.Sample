using SharedKernel.ConfigSections;

namespace API.Host.ConfigModels;

public record ApiAuthorityOptions : IOptionsBase
{
    public static string SectionName => "ApiAuthority";
    public bool Enabled { get; init; }
    public Uri? AuthorityUrl { get; init; }
}
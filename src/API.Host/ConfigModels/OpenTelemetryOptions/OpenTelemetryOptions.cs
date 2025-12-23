using SharedKernel.ConfigSections;

namespace API.Host.ConfigModels.OpenTelemetryOptions;

public record OpenTelemetryOptions : IOptionsBase
{
    public static string SectionName => "OpenTelemetry";
    public OpenTelemetryExporterOptions Exporter { get; init; } = new();
}
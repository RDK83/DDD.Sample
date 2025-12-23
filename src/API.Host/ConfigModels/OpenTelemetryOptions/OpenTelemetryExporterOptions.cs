namespace API.Host.ConfigModels.OpenTelemetryOptions;

public record OpenTelemetryExporterOptions
{
    public Uri? Endpoint { get; init; }
    public string? Headers { get; init; }
}
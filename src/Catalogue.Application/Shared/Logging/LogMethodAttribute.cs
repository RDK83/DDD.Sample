using System.Reflection;
using System.Text.Json;
using MethodDecorator.Fody.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalogue.Application.Shared.Logging;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class LogMethodAttribute : Attribute, IMethodDecorator
{
    private static ILoggerFactory? _loggerFactory;

    private ILogger? _logger;
    private object[]? _args;
    private MethodBase? _methodBase;

    public static void Configure(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public void Init(object instance, MethodBase method, object[] args)
    {
        _args = args;
        _methodBase = method;


        if (method.IsConstructor)
        {
            // Skip logging for constructors
            _logger = null;
            return;
        }

        // Create logger from factory if not found on instance
        _logger = _loggerFactory?.CreateLogger(instance.GetType())
                  ?? throw new InvalidOperationException(
                      "ILoggerFactory is not configured. Call LogMethodAttribute.Configure() at startup.");
    }

    public void OnEntry()
    {
        _logger?.LogInformation("Entering {Method}", GetFullMethodName());

        if (_args is null || _args?.Length == 0 || !HasLogParametersAttribute())
            return;

        // Get the method parameters
        var paramInfos = (_methodBase as MethodInfo)?.GetParameters();

        var paramLog = _args!.Select((arg, index) =>
        {
            var type = arg.GetType();

            // Treat primitives, strings, decimals as simple types
            if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
            {
                var paramName = paramInfos != null && index < paramInfos.Length
                    ? paramInfos[index].Name ?? $"arg{index}"
                    : $"arg{index}";
                return new {Name = paramName, Value = arg};
            }

            // Otherwise treat as complex object, use type name as "name"
            return new {type.Name, Value = arg};
        }).ToArray();

        _logger?.LogInformation(
            "Parameters: {Params}",
            JsonSerializer.Serialize(paramLog, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            }));
    }

    public void OnExit()
    {
        _logger?.LogInformation("Exiting {Method}", GetFullMethodName());
    }

    public void OnException(Exception exception)
    {
        // NO handling — exceptions bubble up to global handlers
    }

    private bool HasLogParametersAttribute()
    {
        if (_methodBase is null)
            return false;

        // If method explicitly opts out, return false
        if (_methodBase.GetCustomAttributes(typeof(NoLogParametersAttribute), true).Any())
            return false;

        // Check method-level attribute
        if (_methodBase.GetCustomAttributes(typeof(LogParametersAttribute), true).Any())
            return true;

        // Check class-level attribute
        if (_methodBase.DeclaringType?.GetCustomAttributes(typeof(LogParametersAttribute), true).Any() == true)
            return true;

        return false;
    }

    private string GetFullMethodName()
    {
        if (_methodBase is null)
            return "UnknownMethod";

        var className = _methodBase.DeclaringType?.Name ?? _methodBase.ReflectedType?.Name ?? "UnknownClass";
        return $"{className}.{_methodBase.Name}";
    }
}
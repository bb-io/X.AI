namespace Apps.XAI.Utils;

public sealed record ModelLimits(int ContextTokens, int MaxOutputTokens);

public static class ModelTokenService
{
    private static readonly ModelLimits Unknown = new(131_072, 8_192);

    private static readonly (string Prefix, ModelLimits Limits)[] Models =
    [
        ("grok-4.20", new(1_000_000, 32_768)),
        ("grok-4.3", new(1_000_000, 32_768)),
        ("grok-4.5", new(500_000, 32_768)),
        ("grok-4.6", new(500_000, 32_768)),
        ("grok-4.7", new(500_000, 32_768)),
        ("grok-build", new(256_000, 32_768)),
        ("grok-code", new(256_000, 32_768)),
        ("grok-4", new(256_000, 32_768)),
        ("grok-3", new(131_072, 16_384))
    ];

    public static ModelLimits For(string? model) =>
        Models.FirstOrDefault(entry => model?.StartsWith(entry.Prefix, StringComparison.OrdinalIgnoreCase) == true).Limits
        ?? Unknown;
}

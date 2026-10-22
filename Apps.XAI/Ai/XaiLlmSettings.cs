namespace Apps.XAI.Ai;

public sealed record XaiLlmSettings(double? Temperature = null, double? TopP = null, int? MaxTokens = null, string? ReasoningEffort = null);

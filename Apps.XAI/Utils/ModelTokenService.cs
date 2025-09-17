namespace Apps.XAI.Utils;
public static class ModelTokenService
{
    public static int GetMaxTokensForModel(string? model)
    {
        return model switch
        {
            "grok-2-1212" => 131072,
            "grok-2-vision-1212" => 32768,
            "grok-3" => 131072,
            "grok-3-mini" => 131072,
            string m when m.Contains("grok-4") => 256000,
            "grok-code-fast-1" => 256000,
            _ => 8000
        };
    }
}

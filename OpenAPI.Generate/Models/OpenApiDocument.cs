using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class OpenApiDocument
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("openapi"), UsedImplicitly]
    public required string OpenApi { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("info"), UsedImplicitly]
    public required Info Info { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("paths"), UsedImplicitly]
    public required Dictionary<string, Dictionary<string, Path>> Paths { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("components"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public Components? Components { get; set; }
}
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Content
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("schema"), UsedImplicitly]
    public required Schema Schema { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("encoding"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required Dictionary<string, Encoding>? Encoding { get; init; }
}
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Response
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("description"), UsedImplicitly]
    public required string Description { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("content"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required Dictionary<string, Content>? Content { get; init; }
}
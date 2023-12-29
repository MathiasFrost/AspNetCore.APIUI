using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class RequestBody
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("content"), UsedImplicitly]
    public required Dictionary<string, Content> Content { get; init; }
}
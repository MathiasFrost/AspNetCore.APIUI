using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Encoding
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("style"), UsedImplicitly]
    public required string Style { get; init; }
}
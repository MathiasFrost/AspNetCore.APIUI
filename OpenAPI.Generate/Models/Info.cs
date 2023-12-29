using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Info
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("title"), UsedImplicitly]
    public required string Title { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("version"), UsedImplicitly]
    public required string Version { get; init; }
}
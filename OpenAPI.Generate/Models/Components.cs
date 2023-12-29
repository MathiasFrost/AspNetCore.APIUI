using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Components
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("schemas"), UsedImplicitly]
    public required Dictionary<string, Schema> Schemas { get; init; }
}
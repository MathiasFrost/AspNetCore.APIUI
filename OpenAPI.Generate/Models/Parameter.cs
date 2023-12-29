using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Parameter
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("name"), UsedImplicitly]
    public required string Name { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("in"), UsedImplicitly]
    public required string In { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("style"), UsedImplicitly]
    public required string Style { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("required"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), UsedImplicitly]
    public required bool Required { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("schema"), UsedImplicitly]
    public required Schema Schema { get; init; }
}
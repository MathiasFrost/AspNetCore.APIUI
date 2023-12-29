using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Property
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required string? Type { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("format"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required string? Format { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("nullable"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), UsedImplicitly]
    public required bool Nullable { get; init; }
}
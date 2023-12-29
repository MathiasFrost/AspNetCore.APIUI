using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace OpenAPI.Generate.Models;

/// <summary> TODOC </summary>
public sealed class Path
{
    /// <summary> TODOC </summary>
    [JsonPropertyName("tags"), UsedImplicitly]
    public required List<string> Tags { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("parameters"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required Parameter[]? Parameters { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("requestBody"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required RequestBody? RequestBody { get; init; }

    /// <summary> TODOC </summary>
    [JsonPropertyName("responses"), UsedImplicitly]
    public required Dictionary<string, Response> Responses { get; init; }
}
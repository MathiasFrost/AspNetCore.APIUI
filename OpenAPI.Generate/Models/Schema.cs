using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using OpenAPI.Generate.Models;

namespace OpenAPI.Generate;

public sealed class Schema
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required string? Type { get; init; }

    [JsonPropertyName("format"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required string? Format { get; init; }

    [JsonPropertyName("$ref"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required string? Ref { get; init; }

    [JsonPropertyName("properties"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required Dictionary<string, Property>? Properties { get; init; }

    [JsonPropertyName("additionalProperties"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required JsonNode? AdditionalProperties { get; init; }

    [JsonPropertyName("items"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UsedImplicitly]
    public required Schema? Items { get; init; }
}
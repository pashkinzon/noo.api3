using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.Core.Utils.Richtext.Delta;

public class DeltaOp
{
    [JsonIgnore]
    public bool HasInsert => Insert != null;

    [JsonIgnore]
    public bool HasDelete => Delete.HasValue;

    [JsonIgnore]
    public bool HasRetain => Retain.HasValue;

    [JsonPropertyName("insert")]
    [JsonConverter(typeof(DeltaInsertConverter))]
    public object? Insert { get; set; }

    [JsonPropertyName("delete")]
    [Range(1, int.MaxValue)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? Delete { get; set; }

    [JsonPropertyName("retain")]
    [Range(1, int.MaxValue)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? Retain { get; set; }

    [JsonPropertyName("attributes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DeltaAttributes? Attributes { get; set; }

    public static DeltaOp Empty()
    {
        return new DeltaOp()
        {
            Insert = "\n",
        };
    }
}

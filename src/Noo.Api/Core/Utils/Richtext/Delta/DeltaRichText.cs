using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Noo.Api.Core.Utils.Richtext.Delta;

public class DeltaRichText : IRichTextType
{
    [JsonIgnore]
    public const string TypeDiscriminator = "delta";

    //[Required]
    //[JsonPropertyName("$type")]
    //public string Type { get; } = TypeDiscriminator;

    [Required]
    [MinLength(1)]
    [JsonPropertyName("ops")]
    public List<DeltaOp> Ops { get; set; } = [];

    /// <summary>
    /// Default constructor for DeltaRichText.
    /// Initializes the Ops property with an empty DeltaOp.
    /// </summary>
    public DeltaRichText()
    {
        Ops = [DeltaOp.Empty()];
    }

    /// <summary>
    /// Constructor for DeltaRichText that takes a JSON string and deserializes it into the Ops property.
    /// </summary>
    public DeltaRichText(string? delta)
    {
        Deserialize(delta);
    }

    /// <summary>
    /// Creates a DeltaRichText object from a string.
    /// </summary>
    public static DeltaRichText FromString(string? delta)
    {
        return new DeltaRichText()
        {
            Ops = [new DeltaOp() {
                Insert = delta ?? string.Empty + "\n"
            }]
        };
    }

    public bool IsEmpty()
    {
        return Length() == 0;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public int Length()
    {
        return Ops.Sum(op => op.HasInsert ? op.Insert?.ToString()?.Length ?? 0 : 0);
    }

    private void Deserialize(string? delta)
    {
        if (delta == null)
        {
            Ops = [];
            return;
        }

        var deserialized = JsonSerializer.Deserialize<DeltaRichText>(delta);

        if (deserialized == null)
        {
            Ops = [];
            return;
        }

        Ops = deserialized.Ops;
    }
}

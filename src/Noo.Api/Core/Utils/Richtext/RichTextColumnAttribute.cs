using System.ComponentModel.DataAnnotations.Schema;

namespace Noo.Api.Core.Utils.Richtext;

[AttributeUsage(AttributeTargets.Property)]
public class RichTextColumnAttribute : ColumnAttribute
{
    public string Charset { get; set; } = "utf8mb4";

    public string Collation { get; set; } = "utf8mb4_unicode_ci";

    public RichTextColumnAttribute(string columnName) : base(columnName)
    {
        TypeName = "JSON";
    }
}

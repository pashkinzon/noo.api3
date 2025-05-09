using System.ComponentModel.DataAnnotations.Schema;

namespace Noo.Api.Core.DataAbstraction.Model.Attributes;

public class ModelAttribute : TableAttribute
{
    public ModelAttribute(string name) : base(name) { }
}

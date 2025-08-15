namespace Noo.Api.Core.DataAbstraction.Model;

public interface ISoftDeletableModel
{
    public bool IsDeleted { get; set; }
}

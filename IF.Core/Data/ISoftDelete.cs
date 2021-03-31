
namespace IF.Core.Data
{
    public interface ISoftDelete:IEntity
    {
        bool SoftDeleted  { get; set; }
    }
}

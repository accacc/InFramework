
namespace IF.Core.Data
{
    public interface IActiveableEntity:IEntity
    {
        bool Active { get; set; }
    }
}

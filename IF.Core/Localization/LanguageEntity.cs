using IF.Core.Data;
using System;


namespace IF.Core.Localization
{
    public interface  ILanguageableEntity:IEntity
    {
        
        //public virtual string Name { get; set; }
    }

    public interface ILanguageEntity: IEntity
    {
         int ObjectId { get; set; }
         int LanguageId { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.Class , AllowMultiple = true)]
    public class LanguageEntityAttribute : Attribute
    {
        public Type Type { get; set; }
        public LanguageEntityAttribute(Type type)
        {
            this.Type = type;
        }
    }

    //public abstract class LanguageEntity<D> : LanguageEntity where D : LanguageableEntity
    //{
    //    //[ForeignKey("ObjectId")]
    //    //public virtual D Object { get; set; }
        
    //}

    //public abstract class LanguageableEntity<L> : LanguageableEntity where L : LanguageEntity
    //{
    //    //public ICollection<L> Languages { get; set; }
    //}
}

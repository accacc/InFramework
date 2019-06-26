using System.Collections.Generic;

namespace Derin.Core.Localization
{

    public interface ILanguageProperty 
    {
    }

    public interface ILanguage : ILanguageProperty
    {
        int LanguageId { get; set; }
        int ObjectId { get; set; }  
    }

    public interface ILanguageProperty<L> : ILanguageProperty where L : ILanguage
    {
        //ICollection<L> Languages { get; set; }
    }

    public interface ILanguage<D> : ILanguage where D : ILanguageProperty
    {
        //D Object { get; set; }
    }
}

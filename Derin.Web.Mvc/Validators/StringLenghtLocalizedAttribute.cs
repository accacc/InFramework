using Derin.Web.Mvc.Resources;
using System.ComponentModel.DataAnnotations;

namespace Derin.Web.Core.Mvc.Validators
{

    public class StringLenghtLocalizedAttribute : StringLengthAttribute
    {
        public StringLenghtLocalizedAttribute(int maximumLength)
            : base(maximumLength)
        {
            this.ErrorMessageResourceType = typeof(MVCR);
            this.ErrorMessageResourceName = "StringLenghtFlield";
        }
    }
}

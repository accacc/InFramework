using Derin.Web.Mvc.Resources;
using System.ComponentModel.DataAnnotations;

namespace Derin.Web.Mvc.Validators
{
    public class RequiredLocalizedAttribute : RequiredAttribute
    {
        public RequiredLocalizedAttribute()
        {
            this.ErrorMessageResourceType = typeof(MVCR);
            this.ErrorMessageResourceName = "RequiredField";
        }
    }


}

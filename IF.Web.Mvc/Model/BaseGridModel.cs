using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Mvc.Model
{
    public abstract class BaseDataModel
    {
        public int Id { get; set; }
    }

    public class BaseFormModel : BaseDataModel
    {

    }

    public class BaseGridModel : BaseDataModel
    {
        
    }
}
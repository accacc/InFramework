using IF.CodeGeneration.Application;
using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.Application.Generator.List;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace IF.Tools.CodeGenerator
{
    public partial class MvcAddGeneratorForm : ApiGeneratorBaseForm
    {

        public MvcAddGeneratorForm(CSInsertGenerator generator) : base(generator)
        {

        }
    }
}

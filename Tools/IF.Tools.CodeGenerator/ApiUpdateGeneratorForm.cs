using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.Application.Generator.Update;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace IF.Tools.CodeGenerator
{
    public partial class ApiUpdateGeneratorForm : ApiGeneratorBaseForm
    {

        public ApiUpdateGeneratorForm(ApiCsUpdateGenerator generator):base(generator)
        {
            
        }

    }
}

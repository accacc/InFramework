using System.Collections.Generic;

namespace IF.CodeGeneration.Core
{
    public class StoredProcedure
    {

        public StoredProcedure()
        {
            this.Parameters = new List<SpParameter>();
        }
        public List<SpParameter> Parameters { get; set; }

        public List<SpColumn> Columns { get; set; }

        public string Name { get; set; }
    }
}

using IF.Core.Control;
using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Navigation
{
    public class NavigationDto: TreeDto<NavigationDto>
    {

        public string Name { get; set; }
        public int PageControlId { get; set; }

        public PageControlType ControlType { get; set; }       

        public string Description { get; set; }

        public string ClientId { get; set; }
    }
}

using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Contract.Commands
{
    public class UserAddCommand : BaseCommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}

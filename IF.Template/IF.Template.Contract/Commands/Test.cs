using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Template.Contract.Commands
{
    public class TestAddCommand : BaseCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public interface ITestAddDbCommandAsync : IDataInsertCommandAsync<TestAddCommand>
    {
    }
}

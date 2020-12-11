using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.RuleEngine
{
    public interface IIFRuleEngine<T> where T : IIFRuleContext
    {
       void Execute();
    }
}

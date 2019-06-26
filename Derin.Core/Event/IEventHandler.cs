using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Event
{
    public interface IDerinEventHandler<TDerinEvent> : IDerinEventHandler
        where TDerinEvent : DerinEvent
    {
        void Handle(TDerinEvent @event);
    }

    public interface IDerinEventHandler
    {
    }
}

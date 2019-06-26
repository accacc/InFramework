using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Validation
{
    public interface IValidator
    {
        /// <summary>Validates the given instance.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when the instance is a null reference.</exception>
        /// <exception cref="ValidationException">Thrown when the instance is invalid.</exception>
        void ValidateObject(object instance);
    }
}

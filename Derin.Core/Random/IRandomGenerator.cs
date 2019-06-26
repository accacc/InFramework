
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core
{
    public interface IRandomGenerator
    {
        string CreateCapcthaString();
        string CreateRandomString(int length);

        string GeneratePassword(int length);

        string GeneratePassword();

        string GeneratePassword(int minLength, int maxLength);
    }
}

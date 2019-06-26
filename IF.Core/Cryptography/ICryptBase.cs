using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.Core.Cryptography
{
    public interface ICryptBase
    {
        String Encrypt(String value);
        String Decrypt(String value);
    }
}

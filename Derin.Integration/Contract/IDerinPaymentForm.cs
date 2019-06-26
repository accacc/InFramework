using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Integration.Contract
{
    public interface IDerinPaymentForm
    {
        string Name { get; set; }


        string CardNumber { get; set; }

        string Cvv { get; set; }

        string ExpireYear { get; set; }
        string ExpireMonth { get; set; }

        bool MesafeliSozlesmeKabulu { get; set; }

        bool OnBilgilendirmeKabul { get; set; }

        int Banka { get; set; }
        int? Taksit { get; set; }
        string KartTipi { get; set; }
    }
}

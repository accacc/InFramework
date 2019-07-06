using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Notification.OneSignal
{
    public enum TuratelResponseCodes
    {
        Basarili = -1,
        Sistem_Hatasi = 00,
        Tanimsiz_Hata = 20,
        Hatali_XML_Formati = 21,
        Kullanici_Aktif_Degil = 22,
        Kullanici_Zaman_Asiminda = 23
    }
}

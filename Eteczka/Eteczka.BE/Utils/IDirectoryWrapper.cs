using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Utils
{
    public interface IDirectoryWrapper
    {
        string UtworzKatalog(string sciezka);
        List<string> PobierzPlikiZKatalogu(string sciezka);
        bool UsunKatalog(string sciezka, bool CzyUsunacZawartosc);
        bool CzyKatalogIstnieje(string sciezka);
        bool CzyPlikIstnieje(string sciezka);
        string WczytajPlik(string sciezka, string rozszerzenie = "");
        bool CzyPlikJestWUzyciu(string nazwaPliku);
        int ZliczPlikiWKatalogu(string katalog);
    }
}

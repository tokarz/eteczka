using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Utils
{
    public class ExcellUtils : IExcellUtils
    {
        private IDirectoryWrapper _Wrapper;

        public ExcellUtils (IDirectoryWrapper wrapper)
        {
            this._Wrapper = wrapper;
        }

        public string WygenerujSciezkeZapisuExcell(string nazwaPliku, string user)
        {

            string eadRoot = ConfigurationManager.AppSettings["rootdir"];
            string raportyExcellFolder = Path.Combine(eadRoot, "RaportyExcell\\");
            string sciezkaZapisu = raportyExcellFolder + nazwaPliku + " - wygenerowano przez " + user + ".xlsx";
              
                if (!_Wrapper.CzyKatalogIstnieje(raportyExcellFolder))
                {
                    _Wrapper.UtworzKatalog(raportyExcellFolder);
                }

                if (!_Wrapper.CzyPlikJestWUzyciu(sciezkaZapisu))
                {
                    sciezkaZapisu = raportyExcellFolder + nazwaPliku + " - wygenerowano przez " + user + ".xlsx";
                }

                else
                {
                    int licznik = _Wrapper.ZliczPlikiWKatalogu(raportyExcellFolder);
                    sciezkaZapisu = raportyExcellFolder + nazwaPliku + " - wygenerowano przez " + user + "(" + licznik + ")" + ".xlsx";
                }

            return sciezkaZapisu;
        }
    }
}

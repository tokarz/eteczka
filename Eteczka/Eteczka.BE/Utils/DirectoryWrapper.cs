using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Utils
{
    public class DirectoryWrapper : IDirectoryWrapper
    {
        public string UtworzKatalog(string sciezka)
        {
            return Directory.CreateDirectory(sciezka).ToString();
        }

        public List<string> PobierzPlikiZKatalogu(string sciezka)
        {
            return Directory.GetFiles(sciezka).ToList();
        }

        public bool UsunKatalog(string sciezka, bool CzyUsunacZawartosc = true)
        {
            bool result = true;
            Directory.Delete(sciezka, true);
            return result;
        }
        public bool CzyKatalogIstnieje(string sciezka)
        {
            return Directory.Exists(sciezka);
        }

        public bool CzyPlikIstnieje(string sciezka)
        {
            return File.Exists(sciezka);
        }


    }
}

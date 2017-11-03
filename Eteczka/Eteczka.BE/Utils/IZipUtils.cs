using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Utils
{
    public interface IZipUtils
    {
        void SpakujPlikiZHaslem(List<string> listaPlikow, string haslo, string tempZipSaveSciezka, string sciezkaDoZipa);
    }
}

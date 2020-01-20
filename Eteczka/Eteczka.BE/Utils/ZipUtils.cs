using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Utils
{
    public class ZipUtils : IZipUtils
    {

        public void SpakujPlikiZHaslem(List<string> listaPlikow, string haslo, string tempZipSaveSciezka, string sciezkaDoZipa)
        {
            using (ZipFile zip = new ZipFile())
            {
                string password = haslo;
                if (!string.IsNullOrEmpty(password))
                {
                    foreach (string plik in listaPlikow)
                    {
                        if (File.Exists(plik))
                        {
                        zip.AddFile(plik, "");
                        }
                    }

                    zip.Save(tempZipSaveSciezka);
                }
                zip.Password = password;
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;

                zip.AddFile(tempZipSaveSciezka, "");
                zip.RemoveSelectedEntries("*.pdf");

                zip.Save(sciezkaDoZipa);
            }
        }
    }
}

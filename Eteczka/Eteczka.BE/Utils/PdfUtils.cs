using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Utils
{
    public class PdfUtils : IPdfUtils
    {
        private IDirectoryWrapper _Wrapper;

        public PdfUtils(IDirectoryWrapper wrapper)
        {
            this._Wrapper = wrapper;
        }

        public bool SavePdf(List<string> plikiDoSpakowania, string tempZrodloKatalog)
        {
            bool result = false;
            PdfDocument document = new PdfDocument();

            foreach (string plikZaszyfrowany in plikiDoSpakowania)
            {
                if (_Wrapper.CzyPlikIstnieje(plikZaszyfrowany))
                //if (File.Exists(plikZaszyfrowany))
                {
                    string nazwaPliku = plikZaszyfrowany.Substring(plikZaszyfrowany.LastIndexOf("\\"));
                    document = PdfReader.Open(plikZaszyfrowany, "adminadmin");
                    document.Save(tempZrodloKatalog + "\\" + nazwaPliku);
                    result = true;
                }
            }

            return result;
        }
    }
}

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
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
                    document = PdfReader.Open(plikZaszyfrowany, "#Blk8dr#Blk8dr#");
                    document.Save(tempZrodloKatalog + "\\" + nazwaPliku);
                    result = true;
                }
            }

            return result;
        }
        public bool GenerujIZapiszRaportPdf(Document doc, string nazwaRaportu, string user)
        {
            bool result = false;

            try
            {
                //Generujemy PDF:
                PdfDocumentRenderer docRend = new PdfDocumentRenderer(true);
                docRend.Document = doc;
                docRend.RenderDocument();
                //Zapis pliku
                string eadRoot = ConfigurationManager.AppSettings["rootdir"];
                string raportyPdfFolder = Path.Combine(eadRoot, "RaportyPdf\\");
                if (!_Wrapper.CzyKatalogIstnieje(raportyPdfFolder))
                {
                    _Wrapper.UtworzKatalog(raportyPdfFolder);
                }

                string nazwaPliku = raportyPdfFolder  + nazwaRaportu + " - wygenerowano przez " + user + ".pdf";

                if (!_Wrapper.CzyPlikJestWUzyciu(nazwaPliku))
                {
                    docRend.PdfDocument.Save(nazwaPliku);
                }

                else
                {
                    int licznik = _Wrapper.ZliczPlikiWKatalogu(raportyPdfFolder);
                    //nazwaPliku = raportyPdfFolder + nazwaRaportu + licznik + ".pdf";
                    nazwaPliku = raportyPdfFolder  + nazwaRaportu + " - wygenerowano przez " + user + "(" + licznik + ")" + ".pdf";
                    docRend.PdfDocument.Save(nazwaPliku);
                }

                //Uruchamianie podglądu pliku
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = nazwaPliku;
                Process.Start(processInfo);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        


    }
}

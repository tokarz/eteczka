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

        public string WczytajPlik(string sciezka, string rozszerzenie = "")
        {
            StringBuilder plik = new StringBuilder();

            if (string.IsNullOrEmpty(rozszerzenie) || sciezka.EndsWith(rozszerzenie))
            {
                // PROBUJEMY OTWORZYC PLIK I LAPIEMY EWENTUALNE WYJATKI
                try
                {   // OTWIERAMY STRUMIEN DO PLIKU
                    using (StreamReader sr = new StreamReader(sciezka))
                    {
                        // WCZYTUJEMY 1 LINIJKE Z PLIKU DO NAPOTKANIA KONCA LINII 
                        string linijka = sr.ReadToEnd();
                        plik.Append(linijka);
                    }
                }
                catch (Exception e)
                {
                    // OBSLUGA WYJATKU
                    Console.WriteLine("BLAD ODCZYTU PLIKU!");
                    Console.WriteLine(e.Message);
                }
            }


            return plik.ToString();
        }

        public bool CzyPlikJestWUzyciu(string nazwaPliku)
        {
            bool zablokowany = false;
            try
            {
                FileStream fs = File.Open(nazwaPliku, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException ex)
            {
                zablokowany = true;
            }
            return zablokowany;
        }

        public int ZliczPlikiWKatalogu(string katalog)
        {
            return Directory.GetFiles(katalog).ToList<string>().Count;
        }
    }
}

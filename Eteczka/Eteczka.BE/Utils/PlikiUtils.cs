using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace Eteczka.BE.Utils
{
    public class PlikiUtils
    {
        //Przykladowe dzialanie metody:
        //Uruchomiona z argumentem c:/katalog/plik.txt
        //Powinna zwrocic plik.txt
        //Jesli sciezka nie ma na koncu zadnego pliku (np c:\costam) powinna zwrocic pusty string ""

        //Przydatne podpowiedzi:
        //Metody : string.split, string lastIndexOf
        //Testy sa juz napisane i swieca sie na czerwono!:)
        public string WezNazwePlikuZeSciezki(string sciezka)
        {
            int lastSlash = sciezka.LastIndexOf("/");
            if (lastSlash == -1)
            {
                lastSlash = sciezka.LastIndexOf("\\");
            }

            int ostatniSlash = (lastSlash + 1);


            string nazwaPliku = (sciezka.Substring(ostatniSlash));

            int bezPliku = nazwaPliku.LastIndexOf(".");
            if (bezPliku == -1)
            {
                nazwaPliku = "";
            }


            return nazwaPliku;
        }

        public List<string> WezNazwePlikowZeSciezek(List<string> sciezki)
        {
            //Tu stworzyles Liste na wyniki ale dales jej ta sama nazwe co parametr funkcji - "sciezki"
            //Od razu wiec przesloniles sobie Liste ktora dostales
            //List<string> sciezki = new List<string>();
            List<string> znalezioneNazwyPlikow = new List<string>();

            foreach (string sciezka in sciezki)
            {
                int ostatniSlash = sciezka.LastIndexOf("/") + 1;
                if (ostatniSlash == 0)
                {
                    ostatniSlash = sciezka.LastIndexOf("\\") + 1;
                }
                string nazwaPliku = sciezka.Substring(ostatniSlash);
                int bezPliku = nazwaPliku.LastIndexOf(".");
                //Do tego miejsca algorytm jest dobry, robi to co ma, czyli wyciaga nazwe pliku ze sciezki
                if (bezPliku != -1)
                {
                    //tu wchodzimy tylko wtedy gdy plik ma w nazwie kropke.
                    //Ale w tym przypadku nie chcemy zwrocic 1 znajezionej nazwy pliku piszac:
                    //return nazwaPliku
                    //ale tym razem chcemy dodac znaleziony plik do Listy wynikow a nie zwrocic tylko jeden:)
                    //Metoda Add dodaje nazwe pliku do listy
                    znalezioneNazwyPlikow.Add(nazwaPliku);
                }
            }

            //dopiero kiedy petla sie skonczy, chcemy zwrocic wyniki
            return znalezioneNazwyPlikow;
        }

        //Metoda dostaje argument : liste plikow, i zwraca tylko te, ktore maja podane rozszerzenie (zip, txt, i tak dalej)
        public List<string> WezPlikiZRozszerzeniem(List<string> sciezki, string rozszerzenie)
        {
            List<string> PlikiZRozszerzeniem = new List<string>();
            foreach (string sciezka in sciezki)
            {
                int ostatniSlash = sciezka.LastIndexOf("/");
                if (ostatniSlash == -1)
                {
                    ostatniSlash = sciezka.LastIndexOf("\\");
                }
                string nazwaPlikuZRozszerzeniem = sciezka.Substring(ostatniSlash + 1);
                //int kropka = nazwaPlikuZRozszerzeniem.LastIndexOf(".");
                int kropka = sciezka.LastIndexOf(".");
                string rozszerzeniePliku = sciezka.Substring(kropka + 1).ToLower();
                string rozszerzenieZParametru = rozszerzenie.Trim().ToLower();  //to podpowiedź Paszcza :))
                if (kropka != -1)  //Tutaj zabezpieczam się przed zwróceniem ścieżki np. D:/dane/pdf
                {
                    //if (rozszerzenieZParametru. == rozszerzeniePliku)
                    if (rozszerzenieZParametru.Equals(rozszerzeniePliku))  //Czy zamiast "==" mogę użyć metody Equals?
                    {
                        PlikiZRozszerzeniem.Add(nazwaPlikuZRozszerzeniem);
                    }
                }

            }
            return PlikiZRozszerzeniem;
        }


        //Ta Metoda ma dostac 2 listy pelnych sciezek A (d:\a\b.txt) i B(c:/costam) i zwrocic liste
        // Tych PLIKOW (samych ich nazw) ktore powtarzaja sie w jednej i drugiej liscie

        public List<string> WezSpolneElementy(List<string> plikiA, List<string> plikiB)
        {
            List<string> WspolneNazwyPlikow = new List<string>();

            List<string> NazwyPlikowA = WezNazwePlikowZeSciezek(plikiA);
            List<string> NazwyPlikowB = WezNazwePlikowZeSciezek(plikiB);
            var porownaniePlikow = NazwyPlikowA.Intersect(NazwyPlikowB);
            // Tę zmienna Visual pozwolił zadeklarować tylko jako var - czy dlatego, żeby metoda była uniwersalna dla różnych typów danych?
            foreach (var pliki in porownaniePlikow)
            {
                WspolneNazwyPlikow.Add(pliki.ToString());
            }

            return WspolneNazwyPlikow;
        }

        public Dictionary<string, int> PoliczRozszerzenia(List<string> pliki)
        {
            //Przygotowujemy sobie pusty slownik.
            Dictionary<string, int> rozszerzeniaPlikow = new Dictionary<string, int>();

            //Zaczynamy analizowac liste wejsciowa sciezka po sciezce
            foreach (string sciezkaPliku in pliki)
            {
                //Korzystajac z napisanej przez Ciebie metory wyciagamy sama nazwe pliku ze sciezki
                string nazwaPliku = WezNazwePlikuZeSciezki(sciezkaPliku);

                //Teraz wyciagamy nazwe rozszerzenia z pliku (txt, zip, rar, itd)
                string aktualneRozszerzenie = nazwaPliku.Substring(nazwaPliku.LastIndexOf(".") + 1);

                //Sprawdzamy, czy aktualnie sprawdzane rozszerzenie bylo juz kiedys znalezione

                if (rozszerzeniaPlikow.ContainsKey(aktualneRozszerzenie))
                {
                    //Jesli rozszerzenie bylo juz przez nas wczesniej analizowane, musimy wyciagnac poprzednia wartosc licznika
                    //i zwiekszyc go o 1. Potem nadpisac stara wartosc i umiescic w slowniku aktualna:
                    ////  rozszerzeniaPlikow["txt"] = 2
                    int iloscPlikow = rozszerzeniaPlikow[aktualneRozszerzenie];
                    iloscPlikow = iloscPlikow + 1;
                    rozszerzeniaPlikow[aktualneRozszerzenie] = iloscPlikow;
                }
                else
                {
                    //Jelsi nie - wrzucamy do slownika pod haslem rozszerzenie - wartosc startowa - 1
                    //Czyli teraz wyglada to tak :
                    //  rozszerzeniaPlikow["txt"] = 1

                    rozszerzeniaPlikow.Add(aktualneRozszerzenie, 1);
                }

            }

            //Co do zapisu : rozszerzeniaPlikow[aktualneRozszerzenie];
            //Moze to byc mylace na poczatku. Klamry [] odnosza sie to tablicy. Jezeli masz typ List
            //To element lista[0], lista[1] odnosza sie do kolejnych indeksow elementow
            //Jezeli masz typ Slownika (Dictionary) to nie masz indeksow, tylko klucze
            //np stolice("Polska") = "Warszawa";
            //np stolice("Wlochy") = "Rzym";
            //I piszac rozszerzeniaPlikow[aktualneRozszerzenie] wyciagamy wartosc dla klucza, ktorym jest rozszerzenie

            return rozszerzeniaPlikow;
        }

        public Dictionary<string, List<string>> PoliczPlikiWKatalogach(List<string> sciezkiPlikow)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();



            foreach (string sciezka in sciezkiPlikow)
            {
                string nazwaPliku = WezNazwePlikuZeSciezki(sciezka);
                int lastSlash = sciezka.LastIndexOf("/");
                if (lastSlash == -1)
                {
                    lastSlash = sciezka.LastIndexOf("\\");
                }
                string nazwaSciezki = sciezka.Substring(0, lastSlash + 1);

                if (result.ContainsKey(nazwaSciezki))
                {
                    List<string> Pliki = result[nazwaSciezki];
                    Pliki.Add(nazwaPliku);
                    result[nazwaSciezki] = Pliki;
                }

                else
                {
                    List<string> Pliki = new List<string>();
                    Pliki.Add(nazwaPliku);
                    result.Add(nazwaSciezki, Pliki);
                }

            }


            return result;
        }

        public List<string> ZnajdzPlikiPoNazwie(string nazwa, List<string> sciezkiPlikow)
        {
            List<string> SciezkiZPlikami = new List<string>();
            foreach (string sciezka in sciezkiPlikow)
            {

                string nazwaPliku = WezNazwePlikuZeSciezki(sciezka);
                if (nazwaPliku.Contains(nazwa))
                {
                    SciezkiZPlikami.Add(sciezka);
                }

            }

            return SciezkiZPlikami;
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

        public List<string> WczytajPlikiZFolderu(string sciezkaDoKatalogu, string rozszerzenie)
        {
            List<string> osoby = new List<string>();

            List<string> pliki = Directory.GetFiles(sciezkaDoKatalogu).ToList<string>();
            foreach (string plik in pliki)
            {
                string zawartoscPliku = WczytajPlik(plik, rozszerzenie);

                /*Pomimo użycia parametru "rozszerzenie"  metoda wczytuje wszystkie pliki, 
                a nie tylko "txt". Działa prawidłowo dopiero po dodaniu poniższego warunku. */


                if (plik.EndsWith(rozszerzenie))

                    /*Dzieje się tak samo nawet wtedy, gdy z metody WczytajPlik 
                                usunę warunek IsNullOrEmpty. Czy nie powinno być tak, 
                                że ścieżki inne niż z końcówką "txt" są pomijane? Chyba się zamotałem :))*/


                    osoby.Add(zawartoscPliku);
            }


            return osoby;
        }

        public List<String> ExcellWczytajWiersz(string sciezka, int arkusz, int wiersz)
        {
            List<String> result = new List<string>();
            if (File.Exists(sciezka))
            {
                Application xlApp = new Application();
                Workbook xlWorkbook = xlApp.Workbooks.Open(Path.GetFullPath(sciezka));
                Worksheet xlWorksheet = xlWorkbook.Sheets[arkusz];
                Range xlRange = xlWorksheet.UsedRange;

                for (int i = 1; i <= xlRange.Columns.Count; i++)
                {
                    result.Add(xlRange.Cells[wiersz, i].Value);
                }

                ZamknijPlik(xlApp, xlWorkbook, xlWorksheet, xlRange);
            }


            return result;
        }

        private void ZamknijPlik(Application xlApp, Workbook xlWorkbook, Worksheet xlWorksheet, Range xlRange)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}

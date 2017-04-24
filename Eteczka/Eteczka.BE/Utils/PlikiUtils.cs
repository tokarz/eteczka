using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

    }
}

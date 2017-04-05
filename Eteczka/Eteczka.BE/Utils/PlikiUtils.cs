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

    }
}

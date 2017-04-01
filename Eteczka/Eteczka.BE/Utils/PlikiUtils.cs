using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

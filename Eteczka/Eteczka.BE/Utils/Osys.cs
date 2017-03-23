﻿using System;
using System.Numerics;

namespace Eteczka.BE.Utils
{
    public class Osys
    {
        private static int MODULO_KONTO = 97;
        private string PoleKlasy = "ta zmienna nic nie robi, pokatuje tylko, ze to jest miejsce na uzycie slow public lub private  :) ";

        public bool SprawdzIban(string kontoBank)
        {
            bool result = false;
            string kontoBankPlBezSpacji = "PL" + kontoBank.Replace(" ", "").Trim();

            if (kontoBankPlBezSpacji.Length < 12)
            {
                return false;
            }

            //Substring w C# ma tylko 1 duza litere : Substring a nie SubString
            string konto1 = kontoBankPlBezSpacji.Substring(0, 4);
            string konto2 = kontoBankPlBezSpacji.Substring(4);

            kontoBankPlBezSpacji = konto2 + konto1;

            konto1 = kontoBankPlBezSpacji.Replace("PL", "2521");
            //Musimy uzyc czegos nowego w C# - Wyjatkow. Metoda Int32.Parse(...) wyrzuci wyjatek i przerwie program jesli jej argument nie bedzie poprawna liczba.
            //Aby temu zapobiec i nadal pozostac w tej funkcji, musimy uzyc blokow try{}catch{}. Dzieki temu wyjatek zostaje zlapany i wtedy robimy return false.
            try
            {
                //Magiczna liczbe 97 zmienilem na stala MODULO_KONTO, zeby nie musiec jej pamietac i nadac jej jakas znaczaca nazwe (bo dlaczego 97? Co ta liczba oznacza?)
                //Substring - indeksowanie w C# zaczynamy od zera, czyli 1 znak stringa to string[0] itd az do string[string.Length - 1]. Dlatego wpisalem 0,7 a nie 1,8 ale moze
                //chciales zaczac od 2 znaku a nie 1 ? tego nei wiem:)
                BigInteger parsedIbanNumber = BigInteger.Parse(konto1);

                result = (parsedIbanNumber % MODULO_KONTO) == 1;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool SprawdzIbanPoElementach(string kontoBank)
        {
            string kontoBankPlBezSpacji = "PL" + kontoBank.Replace(" ", "").Trim();

            if (kontoBankPlBezSpacji.Length != 28)
            {
                return false;
            }

            string konto1 = kontoBankPlBezSpacji.Substring(0, 4);
            string konto2 = kontoBankPlBezSpacji.Substring(4);

            kontoBankPlBezSpacji = konto2 + konto1;

            konto1 = kontoBankPlBezSpacji.Replace("PL", "2521");

            try
            {

                int reszta1 = Int32.Parse(konto1.Substring(0, 9)) % MODULO_KONTO;

                konto2 = reszta1.ToString() + konto1.Substring(9, 7);
                int reszta2 = Int32.Parse(konto2) % MODULO_KONTO;

                string konto3 = reszta2.ToString() + konto1.Substring(16, 7);
                int reszta3 = Int32.Parse(konto3) % MODULO_KONTO;

                string konto4 = reszta3.ToString() + konto1.Substring(23);
                int reszta4 = Int32.Parse(konto4) % MODULO_KONTO;

                if (reszta4 == 1)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool SprawdzPesel(string plecOsoby, string peselOsoby)
        {
            //kod nie jest bezpieczny na podanie np pustych stringow. jak podam SprawdzPesel("","") dostane w twarz wyjatkiem:)
            int liczbaKtrl = 0;
            int cyfraPesel = 0;
            string plecBezSpacji = plecOsoby.Replace(" ", "").Trim();
            string peselBezSpacji = peselOsoby.Replace(" ", "").Trim();

            //petle for zawsze lepiej zaczac od 0! Wtedy nie musisz pamietac o -1 i sie glowic
            //nie musisz na koniec kazdej linii dawac srednika. Srednik konczy wyrazenia i przypisania, a nie kazda linijke
            for (int licznik = 0; licznik < 10; licznik++)
            {
                //tutaj Twoj kod mi nie zadzialal i test byl czerwony. Sprawdzilem algorytm i okazalo sie ze napisales to tak
                //jakbsy sprawdzal cyfry od prawej do lewej, a powinienes od lewej do prawej (miales mnozniki 1,3,7,9 a mialy byc 9, 7, 3, 1)
                cyfraPesel = Int32.Parse(peselBezSpacji.Substring(licznik, 1));
                switch (licznik)
                {
                    //switch case  - Twoje wyrazenie :
                    //              (licznik == 1) || (licznik == 5) || (licznik == 9)
                    // jest wyrazeniem logicznym - czyli wynik tych 3 operacji daje true lub false.
                    //W instrukcji switch wazny jest typ obiektu (licznik). Tutaj jest to int, wiec wartosci po "case" tez musza byc typu int.
                    case 0:
                    case 4:
                    case 8:
                        liczbaKtrl = liczbaKtrl + cyfraPesel * 9;
                        break;
                    case 1:
                    case 5:
                    case 9:
                        liczbaKtrl = liczbaKtrl + cyfraPesel * 7;
                        break;
                    case 2:
                    case 6:
                        liczbaKtrl = liczbaKtrl + cyfraPesel * 3;
                        break;
                    default:
                        liczbaKtrl = liczbaKtrl + cyfraPesel * 1;
                        break;
                }
            }

            int reszta = liczbaKtrl % 10;
            //Tu miales sprawdzenie czy reszta jest rowna 10 a jesli tak ustawiales ja na 0, ale przy modulo 10 nigdy nie bedziesz mial reszty 10
            //wiec to usunalem


            //tu uwaga - od razu jak cyfra kontrolna sie zgadza - robisz return. Zanim sprawdziles czy to dziewucha czy facet:) wiec moze lepiej zrobic to na koncu, badz 
            //uzyc zmiennej wynik i do niej przypisac wynik
            bool czyPeselZgodny = true;
            if (Int32.Parse(peselBezSpacji.Substring(10, 1)) != reszta)
            {
                czyPeselZgodny =  false;
            }

            //Uzyles znaku $, pewnie z foxa. Tu string jest obiektem, wiec ma w swoim ciele metode "Contains" ktora zwraca true lub false gdy ciag znakow zawiera konkretny znak
            string cyfraKontrolna = peselBezSpacji.Substring(10);
            string kobieceKoncowki = "02468";
            string meskieKoncowki = "13579";
            //wyciagnalem rzeczy ktorych ponizej wielokrotnie uzywamy do zmiennych, zeby ich za kazdym razem nie liczyc. Nadalem nazwy takie by wiadomo bylo co chcemy sprawdzic

            if (plecBezSpacji == "M")
            {
                if (kobieceKoncowki.Contains(cyfraKontrolna))
                {
                    czyPeselZgodny = false;
                }
            }
            else
            {
                if (meskieKoncowki.Contains(cyfraKontrolna))
                {
                    czyPeselZgodny = false;
                }
            }
            return czyPeselZgodny;
        }
    }
}

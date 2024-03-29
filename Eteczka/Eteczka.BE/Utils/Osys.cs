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
    }
}

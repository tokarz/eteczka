using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace Eteczka.BE.Mappers
{
    public class JsonToKatFirmyMapper : IJsonToKatFirmyMapper
    {
        public KatFirmy Map(JToken parsedJson)
        {
            KatFirmy aktualnaFirma = new KatFirmy();

            aktualnaFirma.Firma = parsedJson["firma"].ToString();
            aktualnaFirma.Nazwa = parsedJson["nazwa"].ToString();
            aktualnaFirma.Nazwaskrocona = parsedJson["nazwaskrocona"].ToString();
            aktualnaFirma.Ulica = parsedJson["ulica"].ToString();
            aktualnaFirma.Numerdomu = parsedJson["numerdomu"].ToString();
            aktualnaFirma.Numerlokalu = parsedJson["numerlokalu"].ToString();
            aktualnaFirma.Miasto = parsedJson["miasto"].ToString();
            aktualnaFirma.Kodpocztowy = parsedJson["kodpocztowy"].ToString();
            aktualnaFirma.Poczta = parsedJson["poczta"].ToString();
            aktualnaFirma.Gmina = parsedJson["gmina"].ToString();
            aktualnaFirma.Powiat = parsedJson["powiat"].ToString();
            aktualnaFirma.Wojewodztwo = parsedJson["wojewodztwo"].ToString();
            aktualnaFirma.Nip = parsedJson["nip"].ToString();
            aktualnaFirma.Regon = parsedJson["regon"].ToString();
            aktualnaFirma.Pesel = parsedJson["pesel"].ToString();
            aktualnaFirma.Nazwa2 = parsedJson["nazwa2"].ToString();
            aktualnaFirma.Nazwisko = parsedJson["nazwisko"].ToString();
            aktualnaFirma.Imie = parsedJson["imie"].ToString();

            aktualnaFirma.Datamodify = DateTime.Parse(parsedJson["datamodify"].ToString());
            aktualnaFirma.Idoper = parsedJson["idoper"].ToString(); ;
            aktualnaFirma.Idakcept = parsedJson["idakcept"].ToString(); ;
            aktualnaFirma.Dataakcept = DateTime.Parse(parsedJson["dataakcept"].ToString());
            aktualnaFirma.Systembazowy = parsedJson["systembazowy"].ToString();
            //aktualnaFirma.Waitingroom = parsedJson["waitingroom"].ToString();

            string usuniety = parsedJson["usuniety"].ToString();
            if (usuniety == "0")
            {
                aktualnaFirma.Usuniety = false;
            }
            else
            {
                aktualnaFirma.Usuniety = true;
            }

            return aktualnaFirma;
        }
    }
}

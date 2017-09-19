using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace Eteczka.BE.Mappers
{
    public class JsonToPlikiMapper : IJsonToPlikiMapper
    {
        public Pliki Map(JToken parsedJson)
        {
            Pliki wczytanyPlik = new Pliki();
            wczytanyPlik.DataAkcept = DateTime.Parse(parsedJson["pesel"].ToString());
            wczytanyPlik.DataDokumentu = DateTime.Parse(parsedJson["datadokumentu"].ToString());
            wczytanyPlik.DataModyfikacji = DateTime.Parse(parsedJson["datamodyfikacji"].ToString());
            wczytanyPlik.DataPocz = DateTime.Parse(parsedJson["datapocz"].ToString());
            wczytanyPlik.DataSkanu = DateTime.Parse(parsedJson["dataSkanu"].ToString());
            wczytanyPlik.DokumentWlasny = bool.Parse(parsedJson["dokumentwlasny"].ToString());
            wczytanyPlik.IdAkcept = parsedJson["idakcept"].ToString();
            wczytanyPlik.IdOper = parsedJson["idoper"].ToString();
            wczytanyPlik.NazwaPliku = parsedJson["nazwapliku"].ToString();
            wczytanyPlik.NumerEad = parsedJson["numeread"].ToString();
            wczytanyPlik.OpisDodatkowy = parsedJson["opisdodatkowy"].ToString();
            wczytanyPlik.PelnaSciezka = parsedJson["pelnasciezka"].ToString();
            wczytanyPlik.Symbol = parsedJson["symbol"].ToString();
            wczytanyPlik.TypPliku = parsedJson["typpliku"].ToString();

            return wczytanyPlik;
        }
    }
}

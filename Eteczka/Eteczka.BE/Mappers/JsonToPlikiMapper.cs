using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace Eteczka.BE.Mappers
{
    public class JsonToPlikiMapper : IJsonToPlikiMapper
    {
        public Pliki Map(JToken parsedJson)
        {


            Pliki wczytanyPlik = new Pliki()
            {
                Id = long.Parse(parsedJson["id"].ToString()),
                Firma = parsedJson["firma"].ToString(),
                NumerEad = parsedJson["numeread"].ToString(),
                Symbol = parsedJson["symbol"].ToString(),
                DataSkanu = DateTime.Parse(parsedJson["dataskanu"].ToString()),
                DataDokumentu = DateTime.Parse(parsedJson["datadokumentu"].ToString()),
                DataPocz = DateTime.Parse(parsedJson["datapocz"].ToString()),
                DataKoniec = DateTime.Parse(parsedJson["datakoniec"].ToString()),
                NazwaScan = parsedJson["nazwascan"].ToString(),
                NazwaEad = parsedJson["nazwaead"].ToString(),
                PelnasciezkaEad = parsedJson["pelnasciezkaead"].ToString(),
                TypPliku = parsedJson["typpliku"].ToString(),
                OpisDodatkowy = parsedJson["opisdodatkowy"].ToString(),
                DokumentWlasny = bool.Parse(parsedJson["dokwlasny"].ToString()),
                IdOper = parsedJson["idoper"].ToString(),
                IdAkcept = parsedJson["idakcept"].ToString(),
                DataModyfikacji = DateTime.Parse(parsedJson["datamodify"].ToString()),
                DataAkcept = DateTime.Parse(parsedJson["dataakcept"].ToString()),
                Systembazowy = parsedJson["systembazowy"].ToString(),//EAD
                Usuniety = bool.Parse(parsedJson["usuniety"].ToString()),
            };

            return wczytanyPlik;
        }
    }
}

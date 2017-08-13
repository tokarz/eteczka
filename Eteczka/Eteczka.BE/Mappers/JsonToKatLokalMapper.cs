using Eteczka.DB.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace Eteczka.BE.Mappers
{
    public class JsonToKatLokalMapper : IJsonToKatLokalMapper
    {
        public KatLokalPapier Map(JToken parsedJson)
        {
            KatLokalPapier aktualneArchiwumDoWczytania =  new KatLokalPapier();
            aktualneArchiwumDoWczytania.Firma = parsedJson["firma"].ToString();
            aktualneArchiwumDoWczytania.Nazwa = parsedJson["nazwa"].ToString();
            aktualneArchiwumDoWczytania.Ulica = parsedJson["ulica"].ToString();
            aktualneArchiwumDoWczytania.Numerdomu = parsedJson["numerdomu"].ToString();
            aktualneArchiwumDoWczytania.Numerlokalu = parsedJson["numerlokalu"].ToString();
            aktualneArchiwumDoWczytania.Kodpocztowy = parsedJson["kodpocztowy"].ToString();
            aktualneArchiwumDoWczytania.Miasto = parsedJson["miasto"].ToString();
            aktualneArchiwumDoWczytania.Poczta = parsedJson["poczta"].ToString();
            aktualneArchiwumDoWczytania.LokalPapier = parsedJson["lokalpapier"].ToString();
            aktualneArchiwumDoWczytania.Datamodify = DateTime.Parse(parsedJson["datamodify"].ToString());
            aktualneArchiwumDoWczytania.Dataakcept = DateTime.Parse(parsedJson["dataakcept"].ToString());

            return aktualneArchiwumDoWczytania;
        }
    }
}

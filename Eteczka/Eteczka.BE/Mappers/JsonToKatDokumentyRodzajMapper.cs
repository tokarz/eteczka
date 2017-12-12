using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace Eteczka.BE.Mappers
{
    public class JsonToKatDokumentyRodzajMapper : IJsonToKatDokumentyRodzajMapper
    {
        public KatDokumentyRodzaj Map(JToken parsedJson)
        {
            KatDokumentyRodzaj aktualnyRodzaj = new KatDokumentyRodzaj();

            aktualnyRodzaj.Symbol = parsedJson["symbol"].ToString();
            aktualnyRodzaj.Nazwa = parsedJson["nazwa"].ToString();
            string wlasny = parsedJson["dokwlasny"].ToString();
            if (wlasny == "0")
            {
                aktualnyRodzaj.Dokwlasny = false;
            }
            else
            {
                aktualnyRodzaj.Dokwlasny = true;
            }

            aktualnyRodzaj.Jrwa = parsedJson["jrwa"].ToString();
            aktualnyRodzaj.Teczkadzial = parsedJson["teczkadzial"].ToString();
            aktualnyRodzaj.Typedycji = parsedJson["typedycji"].ToString();
            aktualnyRodzaj.Idoper = parsedJson["idoper"].ToString();
            aktualnyRodzaj.Idakcept = parsedJson["idakcept"].ToString();
            aktualnyRodzaj.Datamodify = DateTime.Parse(parsedJson["datamodify"].ToString());
            aktualnyRodzaj.Dataakcept = DateTime.Parse(parsedJson["dataakcept"].ToString());
            aktualnyRodzaj.SystemBazowy = parsedJson["systembazowy"].ToString();

            string usuniety = parsedJson["usuniety"].ToString();
            if (usuniety == "0")
            {
                aktualnyRodzaj.Usuniety = false;
            }
            else
            {
                aktualnyRodzaj.Usuniety = true;
            }


            aktualnyRodzaj.Confidential = int.Parse(parsedJson["confidential"].ToString());
            aktualnyRodzaj.SymbolEad = parsedJson["symbolead"].ToString();

            string audyt = parsedJson["audyt"].ToString();
            if (audyt == "0")
            {
                aktualnyRodzaj.Audyt = false;
            }
            else
            {
                aktualnyRodzaj.Audyt = true;
            }


            return aktualnyRodzaj;

        }
    }
}

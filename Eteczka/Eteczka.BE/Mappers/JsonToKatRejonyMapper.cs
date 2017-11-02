using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Mappers
{
    public class JsonToKatRejonyMapper : IJsonToKatRejonyMapper
    {
        public KatRejony Map(JToken parsedJson)
        {
            KatRejony aktualnyRejon = new KatRejony();

            aktualnyRejon.Rejon = parsedJson["rejon"].ToString();
            aktualnyRejon.Nazwa = parsedJson["nazwa"].ToString();
            aktualnyRejon.Firma = parsedJson["firma"].ToString();
            aktualnyRejon.Idoper = parsedJson["idoper"].ToString(); ;
            aktualnyRejon.Idakcept = parsedJson["idakcept"].ToString(); ;
            aktualnyRejon.Datamodify = DateTime.Parse(parsedJson["datamodify"].ToString());
            aktualnyRejon.Dataakcept = DateTime.Parse(parsedJson["dataakcept"].ToString());
            aktualnyRejon.Mnemonik = parsedJson["mnemonik"].ToString();
            aktualnyRejon.Systembazowy = parsedJson["systembazowy"].ToString();

            string usuniety = parsedJson["usuniety"].ToString();
            if (usuniety == "0")
            {
                aktualnyRejon.Usuniety = false;
            }
            else
            {
                aktualnyRejon.Usuniety = true;
            }

            return aktualnyRejon;
        }
    }
}

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
            aktualnyRejon.Datamodify = DateTime.Now;
            aktualnyRejon.Dataakcept = DateTime.Now;
            aktualnyRejon.Mnemonik = parsedJson["mnemonik"].ToString();

            return aktualnyRejon;
        }
    }
}

using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Mappers
{
    public class JsonToKonto5Mapper : IJsonToKonto5Mapper
    {
        public KatKonto5 Map(JToken token)
        {
            KatKonto5 wczytaneKonto5 = new KatKonto5();
            wczytaneKonto5.Konto5 = token["konto5"].ToString();
            wczytaneKonto5.Nazwa = token["nazwa"].ToString();
            wczytaneKonto5.Idoper = token["idoper"].ToString();
            wczytaneKonto5.Idakcept = token["idakcept"].ToString();
            wczytaneKonto5.Firma = token["firma"].ToString();
            wczytaneKonto5.Kontoskr = token["kontoskr"].ToString();
            wczytaneKonto5.Datamodify = DateTime.Parse(token["datamodify"].ToString());
            wczytaneKonto5.Dataakcept = DateTime.Parse(token["dataakcept"].ToString());
            wczytaneKonto5.Systembazowy = token["systembazowy"].ToString();
            wczytaneKonto5.Usuniety = 

            return wczytaneKonto5;
        }
    }
}

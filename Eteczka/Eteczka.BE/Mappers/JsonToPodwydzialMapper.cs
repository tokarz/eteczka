using Eteczka.DB.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Mappers
{
    public class JsonToPodwydzialMapper : IJsonToPodwydzialMapper
    {
        public KatPodWydzialy Map(JToken token)
        {
            KatPodWydzialy wczytanyPodwydzial = new KatPodWydzialy();
            wczytanyPodwydzial.Podwydzial = token["podwydzial"].ToString();
            wczytanyPodwydzial.Nazwa = token["nazwa"].ToString();
            wczytanyPodwydzial.Wydzial = token["wydzial"].ToString();
            wczytanyPodwydzial.Datamodify = DateTime.Parse(token["datamodify"].ToString());
            wczytanyPodwydzial.Idoper = token["idoper"].ToString();
            wczytanyPodwydzial.Idakcept = token["idakcept"].ToString();
            wczytanyPodwydzial.Dataakcept = DateTime.Parse(token["dataakcept"].ToString());
            wczytanyPodwydzial.Firma = token["firma"].ToString();

            return wczytanyPodwydzial;
        }
    }
}

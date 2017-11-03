﻿using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Mappers
{
    public class JsonToWydzialMapper : IJsonToWydzialMapper
    {
        public KatWydzialy Map(JToken token)
        {
            KatWydzialy wczytanyWydzial = new KatWydzialy();
            wczytanyWydzial.Wydzial = token["wydzial"].ToString();
            wczytanyWydzial.Nazwa = token["nazwa"].ToString();
            wczytanyWydzial.Datamodify = DateTime.Parse(token["datamodify"].ToString());
            wczytanyWydzial.Idoper = token["idoper"].ToString();
            wczytanyWydzial.Idakcept = token["idakcept"].ToString();
            wczytanyWydzial.Dataakcept = DateTime.Parse(token["dataakcept"].ToString());
            wczytanyWydzial.Firma = token["firma"].ToString();
            wczytanyWydzial.Systembazowy = token["systembazowy"].ToString();
            string usuniety = token["usuniety"].ToString();

            if (usuniety == "0")
            {
                wczytanyWydzial.Usuniety = false;
            }
            else
            {
                wczytanyWydzial.Usuniety = true;
            }

            return wczytanyWydzial;
        }
    }
}

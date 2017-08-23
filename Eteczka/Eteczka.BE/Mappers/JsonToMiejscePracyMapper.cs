using Eteczka.DB.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Mappers
{
    public class JsonToMiejscePracyMapper : IJsonToMiejscePracyMapper
    {
        public MiejscePracy Map(JToken parsedJson)
        {
            MiejscePracy miejscePracy = new MiejscePracy();

            miejscePracy.Firma = parsedJson["firma"].ToString();
            miejscePracy.Rejon = parsedJson["rejon"].ToString();
            miejscePracy.Wydzial = parsedJson["wydzial"].ToString();
            miejscePracy.Podwydzial = parsedJson["podwydzial"].ToString();
            miejscePracy.Konto5 = parsedJson["konto5"].ToString();
            miejscePracy.DataPocz = DateTime.Parse(parsedJson["datapocz"].ToString());
            string dataKoniec = parsedJson["datakoniec"].ToString(); ;
            if (dataKoniec.Equals("9999-99-99"))
            {
                miejscePracy.DataKoniec = DateTime.MaxValue.Date;
            }
            else
            {
                miejscePracy.DataKoniec = DateTime.Parse(dataKoniec);
            }
            miejscePracy.IdOper = parsedJson["idoper"].ToString();
            miejscePracy.IdAkcept = parsedJson["idakcept"].ToString();
            miejscePracy.DataModify = DateTime.Parse(parsedJson["datamodify"].ToString());
            miejscePracy.DataAkcept = DateTime.Parse(parsedJson["dataakcept"].ToString());
            miejscePracy.NumerEad = parsedJson["numeread"].ToString();
            miejscePracy.Id = long.Parse(parsedJson["id"].ToString());
            miejscePracy.Usuniety = false;

            return miejscePracy;
        }
    }
}

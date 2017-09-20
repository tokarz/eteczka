using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using System.Data;
using Eteczka.Model.Entities;

namespace Eteczka.DB.Mappers
{
    public class KatPodWydzialMapper : IKatPodWydzialMapper
    {
        public KatPodWydzialy MapujZSql(DataRow row)
        {
            KatPodWydzialy fetchedPodwydzial = new KatPodWydzialy();


            fetchedPodwydzial.Podwydzial = row[0].ToString();
            fetchedPodwydzial.Nazwa = row[1].ToString();
            fetchedPodwydzial.Wydzial = row[2].ToString();
            fetchedPodwydzial.Datamodify = DateTime.Parse(row[3].ToString());
            fetchedPodwydzial.Idoper = row[4].ToString();
            fetchedPodwydzial.Idakcept = row[5].ToString();
            fetchedPodwydzial.Dataakcept = DateTime.Parse(row[6].ToString());
            fetchedPodwydzial.Firma = row[7].ToString();
            fetchedPodwydzial.SystemBazowy = row[8].ToString();
            fetchedPodwydzial.Usuniety = bool.Parse(row[9].ToString());


            return fetchedPodwydzial;
        }
    }
            
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Eteczka.DB.Entities;

namespace Eteczka.DB.Mappers
{
    public class KatWydzialMapper : IKatWydzialMapper
    {
        public KatDzialy MapujzSql(DataRow row)
        {
            KatDzialy fetchedDzialy = new KatDzialy();

            fetchedDzialy.Wydzial = row[0].ToString();
            fetchedDzialy.Nazwa = row[1].ToString();
            fetchedDzialy.Datamodify = DateTime.Parse(row[2].ToString());
            fetchedDzialy.Idoper = row[3].ToString();
            fetchedDzialy.Idakcept = row[4].ToString();
            fetchedDzialy.Dataakcept = DateTime.Parse(row[5].ToString());
            fetchedDzialy.Firma = row[6].ToString();
            fetchedDzialy.Systembazowy = row[7].ToString();
            fetchedDzialy.Usuniety = bool.Parse(row[8].ToString());

            return fetchedDzialy;
        }
    }
}

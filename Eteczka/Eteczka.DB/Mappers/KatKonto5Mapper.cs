using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public class KatKonto5Mapper : IKatKonto5Mapper
    {
        public KatKonto5 MapujZSql(DataRow result)
        {
            KatKonto5 fetchedKonto5 = new KatKonto5();

            fetchedKonto5.Konto5 = result[0].ToString();
            fetchedKonto5.Nazwa = result[1].ToString();
            fetchedKonto5.Idoper = result[2].ToString();
            fetchedKonto5.Idakcept = result[3].ToString();
            fetchedKonto5.Firma = result[4].ToString();
            fetchedKonto5.Kontoskr = result[5].ToString();
            fetchedKonto5.Datamodify = DateTime.Parse(result[6].ToString());
            fetchedKonto5.Dataakcept = DateTime.Parse(result[7].ToString());
            fetchedKonto5.Systembazowy = result[8].ToString();
            fetchedKonto5.Usuniety = bool.Parse(result[9].ToString());



            return fetchedKonto5; 
        }
    }
}

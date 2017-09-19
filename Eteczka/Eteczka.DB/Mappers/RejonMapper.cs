using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public class RejonMapper : IRejonMapper
    {
        public KatRejony MapujZSql(DataRow result)
        {
             KatRejony fetchedRejon = new KatRejony();

            fetchedRejon.Rejon = result[0].ToString();
            fetchedRejon.Nazwa = result[1].ToString();
            fetchedRejon.Idoper = result[2].ToString();
            fetchedRejon.Idakcept = result[3].ToString();
            fetchedRejon.Firma = result[4].ToString();
            fetchedRejon.Datamodify = DateTime.Parse(result[5].ToString());
            fetchedRejon.Dataakcept = DateTime.Parse(result[6].ToString());
            fetchedRejon.Mnemonik = result[7].ToString();
            fetchedRejon.Systembazowy = result[8].ToString();
            fetchedRejon.Usuniety = bool.Parse(result[9].ToString());


            return fetchedRejon;
        }
    }
}

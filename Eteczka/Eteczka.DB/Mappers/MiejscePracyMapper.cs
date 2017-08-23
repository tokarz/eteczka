using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public class MiejscePracyMapper
    {
        public MiejscePracy MapujZsql(DataRow row)
        {
            MiejscePracy fetchedMiejscePracy = new MiejscePracy();

            fetchedMiejscePracy.Firma = row[0].ToString();
            fetchedMiejscePracy.Rejon = row[1].ToString();
            fetchedMiejscePracy.Wydzial = row[2].ToString();
            fetchedMiejscePracy.Podwydzial = row[3].ToString();
            fetchedMiejscePracy.Konto5 = row[4].ToString();
            fetchedMiejscePracy.DataPocz = DateTime.Parse(row[5].ToString());
            fetchedMiejscePracy.DataKoniec = DateTime.Parse(row[6].ToString());
            fetchedMiejscePracy.IdOper = row[7].ToString();
            fetchedMiejscePracy.IdAkcept = row[8].ToString();
            fetchedMiejscePracy.DataModify = DateTime.Parse(row[9].ToString());
            fetchedMiejscePracy.DataAkcept = DateTime.Parse(row[10].ToString());
            fetchedMiejscePracy.NumerEad = row[11].ToString();
            fetchedMiejscePracy.SystemBazowy = row[12].ToString();
            fetchedMiejscePracy.Usuniety = bool.Parse(row[13].ToString());
            //fetchedMiejscePracy.Id = row[14].ToString(); - tego chyba nie mapujemy? W miejscach pracy jest private set.

            return fetchedMiejscePracy;
        }
    }
}

       




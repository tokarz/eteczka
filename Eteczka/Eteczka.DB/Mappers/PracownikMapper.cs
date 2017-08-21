using Eteczka.DB.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Mappers
{
    public class PracownikMapper : IPracownikMapper
    {
        public Pracownik MapujZSql(DataRow row)
        {
            Pracownik fetchedUser = new Pracownik();
            fetchedUser.Imie = row[0].ToString();
            fetchedUser.Nazwisko = row[1].ToString();
            fetchedUser.PESEL = row[2].ToString();
            fetchedUser.Numeread = row[3].ToString();
            fetchedUser.Kraj = row[4].ToString();
            fetchedUser.NazwiskoRodowe= row[5].ToString();
            fetchedUser.ImieMatki = row[6].ToString();
            fetchedUser.ImieOjca= row[7].ToString();
            fetchedUser.PeselInny= row[8].ToString();
            fetchedUser.IdOper = row[9].ToString();
            fetchedUser.IdAkcept= row[10].ToString();
            fetchedUser.DataModify= DateTime.Parse(row[11].ToString());
            fetchedUser.DataAkcept= DateTime.Parse(row[12].ToString());
            fetchedUser.DataUrodzenia = row[13].ToString();
            fetchedUser.Imie2 = row[14].ToString();
            fetchedUser.SystemBazowy = row[15].ToString();
            fetchedUser.Usuniety = bool.Parse(row[16].ToString());

            return fetchedUser;
        }
    }
}

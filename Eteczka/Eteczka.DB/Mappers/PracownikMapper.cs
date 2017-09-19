using Eteczka.Model.Entities;
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
            fetchedUser.Imie = row["imie"].ToString();
            fetchedUser.Nazwisko = row["nazwisko"].ToString();
            fetchedUser.PESEL = row["pesel"].ToString();
            fetchedUser.Numeread = row["numeread"].ToString();
            fetchedUser.Kraj = row["kraj"].ToString();
            fetchedUser.NazwiskoRodowe = row["nazwiskorodowe"].ToString();
            fetchedUser.ImieMatki = row["imiematki"].ToString();
            fetchedUser.ImieOjca = row["imieojca"].ToString();
            fetchedUser.PeselInny = row["peselinny"].ToString();
            fetchedUser.IdOper = row["idoper"].ToString();
            fetchedUser.IdAkcept = row["idakcept"].ToString();
            fetchedUser.DataModify = DateTime.Parse(row["datamodify"].ToString());
            fetchedUser.DataAkcept = DateTime.Parse(row["dataakcept"].ToString());
            fetchedUser.DataUrodzenia = row["dataurodzenia"].ToString();
            fetchedUser.Imie2 = row["imie2"].ToString();
            fetchedUser.SystemBazowy = row["systembazowy"].ToString();
            fetchedUser.Usuniety = bool.Parse(row["usuniety"].ToString());
            fetchedUser.Kodkierownik = row["kodkierownik"].ToString();
            int parsedInt = -1;
            if (int.TryParse(row["confidential"].ToString(), out parsedInt))
            {
                fetchedUser.Confidential = parsedInt;
            }

            return fetchedUser;
        }
    }
}

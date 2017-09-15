using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public class FirmyMapper : IFirmyMapper
    {
        public KatFirmy MapujZSql(DataRow row)
        {
            KatFirmy fetchedFirma = new KatFirmy();
            fetchedFirma.Firma = row["firma"].ToString();
            fetchedFirma.Nazwa = row["nazwa"].ToString();
            fetchedFirma.Nazwaskrocona = row["nazwaskrocona"].ToString();
            fetchedFirma.Ulica = row["ulica"].ToString();
            fetchedFirma.Numerdomu = row["numerdomu"].ToString();
            fetchedFirma.Numerlokalu = row["numerlokalu"].ToString();
            fetchedFirma.Miasto = row["miasto"].ToString();
            fetchedFirma.Kodpocztowy = row ["kodpocztowy"].ToString();
            fetchedFirma.Poczta = row["poczta"].ToString();
            fetchedFirma.Gmina = row["gmina"].ToString();
            fetchedFirma.Powiat = row["powiat"].ToString();
            fetchedFirma.Wojewodztwo = row["wojewodztwo"].ToString();
            fetchedFirma.Nip = row["nip"].ToString();
            fetchedFirma.Regon = row["regon"].ToString();
            fetchedFirma.Nazwa2 = row["nazwa2"].ToString();
            fetchedFirma.Pesel = row["pesel"].ToString();
            fetchedFirma.Idoper = row["idoper"].ToString();
            fetchedFirma.Idakcept = row["idakcept"].ToString();
            fetchedFirma.Nazwisko = row["nazwisko"].ToString();
            fetchedFirma.Imie = row["imie"].ToString();
            fetchedFirma.Datamodify = DateTime.Parse(row["datamodify"].ToString());
            fetchedFirma.Dataakcept = DateTime.Parse(row["dataakcept"].ToString());
            fetchedFirma.Systembazowy = row["systembazowy"].ToString();
            fetchedFirma.Usuniety = bool.Parse(row["usuniety"].ToString());
            fetchedFirma.Waitingroom = row["waitingroom"].ToString();

            return fetchedFirma;
        }
    }
}

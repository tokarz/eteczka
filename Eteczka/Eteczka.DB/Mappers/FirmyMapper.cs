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
            fetchedFirma.Firma = row[0].ToString();
            fetchedFirma.Nazwa = row[1].ToString();
            fetchedFirma.Nazwaskrocona = row[2].ToString();
            fetchedFirma.Ulica = row[3].ToString();
            fetchedFirma.Numerdomu = row[4].ToString();
            fetchedFirma.Numerlokalu = row[5].ToString();
            fetchedFirma.Miasto = row[6].ToString();
            fetchedFirma.Kodpocztowy = row [7].ToString();
            fetchedFirma.Poczta = row[8].ToString();
            fetchedFirma.Gmina = row[9].ToString();
            fetchedFirma.Powiat = row[10].ToString();
            fetchedFirma.Wojewodztwo = row[11].ToString();
            fetchedFirma.Nip = row[12].ToString();
            fetchedFirma.Regon = row[13].ToString();
            fetchedFirma.Nazwa2 = row[14].ToString();
            fetchedFirma.Pesel = row[15].ToString();
            fetchedFirma.Idoper = row[16].ToString();
            fetchedFirma.Idakcept = row[17].ToString();
            fetchedFirma.Nazwisko = row[18].ToString();
            fetchedFirma.Imie = row[19].ToString();
            fetchedFirma.Datamodify = DateTime.Parse(row[20].ToString());
            fetchedFirma.Dataakcept = DateTime.Parse(row[21].ToString());
            fetchedFirma.Systembazowy = row[22].ToString();
            fetchedFirma.Usuniety = bool.Parse(row[23].ToString());


            return fetchedFirma;
        }
    }
}

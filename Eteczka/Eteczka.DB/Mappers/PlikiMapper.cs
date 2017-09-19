using Eteczka.Model.Entities;
using System;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public class PlikiMapper : IPlikiMapper
    {

        public Pliki MapujZSql(DataRow row)
        {
            Pliki fetchedDok = new Pliki();

            fetchedDok.Symbol = row[0].ToString();
            fetchedDok.DataSkanu = DateTime.Parse(row[0].ToString());
            fetchedDok.DataDokumentu = DateTime.Parse(row[0].ToString());
            fetchedDok.DataPocz = DateTime.Parse(row[0].ToString());
            fetchedDok.DataKoniec = DateTime.Parse(row[0].ToString());
            fetchedDok.NazwaPliku = row[0].ToString();
            fetchedDok.PelnaSciezka = row[0].ToString();
            fetchedDok.TypPliku = row[0].ToString();
            fetchedDok.OpisDodatkowy = row[0].ToString();
            fetchedDok.NumerEad = row[0].ToString();
            fetchedDok.DokumentWlasny = bool.Parse(row[0].ToString());
            fetchedDok.IdOper = row[0].ToString();
            fetchedDok.IdAkcept = row[0].ToString();
            fetchedDok.DataModyfikacji = DateTime.Parse(row[0].ToString());
            fetchedDok.DataAkcept = DateTime.Parse(row[0].ToString());

            return fetchedDok;
        }
    }
}

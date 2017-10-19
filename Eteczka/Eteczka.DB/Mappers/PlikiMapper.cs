using Eteczka.Model.Entities;
using System;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public class PlikiMapper : IPlikiMapper
    {
        public Pliki MapujZSql(DataRow row)
        {
            Pliki fetchedDok = new Pliki()
            {
                Id = long.Parse(row["id"].ToString()),
                Firma = row["firma"].ToString(),
                NumerEad = row["numeread"].ToString(),
                Symbol = row["symbol"].ToString(),
                DataSkanu = DateTime.Parse(row["dataskanu"].ToString()),
                DataDokumentu = DateTime.Parse(row["datadokumentu"].ToString()),
                DataPocz = DateTime.Parse(row["datapocz"].ToString()),
                DataKoniec = DateTime.Parse(row["datakoniec"].ToString()),
                NazwaScan = row["nazwascan"].ToString(),
                NazwaEad = row["nazwaead"].ToString(),
                PelnasciezkaEad = row["pelnasciezkaead"].ToString(),
                TypPliku = row["typpliku"].ToString(),
                OpisDodatkowy = row["opisdodatkowy"].ToString(),
                DokumentWlasny = bool.Parse(row["dokwlasny"].ToString()),
                IdOper = row["idoper"].ToString(),
                IdAkcept = row["idakcept"].ToString(),
                DataModyfikacji = DateTime.Parse(row["datamodify"].ToString()),
                DataAkcept = DateTime.Parse(row["dataakcept"].ToString()),
                Systembazowy = row["systembazowy"].ToString(),//EAD
                Usuniety = bool.Parse(row["usuniety"].ToString()),
                Imie = row["imie"].ToString(),
                Nazwisko = row["nazwisko"].ToString(),
                Pesel = row["pesel"].ToString(),
                DrugieImie = row["imie2"].ToString(),
                DataUrodzenia = DateTime.Parse(row["dataurodzenia"].ToString())
            };

            return fetchedDok;
        }
    }
}

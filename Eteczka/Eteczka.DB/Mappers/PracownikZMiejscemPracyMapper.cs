using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Mappers
{
    public class PracownikZMiejscemPracyMapper : IPracownikZMiejscemPracyMapper
    {
        public Pracownik MapujDoPracownika(PracownikZMiejscemPracy pracownikDoDodania)
        {
            Pracownik zmapowany = new Pracownik()
            {
                PESEL = pracownikDoDodania.Pesel,
                DataUrodzenia = pracownikDoDodania.DataUrodzenia,
                Nazwisko = pracownikDoDodania.Nazwisko,
                Imie = pracownikDoDodania.Imie,
                Imie2 = pracownikDoDodania.DrugieImie,
                NazwiskoRodowe = pracownikDoDodania.NazwiskoRodowe,
                ImieMatki = pracownikDoDodania.ImieMatki,
                ImieOjca = pracownikDoDodania.ImieOjca,
                Kodkierownik = pracownikDoDodania.KodKierownik,
                Confidential = pracownikDoDodania.Confidential,
                PeselInny = pracownikDoDodania.PeselObcy
            };

            return zmapowany;
        }

        public MiejscePracy MapujDoMiejscaPracy(PracownikZMiejscemPracy miejscePracyDoDodania)
        {
            MiejscePracy zmapowaneMiejsce = new MiejscePracy()
            {
                Firma = miejscePracyDoDodania.Firma,
                Rejon = miejscePracyDoDodania.Rejon,
                Wydzial = miejscePracyDoDodania.Wydzial,
                Podwydzial = miejscePracyDoDodania.PodWydzial,
                Konto5 = miejscePracyDoDodania.Konto5,
                DataPocz = miejscePracyDoDodania.DataPocz,
                DataKoniec = miejscePracyDoDodania.DataKoniec
            };
            return zmapowaneMiejsce;
        }
    }
}

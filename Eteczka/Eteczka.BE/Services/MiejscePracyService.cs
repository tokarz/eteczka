using Eteczka.BE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Eteczka.Model.DTO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public class MiejscePracyService : IMiejscePracyService
    {
        private MiejscePracyDAO _MiejscePracyDao;

        public MiejscePracyService(MiejscePracyDAO miejscePracyDao)
        {
            this._MiejscePracyDao = miejscePracyDao;
        }

        public List<MiejscePracyDlaPracownika> PobierzMiejscaPracyDlaPracownika(string numerEad, string firma)
        {
            List<MiejscePracyDlaPracownika> pobraneMiejscaPracy = _MiejscePracyDao.PobierzMiejscaPracyDlaPracownika(numerEad, firma);

            return pobraneMiejscaPracy;
        }

        public InsertResult DodajMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoDodania)
        {

            bool CzyPracownikMaAktualneMiejscePracy = _MiejscePracyDao.CzyPracownikMaAktualneMiejscePracy(miejsceDoDodania);
            InsertResult result = new InsertResult()
            {
                Result = _MiejscePracyDao.DodajMiejscePracy(miejsceDoDodania, sesja.IdUzytkownika, sesja.IdUzytkownika)
            };
           
            if (CzyPracownikMaAktualneMiejscePracy && result.Result)
            {
                result.Message = "Dodano nowe miejsce pracy. Uwaga: pracownik już posiada w firmie inne aktualne miejsce pracy. Upewnij się czy nie należy zamknąć poprzedniego miejsca pracy.";
            }
            else
            {
                result.Message = result.Result == true ? "Dodano nowe miejsce pracy." : "Próba dodania nowego miejsca pracy nie powiodła się. Skontaktuj się z administratorem.";
            }

            return result;
        }

        public InsertResult EdytujMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoEdycji)
        {
            InsertResult result = new InsertResult();

            if (_MiejscePracyDao.CzyMiejscePracyIstnieje(miejsceDoEdycji.Id))
            {
                result.Result = _MiejscePracyDao.EdytujMiejscePracy(miejsceDoEdycji, sesja.IdUzytkownika, sesja.IdUzytkownika);
                result.Message = result.Result == true ? "Zapisano zmiany." : "Zmiany nie zostały zapisane. Skontaktuj się z administratorem.";
            }
            else
            {
                result.Message = "Zmiany nie zostały zapisane. Skontaktuj się z administratorem.";
            }
            return result;
        }

        public InsertResult UsunMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoUsuniecia)
        {
            InsertResult result = new InsertResult();
        
            if(_MiejscePracyDao.CzyMiejscePracyIstnieje(miejsceDoUsuniecia.Id))
            {
                if ( _MiejscePracyDao.CzyPracownikPosiadaWiecejNizJednoMiejscePracyWFirmie(miejsceDoUsuniecia))
                {
                    result.Result = _MiejscePracyDao.UsunMiejscePracy(miejsceDoUsuniecia.Id, sesja.IdUzytkownika, sesja.IdUzytkownika);
                    result.Message = result.Result == true ? "Miejsce pracy zostało usunięte." : "Miejsce pracy nie zostało usunięte. Skontaktuj się z administratorem.";
                }
                else
                {
                    result.Message = "Nie można usunąć miejsca pracy. Pracownik musi posiadać przynajmniej jedno miejsce pracy. Dodaj pracownikowi inne miejsce pracy, a potem usuń stare, lub zmień datę końca zatrudnienia.";
                }
  
            }

            return result;
        }
    }
}

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
            InsertResult result = new InsertResult();

            if (_MiejscePracyDao.SprawdzCzyMiejscePracyIstniejeWFirmie(miejsceDoDodania.Id))
            {
                result.Message = "Pracownik  aktualnie posiada w firmie miejsce pracy. Upewnij się czy nie należy zamknąć poprzedniego miejsca pracy";

            }

            result.Result = _MiejscePracyDao.DodajMiejscePracy(miejsceDoDodania, sesja.IdUzytkownika, sesja.IdUzytkownika);

            return result;
        }

        public InsertResult EdytujMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoEdycji)
        {
            InsertResult result = new InsertResult();

            if (_MiejscePracyDao.SprawdzCzyMiejscePracyIstniejeWFirmie(miejsceDoEdycji.Id))
            {
                result.Result = _MiejscePracyDao.EdytujMiejscePracy(miejsceDoEdycji, sesja.IdUzytkownika, sesja.IdUzytkownika);
            }
            return result;
        }

        public InsertResult UsunMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoUsuniecia)
        {
            InsertResult result = new InsertResult();
        
            if(_MiejscePracyDao.SprawdzCzyMiejscePracyIstniejeWFirmie(miejsceDoUsuniecia.Id))
            {
                result.Result = _MiejscePracyDao.UsunMiejscePracy(miejsceDoUsuniecia.Id, sesja.IdUzytkownika, sesja.IdUzytkownika);
            }

            return result;
        }
    }
}

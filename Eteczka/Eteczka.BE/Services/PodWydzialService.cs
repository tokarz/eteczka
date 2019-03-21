using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public class PodWydzialService : IPodWydzialService
    {
        private KatPodwydzialDAO _PodWydzialDAO;
     

        public PodWydzialService(KatPodwydzialDAO PodWydzialDAO)
        {
            this._PodWydzialDAO = PodWydzialDAO;
            
        }

        public List<KatPodWydzialy> PobranaListaPodWydzialow(SessionDetails sesja, string wydzial)
        {
            List<KatPodWydzialy> pobranePodWydzialy = _PodWydzialDAO.PobierzPodWydzialy(sesja.AktywnaFirma.Firma, wydzial);

            return pobranePodWydzialy;
        }

        public InsertResult DodajPodWydzial(KatPodWydzialy wydzialDoDodania, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (!_PodWydzialDAO.SprawdzCzyPodWydzialIstnieje(wydzialDoDodania.Firma, wydzialDoDodania.Wydzial, wydzialDoDodania.Podwydzial))
            {
                result.Result = _PodWydzialDAO.DodajPodWydzial(wydzialDoDodania, idoper, idakcept);
                result.Message = result.Result == true ? "Podwydział został dodany." : "Dodawanie wydziału nie powiodło się.";
            }
            else
            {
                result.Message = "Dodawanie nie powiodło się. Podwydział już istnieje.";
            }

            return result;
        }

        public InsertResult EdytujPodWydzial(KatPodWydzialy podWydzialDoEdycji, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (_PodWydzialDAO.SprawdzCzyPodWydzialIstnieje(podWydzialDoEdycji.Firma, podWydzialDoEdycji.Wydzial, podWydzialDoEdycji.Podwydzial))
            {
                result.Result = _PodWydzialDAO.EdytujPodWydzial(podWydzialDoEdycji, idoper, idakcept);
                result.Message = result.Result == true ? "Edycja zakończona pomyślnie." : "Edycja się nie powiodła.";
            }
            else
            {
                result.Message = "Edycja nie powiodła się. Podany podwydział nie istnieje w bazie danych.";
            }

            return result;
        }

        public InsertResult UsunPodWydzial(KatPodWydzialy podWydzialDoUsuniecia, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (_PodWydzialDAO.SprawdzCzyPodWydzialIstnieje(podWydzialDoUsuniecia.Firma, podWydzialDoUsuniecia.Wydzial, podWydzialDoUsuniecia.Podwydzial))
            {
                result.Result = _PodWydzialDAO.UsunPodWydzial(podWydzialDoUsuniecia, idoper, idakcept);
                result.Message = result.Result == true ? "Podwydział został przeniesiony do nieaktywnych." : "Próba usunięcia nie powiodła się.";
            }
            else
            {
                result.Message = "Usuwanie nie powiodło się. Podany podwydział nie istnieje w bazie danych.";
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;

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
            List<KatPodWydzialy> pobranePodWydzialy = _PodWydzialDAO.PobierzPodWydzialy(sesja.AktywnaFirma, wydzial);

            return pobranePodWydzialy;
        }
    }
}

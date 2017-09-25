using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;


namespace Eteczka.BE.Services
{
    public class KatDokumentyRodzajService : IKatDokumentyRodzajService
    {
        private KatDokumentyRodzajDAO _KatDokumentyRodzajDAO;

        public  KatDokumentyRodzajService (KatDokumentyRodzajDAO KatDokumentyRodzajDAO)
        {
            this._KatDokumentyRodzajDAO = KatDokumentyRodzajDAO;
        }

    
    
       public List<KatDokumentyRodzaj> PobierzRodzDok()
        {
            List<KatDokumentyRodzaj> PobraneDok = _KatDokumentyRodzajDAO.PobierzWszystkieRodzDok();

            return PobraneDok;
        }
    }
}

using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Services
{
    public class KoszykService : IKoszykService
    {
        private IKoszykDAO _KoszykDao;

        public KoszykService(IKoszykDAO koszykDao)
        {
            this._KoszykDao = koszykDao;
        }

        public int PobierzIloscPlikowWKoszyku(KatLoginyFirmy aktywnaFirma)
        {
            return _KoszykDao.Policz(aktywnaFirma);
        }

        public List<Pliki> PobierzPlikiWKoszyku(KatLoginyFirmy aktywnaFirma)
        {
            return _KoszykDao.PobierzZawartoscKoszyka(aktywnaFirma);
        }

        public bool DodajPlikiDoKoszyka(KatLoginyFirmy aktywnaFirma, List<string> plikiId)
        {
            return _KoszykDao.DodajPlikiDoKoszyka(aktywnaFirma, plikiId);
        }

        public bool UsunZKoszyka(KatLoginyFirmy aktywnaFirma, List<string> plikiId)
        {
            return _KoszykDao.UsunZKoszyka(aktywnaFirma, plikiId);
        }
        public bool WyczyscKoszyk(KatLoginyFirmy aktywnaFirma)
        {
            return _KoszykDao.WyczyscKoszyk(aktywnaFirma);
        }
    }
}

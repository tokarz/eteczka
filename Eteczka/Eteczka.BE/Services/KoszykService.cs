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

        public int PobierzIloscPlikowWKoszyku(string firma, KatLoginyDetale aktywnyUser)
        {
            return _KoszykDao.Policz(firma, aktywnyUser);
        }

        public List<Pliki> PobierzPlikiWKoszyku(string firma, KatLoginyDetale aktywnyUser)
        {
            return _KoszykDao.PobierzZawartoscKoszyka(firma, aktywnyUser);
        }
    }
}

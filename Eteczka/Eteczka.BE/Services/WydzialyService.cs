using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using Eteczka.BE.Mappers;

namespace Eteczka.BE.Services
{
    public class WydzialyService : IWydzialyService
    {
        private KatWydzialDAO _WydzialDao;

        public WydzialyService(KatWydzialDAO wydzialDao)
        {
            this._WydzialDao = wydzialDao;
        }

        public List<KatWydzialy> PobierzWydzialyDlaFirmy(string firma)
        {
            List<KatWydzialy> pobraneWydzialy = _WydzialDao.PobierzDlaFirmy(firma);

            return pobraneWydzialy;
        }
    }
}

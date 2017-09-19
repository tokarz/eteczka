using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Services
{
    public class FirmyService : IFirmyService
    {
        private FirmyDAO _Dao;
        public FirmyService(FirmyDAO firmaDAO)
        {
            this._Dao = firmaDAO;
        }

        public List<KatFirmy> PobierzWszystkie()
        {
            List<KatFirmy> PobraneFirmy = _Dao.PobierzFirmyZBazy();
            return PobraneFirmy;
        }
    }
}

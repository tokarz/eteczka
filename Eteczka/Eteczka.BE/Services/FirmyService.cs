using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public class FirmyService : IFirmyService
    {
        private IFirmyDAO _Dao;
        public FirmyService(IFirmyDAO firmaDAO)
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

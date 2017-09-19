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

namespace Eteczka.BE.Services
{
    public class MiejscePracyService : IMiejscePracyService
    {
        private MiejscePracyDAO _MiejscePracyDao;

        public MiejscePracyService(MiejscePracyDAO miejscePracyDao)
        {
            this._MiejscePracyDao = miejscePracyDao;
        }

        public List<MiejscePracyDlaPracownika> PobierzMiejscaPracyDlaPracownika(Pracownik pracownik)
        {
            List<MiejscePracyDlaPracownika> pobraneMiejscaPracy = _MiejscePracyDao.PobierzMiejscaPracyDlaPracownika(pracownik);

            return pobraneMiejscaPracy;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public class RejonyService : IRejonyService
    {
        private IRejonyDAO _RejonDao;

        public RejonyService(IRejonyDAO rejonDao)
        {
            this._RejonDao = rejonDao;
        }

        public List<KatRejony> PobierzRejony()
        {
            List<KatRejony> pobraneRejony = _RejonDao.PobieraczRejonow();

            return pobraneRejony;
        }
        public List<KatRejony> PobierzRejonyDlaFirmy(SessionDetails sesja)
        {
            List<KatRejony> pobraneRejony = _RejonDao.PobieraczRejonowDlaFirmy(sesja.AktywnaFirma.Firma);
            return pobraneRejony;
        }
    }
}

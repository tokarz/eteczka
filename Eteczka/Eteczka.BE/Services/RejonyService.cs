using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;

namespace Eteczka.BE.Services
{
    public class RejonyService : IRejonyService
    {
        private RejonyDAO _RejonDao;

        public RejonyService(RejonyDAO rejonDao)
        {
            this._RejonDao = rejonDao;
        }

        public List<KatRejony> PobierzRejony()
        {
            List<KatRejony> pobraneRejony = _RejonDao.PobieraczRejonow();

            return pobraneRejony;
        }
        public List<KatRejony> PobierzRejonyDlaFirmy(string firma)
        {
            List<KatRejony> pobraneRejony = _RejonDao.PobieraczRejonowDlaFirmy(firma);
            return pobraneRejony;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public class Konto5Service : IKonto5Service
    {

        private Konto5DAO _konto5DAO;

        public Konto5Service(Konto5DAO konto5DAO)
        {
            this. _konto5DAO = konto5DAO;
        }
        public List<KatKonto5> PobierzKonta5(SessionDetails sesja)
        {
            List<KatKonto5> pobraneKonta5 = _konto5DAO.pobierajKonto5DlaFirmy(sesja.AktywnaFirma);

            return pobraneKonta5;
        }
    }
}

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
    public class PodWydzialService : IPodWydzialService
    {
        private KatPodwydzialDAO _PodWydzialDAO;
        private IPodWydzialDtoMapper _PodWydzialDtoMapper;

        public PodWydzialService(KatPodwydzialDAO PodWydzialDAO, IPodWydzialDtoMapper PodWydzialDtoMapper)
        {
            this._PodWydzialDAO = PodWydzialDAO;
            this._PodWydzialDtoMapper = PodWydzialDtoMapper;
        }

        public List<KatPodWydzialy> PobranaListaPodWydzialow()
        {
            List<KatPodWydzialy> pobranePodWydzialy = _PodWydzialDAO.PobierzPodWydzialy();

            return pobranePodWydzialy;
        }
    }
}

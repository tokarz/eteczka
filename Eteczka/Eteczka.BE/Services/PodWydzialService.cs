using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Services
{

    public class PodWydzialService : IPodWydzialService
    {
        private KatPodwydzialDAO _PodWydzialDAO;
        private IPodWydzialDtoMapper _PodWydzialDtoMapper;

        public PodWydzialService (KatPodwydzialDAO PodWydzialDAO, IPodWydzialDtoMapper PodWydzialDtoMapper)
        {
            this._PodWydzialDAO = PodWydzialDAO;
            this._PodWydzialDtoMapper = PodWydzialDtoMapper;
        }

        public List<PodWydzialDTO> PobranaListaPodWydzialow()
        {
            List<PodWydzialDTO>  PobranePodWydzialyDto = new List<PodWydzialDTO>();
            List<KatPodWydzialy> PobranePodWydzialy = _PodWydzialDAO.PobierzPodWydzialy();
            foreach (KatPodWydzialy podwydzial in PobranePodWydzialy)
            {
                PodWydzialDTO pobranyPodWydzialDto = _PodWydzialDtoMapper.mapuj(podwydzial);
                PobranePodWydzialyDto.Add(pobranyPodWydzialDto);

            }
            return PobranePodWydzialyDto;
        }
    }
}

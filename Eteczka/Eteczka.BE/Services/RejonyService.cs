using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Services
{
    public class RejonyService : IRejonyService
    {
        private RejonyDAO _RejonDao;
        private IRejonDtoMapper _RejonDtoMapper;

        public RejonyService (RejonyDAO rejonDao, IRejonDtoMapper rejonDtoMapper)
        {
            this._RejonDao = rejonDao;
            this._RejonDtoMapper = rejonDtoMapper;
        }

        public List<RejonDTO> PobierzRejony()
        {
            List<RejonDTO> PobraneRejonyDto = new List<RejonDTO>();
            List<KatRejony> PobraneRejony = _RejonDao.PobieraczRejonow();
            foreach (KatRejony pobranyRejon in PobraneRejony)
            {
                RejonDTO pobranyRejonDto = _RejonDtoMapper.mapuj(pobranyRejon);
                PobraneRejonyDto.Add(pobranyRejonDto);

            }
            return PobraneRejonyDto;
        }
        public List<RejonDTO> PobierzRejonyDlaFirmy(string firma)
        {
            List<RejonDTO> PobraneRejonyDTO = new List<RejonDTO>();
            List<KatRejony> PobraneRejony = _RejonDao.PobieraczRejonowDlaFirmy(firma);
            foreach (KatRejony pobranyRejon in PobraneRejony)
            {
                RejonDTO pobranyRejonDto = _RejonDtoMapper.mapuj(pobranyRejon);
                PobraneRejonyDTO.Add(pobranyRejonDto);
            }
            return PobraneRejonyDTO;

        }
    }
}

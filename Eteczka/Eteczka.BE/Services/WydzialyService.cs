using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;

namespace Eteczka.BE.Services
{
    public class WydzialyService : IWydzialyService

    {
        private KatWydzialDAO _WydzialDao;
        private IMapowalnyDoWydzialDto _WydzialMapper;

        public WydzialyService (KatWydzialDAO wydzialDao, IMapowalnyDoWydzialDto wydzialMapper)
        {
            this._WydzialDao = wydzialDao;
            this._WydzialMapper = wydzialMapper;
        }

        public List<WydzialDTO> PobierzWydzialyDlaFirmy(string firma)
        {
            List<WydzialDTO> WydzialyDTO = new List<WydzialDTO>();
            List<KatWydzialy> PobraneWydzialy = _WydzialDao.PobierzDlaFirmy(firma);

            foreach (KatWydzialy wydzial in PobraneWydzialy)
            {
                WydzialDTO wydzialDTO = _WydzialMapper.Mapper(wydzial);
                WydzialyDTO.Add(wydzialDTO);
            }
            return WydzialyDTO;
        }
    }
}

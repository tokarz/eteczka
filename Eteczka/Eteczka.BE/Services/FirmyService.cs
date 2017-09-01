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
    public class FirmyService : IFirmyService
    {
        private FirmyDAO _Dao;
        private KatWydzialDAO _WydzialDao;
        private ImapowalnyDoFirmaDto _Mapper;
        private IMapowalnyDoWydzialDto _WydzialMapper;
        public FirmyService (ImapowalnyDoFirmaDto mapper, FirmyDAO firmaDAO, IMapowalnyDoWydzialDto wydzialMapper, KatWydzialDAO wydzialDao)
        {
            this._Mapper = mapper;
            this._Dao = firmaDAO;
            this._WydzialMapper = wydzialMapper;
            this._WydzialDao = wydzialDao;
        }

        public List<FirmaDTO> PobierzWszystkie()
        {
            List <KatFirmy> PobraneFirmy = _Dao.PobierzFirmyZBazy();
            List<FirmaDTO> FirmyDTO = new List<FirmaDTO>();

            foreach (KatFirmy pobranaFirma in PobraneFirmy)
            {
                FirmaDTO firmaDto = _Mapper.mapuj(pobranaFirma);
                FirmyDTO.Add(firmaDto);
            }
            return FirmyDTO;
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

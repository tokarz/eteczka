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
        private RejonyDAO _RejonDao;
        private RejonDtoMapper _RejonDtoMapper;
        private ImapowalnyDoFirmaDto _Mapper;
        public FirmyService (ImapowalnyDoFirmaDto mapper, FirmyDAO firmaDAO,  RejonyDAO RejonDao, RejonDtoMapper RejonDtoMapper)
        {
            this._Mapper = mapper;
            this._Dao = firmaDAO;
            this._RejonDao = RejonDao;
            this._RejonDtoMapper = RejonDtoMapper;
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

        

        
    }
}

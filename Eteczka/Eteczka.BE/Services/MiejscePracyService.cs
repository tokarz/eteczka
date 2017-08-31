using Eteczka.BE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.DB.Entities;
using Eteczka.BE.Mappers;
using Eteczka.DB.DTO;

namespace Eteczka.BE.Services
{
    public class MiejscePracyService : IMiejscePracyService
    {
        private MiejscePracyDAO _MiejscePracyDao;
        private IMapowalnyDoPracownikDto _PracownikMapper;
        private MiejscePracyDtoMapper _MiejscePracyMapper;

        public MiejscePracyService(MiejscePracyDAO miejscePracyDao, IMapowalnyDoPracownikDto pracownikMapper, MiejscePracyDtoMapper miejscePracyMapper)
        {
            this._MiejscePracyDao = miejscePracyDao;
            this._PracownikMapper = pracownikMapper;
            this._MiejscePracyMapper = miejscePracyMapper;
        }

        public List<MiejscePracyDlaPracownikaDto> PobierzMiejscaPracyDlaPracownika(PracownikDTO pracownik)
        {
            Pracownik mapowanyPracownik = _PracownikMapper.mapuj(pracownik);
            List<MiejscePracyDlaPracownikaDto> pobraneMiejscaPracy = _MiejscePracyDao.PobierzMiejscaPracyDlaPracownika(mapowanyPracownik);

            return pobraneMiejscaPracy;
        }
    }
}

using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using System.Configuration;
using System.Collections.Generic;
using Eteczka.Model.Entities;
using Eteczka.Model.DTO;
using Eteczka.DB.Mappers;
using System.Linq;

namespace Eteczka.BE.Services
{
    public class KatLoginyService : IKatLoginyService
    {
        private KatLoginDAO _Dao;
        private IKatLoginyMapper _Mapper;

        public KatLoginyService(KatLoginDAO dao, IKatLoginyMapper mapper)
        {
            this._Dao = dao;
            this._Mapper = mapper;
        }

        public bool UsunFirmeUzytkownika(KatLoginyFirmy firma)
        {
            return this._Dao.UsunFirmeUzytkownika(firma);
        }

        public KatLoginy GetUserByNameAndPassword(string username, string password)
        {
            KatLoginy queryResult = _Dao.WczytajPracownikaPoNazwieIHasle(username, password);

            return queryResult;
        }

        public List<DaneiDetaleUzytkownika> PobierzDaneUzytkownikow()
        {
            List<DaneiDetaleUzytkownika> wynik = new List<DaneiDetaleUzytkownika>();
            List<KatLoginyDetale> detaleZBazy = _Dao.WczytajWszystkieDetale();
            List<KatLoginyFirmy> firmyZBazy = _Dao.WczytajWszystkieFirmy();

            detaleZBazy.ForEach(detal =>
            {

                DaneiDetaleUzytkownika daneDoDodania = new DaneiDetaleUzytkownika()
                {
                    Detale = detal,
                    Firmy = firmyZBazy.Where(x => x.Identyfikator.Trim() == detal.Identyfikator.Trim()).ToList()
                };

                wynik.Add(daneDoDodania);

            });

            return wynik;
        }

        public List<KatLoginyFirmy> GetUserCompanies(string identyfikator)
        {
            List<KatLoginyFirmy> queryResult = _Dao.WczytajFirmyDlaUzytkownika(identyfikator);

            return queryResult;
        }

        public KatLoginyDetale GetUserDetails(string identyfikator)
        {
            KatLoginyDetale queryResult = _Dao.WczytajDetaleDlaUzytkownika(identyfikator);

            return queryResult;
        }

        public List<KatLoginyDetale> GetAllUsersDetails()
        {
            List<KatLoginyDetale> queryResult = _Dao.WczytajWszystkieDetale();

            return queryResult;
        }

        public bool AktualizujFirmeDlaUzytkownika(KatLoginyFirmy firma)
        {
            return _Dao.AktualizujFirmeDlaUzytkownika(firma);
        }

        public bool DodajFirmeDlaUzytkownika(KatLoginyFirmy firma)
        {
            return _Dao.DodajFirmeDlaUzytkownika(firma);
        }

        public bool DodajNowegoUzytkownika(AddKatLoginyDto user)
        {
            KatLoginy nowyUser = _Mapper.MapujKatLoginy(user);
            KatLoginyDetale detale = _Mapper.MapujKatLoginyDetale(user);

            bool result = _Dao.DodajNowegoPracownika(nowyUser, detale);

            return result;
        }

        public bool ZmienHaslo(AddKatLoginyDto user)
        {
            bool result = _Dao.ZmienHasloUzytkownika(user);

            return result;
        }

        public bool ZmienHasloAdministratora(string shortPassword, string longPassword)
        {
            bool result = _Dao.ZmienHasloAdministratora(shortPassword, longPassword);

            return result;
        }

        public bool UsunUzytkownika(string id)
        {
            bool result = _Dao.UsunUzytkownika(id);

            return result;
        }

        
    }
}

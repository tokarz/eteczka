﻿using Eteczka.BE.DTO;
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

        public bool UsunFirmeUzytkownika(KatLoginy user, string firma)
        {
            return this._Dao.UsunFirmeUzytkownika(user, firma);
        }

        public KatLoginy GetUserByNameAndPassword(string username, string password)
        {
            KatLoginy queryResult = _Dao.WczytajPracownikaPoNazwieIHasle(username, password);

            return queryResult;
        }

        public List<DaneiDetaleUzytkownika> PobierzDaneUzytkownikow()
        {
            List<DaneiDetaleUzytkownika> wynik = new List<DaneiDetaleUzytkownika>();
            List<KatLoginyDetale> daneZBazy = _Dao.WczytajWszystkieDetale();

            daneZBazy.ForEach(detal =>
            {

                DaneUzytkownika dane = new DaneUzytkownika
                {
                    Identyfikator = detal.Identyfikator,
                    Imie = detal.Imie,
                    Nazwisko = detal.Nazwisko,
                    Email = detal.Email,
                    Usuniety = detal.Usuniety
                };

                DaneiDetaleUzytkownika daneDoDodania = new DaneiDetaleUzytkownika()
                {
                    DaneUzytkownika = dane,
                    Detale = new List<KatLoginyDetale>() { detal }
                };

                if (wynik.Contains(daneDoDodania))
                {
                    DaneiDetaleUzytkownika juzDodaneDetale = wynik.Find(el => el.DaneUzytkownika.Identyfikator == dane.Identyfikator);
                    if (juzDodaneDetale != null)
                    {
                        juzDodaneDetale.Detale.Add(detal);
                    }
                }
                else
                {
                    wynik.Add(daneDoDodania);
                }

            });

            return wynik;
        }

        public List<KatLoginyDetale> GetUserDetails(string identyfikator)
        {
            List<KatLoginyDetale> queryResult = _Dao.WczytajDetaleDlaUzytkownika(identyfikator);

            return queryResult;
        }

        public List<KatLoginyDetale> GetAllUsersDetails()
        {
            List<KatLoginyDetale> queryResult = _Dao.WczytajWszystkieDetale();

            return queryResult;
        }

        public bool DodajNowegoUzytkownika(AddKatLoginyDto user)
        {
            KatLoginy nowyUser = _Mapper.MapujKatLoginy(user);
            List<KatLoginyDetale> detale = _Mapper.MapujKatLoginyDetale(user);

            bool result = _Dao.DodajNowegoPracownika(nowyUser, detale);

            return result;
        }

        public bool ZmienHaslo(AddKatLoginyDto user)
        {
            bool result = _Dao.ZmienHasloUzytkownika(user);

            return result;
        }
        public bool UsunUzytkownika(AddKatLoginyDto user)
        {
            bool result = _Dao.UsunUzytkownika(user);

            return result;
        }
    }
}

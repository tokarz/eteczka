using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System.Configuration;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public class UsersService : IUsersService
    {
        private KatLoginDAO _Dao;

        public UsersService(KatLoginDAO dao)
        {
            this._Dao = dao;
        }

        public UserDto GetUserByNameAndPassword(string username, string password)
        {
            UserDto wczytanyUser = null;

            KatLoginy queryResult = _Dao.WczytajPracownikaPoNazwieIHasle(username, password);
            if (queryResult != null)
            {
                wczytanyUser = new UserDto();
                wczytanyUser.Id = queryResult.Id;
                wczytanyUser.Nazwa = queryResult.Identyfikator;
                wczytanyUser.isAdmin = queryResult.isAdmin;

                Uprawnienia uprawnienia = new Uprawnienia();
                uprawnienia.RolaReadOnly = queryResult.Rolareadonly;
                uprawnienia.RolaAddPracownik = queryResult.Rolaaddpracownik;
                uprawnienia.RolaModifyPracownik = queryResult.Rolamodifypracownik;
                uprawnienia.RolaAddFile = queryResult.Rolaaddfile;
                uprawnienia.RolaModifyFile = queryResult.Rolamodifyfile;
                uprawnienia.RolaSlowniki = queryResult.Rolaslowniki;
                uprawnienia.RolaSendEmail = queryResult.Rolasendmail;
                uprawnienia.RolaRaport = queryResult.Rolaraport;
                uprawnienia.RolaRaportExport = queryResult.Rolaraportexport;
                uprawnienia.RolaDoubleAkcept = queryResult.Roladoubleakcept;

                wczytanyUser.Uprawnienia = uprawnienia;
                wczytanyUser.DataModify = queryResult.Datamodify;
                wczytanyUser.FirmaSymbol = queryResult.FirmaSymbol;
            }

            return wczytanyUser;
        }
    }
}

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
        public List<UserDto> GetUserByNameAndPassword(string username, string password)
        {
            List<UserDto> allUsers = null;

            string user = ConfigurationManager.AppSettings["dbuser"];
            string dbPassword = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            KatLoginDAO dao = new KatLoginDAO(new DbConnectionFactory(new ConnectionDetails(user, dbPassword, host, port, name)));
            List<KatLoginy> queryResult = dao.WczytajPracownikaPoNazwieIHasle(username, password);
            if (queryResult != null)
            {
                allUsers = new List<UserDto>();
                foreach (KatLoginy result in queryResult)
                {
                    UserDto wczytanyUser = new UserDto();
                    wczytanyUser.Id = result.Id;
                    wczytanyUser.Nazwa = result.Identyfikator;
                    wczytanyUser.isAdmin = result.isAdmin;

                    Uprawnienia uprawnienia = new Uprawnienia();
                    uprawnienia.RolaReadOnly = result.Rolareadonly;
                    uprawnienia.RolaAddPracownik = result.Rolaaddpracownik;
                    uprawnienia.RolaModifyPracownik = result.Rolamodifypracownik;
                    uprawnienia.RolaAddFile = result.Rolaaddfile;
                    uprawnienia.RolaModifyFile = result.Rolamodifyfile;
                    uprawnienia.RolaSlowniki = result.Rolaslowniki;
                    uprawnienia.RolaSendEmail = result.Rolasendmail;
                    uprawnienia.RolaRaport = result.Rolaraport;
                    uprawnienia.RolaRaportExport = result.Rolaraportexport;
                    uprawnienia.RolaDoubleAkcept = result.Roladoubleakcept;
                    

                    wczytanyUser.Uprawnienia = uprawnienia;
                    wczytanyUser.DataModify = result.Datamodify;
                    wczytanyUser.FirmaSymbol = result.FirmaSymbol;

                    allUsers.Add(wczytanyUser);
                }

            }


            return allUsers;
        }
    }
}

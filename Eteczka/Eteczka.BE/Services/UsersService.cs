using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System.Configuration;

namespace Eteczka.BE.Services
{
    public class UsersService : IUsersService
    {
        public UserDto GetUserByNameAndPassword(string username, string password)
        {
            UserDto wczytanyUser = null;

            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            KatLoginDAO dao = new KatLoginDAO(new DbConnectionFactory(new ConnectionDetails(host, port, name)));
            KatLoginy result = dao.WczytajPracownikaPoNazwieIHasle(username, password);
            if (result != null)
            {
                wczytanyUser = new UserDto();
                wczytanyUser.Id = result.Id;
                wczytanyUser.Nazwa = result.Identyfikator;

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

            }



            return wczytanyUser;
        }
    }
}

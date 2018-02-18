using Eteczka.Model.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public class UprawnieniaMapper : IUprawnieniaMapper
    {
        public Uprawnienia Map(DataRow row)
        {
            Uprawnienia result = new Uprawnienia();
            result.RolaReadOnly = bool.Parse(row["rolareadonly"].ToString());
            result.RolaAddPracownik = bool.Parse(row["RolaAddPracownik".ToLower()].ToString());
            result.RolaModifyPracownik = bool.Parse(row["RolaModifyPracownik".ToLower()].ToString());
            result.RolaAddFile = bool.Parse(row["RolaAddFile".ToLower()].ToString());
            result.RolaModifyFile = bool.Parse(row["RolaModifyFile".ToLower()].ToString());
            result.RolaSlowniki = bool.Parse(row["RolaSlowniki".ToLower()].ToString());
            result.RolaSendEmail = bool.Parse(row["RolaSendMail".ToLower()].ToString());
            result.RolaRaport = bool.Parse(row["RolaRaport".ToLower()].ToString());
            result.RolaRaportExport = bool.Parse(row["RolaRaportExport".ToLower()].ToString());
            result.RolaDoubleAkcept = bool.Parse(row["RolaDoubleAkcept".ToLower()].ToString());

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public interface IKonto5Service
    {
        List<KatKonto5> PobierzKonta5(string firma);
        List<KatKonto5> PobierzAktywneKonta5DlaFirmy(string firma);
        List<KatKonto5> PobierzNieaktywneKonta5DlaFirmy(string firma);
        InsertResult DodajKonto5(KatKonto5 konto, string idoper, string idakcept);
        InsertResult EdytujKonto5(KatKonto5 konto, string idoper, string idakcept);
        InsertResult UsunKonto5(KatKonto5 konto, string idoper, string idakcept);
        InsertResult PrzywrocKonto5(KatKonto5 konto, string idoper, string idakcept);
        List<KatKonto5> WyszukajKonto5(string firma, string search);
    }
   
}

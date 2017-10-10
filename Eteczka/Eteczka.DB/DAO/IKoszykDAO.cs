using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.DAO
{
    public interface IKoszykDAO
    {
        int Policz(string firma, KatLoginyDetale user);
        List<Pliki> PobierzZawartoscKoszyka(string firma, KatLoginyDetale user);
        bool DodajPlikiDoKoszyka(string firma, KatLoginyDetale aktywnyUser, List<string> plikiId);
        bool UsunZKoszyka(string firma, KatLoginyDetale aktywnyUser, List<string> plikiId);
        bool WyczyscKoszyk(string firma, KatLoginyDetale aktywnyUser);
    }
}

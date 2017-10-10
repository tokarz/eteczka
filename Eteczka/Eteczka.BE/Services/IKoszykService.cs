using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Services
{
    public interface IKoszykService
    {
        int PobierzIloscPlikowWKoszyku(string firma, KatLoginyDetale aktywnyUser);
        List<Pliki> PobierzPlikiWKoszyku(string firma, KatLoginyDetale aktywnyUser);
        bool DodajPlikiDoKoszyka(string firma, KatLoginyDetale aktywnyUser, List<string> plikiId);
        bool UsunZKoszyka(string firma, KatLoginyDetale aktywnyUser, List<string> plikiId);
        bool WyczyscKoszyk(string firma, KatLoginyDetale aktywnyUser);
    }
}

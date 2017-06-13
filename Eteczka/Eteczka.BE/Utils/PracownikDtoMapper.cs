using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Utils
{
    public class PracownikDtoMapper : IMapowalnyDoPracownikDto
    {
        public PracownikDTO mapuj(Pracownik zrodlo)
        {
            PracownikDTO result = new PracownikDTO();
            result.Pliki = zrodlo.Pliki;
            result.Id = zrodlo.Id;
            result.Imie = zrodlo.Imie;
            result.Nazwisko = zrodlo.Nazwisko;
            result.PESEL = zrodlo.PESEL;
            result.DataUrodzenia = zrodlo.DataUrodzenia;
            result.NumerPracownika = zrodlo.NumerPracownika;


            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Mappers
{
    public class PracownikDtoMapper : IMapowalnyDoPracownikDto
    {
        public PracownikDTO mapuj(Pracownik zrodlo)
        {
            PracownikDTO result = new PracownikDTO();
            result.Imie = zrodlo.Imie;
            result.Nazwisko = zrodlo.Nazwisko;
            result.PESEL = zrodlo.PESEL;
            result.Numeread = zrodlo.Numeread;
            result.NazwiskoRodowe = zrodlo.NazwiskoRodowe;
            result.ImieMatki = zrodlo.ImieMatki;
            result.ImieOjca = zrodlo.ImieOjca;

            result.PeselInny = zrodlo.PeselInny;
            result.IdOper = zrodlo.IdOper;
            result.IdAkcept = zrodlo.IdAkcept;
            result.DataModify = zrodlo.DataModify;
            result.DataAkcept = zrodlo.DataAkcept;
            result.DataUrodzenia = zrodlo.DataUrodzenia;
            result.Imie2 = zrodlo.Imie2;

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Mappers
{
    public class FirmaDtoMapper : ImapowalnyDoFirmaDto
    {
        public FirmaDTO mapuj(KatFirmy zrodlo)
        {
            FirmaDTO firma = new FirmaDTO();

            firma.Firma = zrodlo.Firma;
            firma.Nazwa = zrodlo.Nazwa;
            firma.Nazwaskrocona = zrodlo.Nazwaskrocona;
            firma.Ulica = zrodlo.Ulica;
            firma.Numerdomu = zrodlo.Numerdomu;
            firma.Numerlokalu = zrodlo.Numerlokalu;
            firma.Miasto = zrodlo.Miasto;
            firma.Kodpocztowy = zrodlo.Kodpocztowy;
            firma.Poczta = zrodlo.Poczta;
            firma.Gmina = zrodlo.Gmina;
            firma.Powiat = zrodlo.Gmina;
            firma.Wojewodztwo = zrodlo.Wojewodztwo;
            firma.Nip = zrodlo.Nip;
            firma.Regon = zrodlo.Regon;
            firma.Nazwa2 = zrodlo.Nazwa2;
            firma.Pesel = zrodlo.Pesel;
            firma.Idoper = zrodlo.Idoper;
            firma.Idakcept = zrodlo.Idakcept;
            firma.Nazwisko = zrodlo.Nazwisko;
            firma.Imie = zrodlo.Imie;
            firma.Datamodify = zrodlo.Datamodify;
            firma.Dataakcept = zrodlo.Dataakcept;
            firma.Systembazowy = zrodlo.Systembazowy;
            firma.Usuniety = zrodlo.Usuniety;
            firma.Waitingroom = zrodlo.Waitingroom;

            return firma;
        }
        
    

    }
}

﻿using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.DAO
{
    public interface IPracownikDAO
    {
        bool ImportujPracownikow(List<Pracownik> pracownicy);
        List<Pracownik> PobierzPozostalychPracownikow(string firma, string orderby = "nazwisko,imie", bool asc = true);
        List<Pracownik> PobierzZatrudnionychPracownikow(string firma, string orderby = "nazwisko,imie", bool asc = true);
        List<Pracownik> PobierzPracownikow(string firma, string limit = "*", string offset = "0", string orderby = "nazwisko,imie", bool asc = true);
        Pracownik PobierzPracownikaPoId(string numeread);
        List<Pracownik> WyszukiwaczPracownikow(string search, string firma);
        List<Pracownik> WyszukiwaczPracownikowPoTekscie(string search, string firma, int limit = 100, string orderby = "nazwisko", bool asc = true);
        int PoliczPracownikowWBazie();
        List<Pracownik> WyszukiwaczZatrPracownikowPoTekscie(string search, string firma, int limit = 500, string orderby = "nazwisko", bool asc = true);
        List<Pracownik> WyszukiwaczPozostZatrPracownikowPoTekscie(string search, string firma, int limit = 500, string orderby = "nazwisko", bool asc = true);
    }
}
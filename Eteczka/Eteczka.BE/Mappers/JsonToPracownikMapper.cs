using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Mappers
{
    public class JsonToPracownikMapper : IJsonToPracownikMapper
    {
        public Pracownik Map(JToken pracownik)
        {
            Pracownik wczytanyPracownik = new Pracownik();
            wczytanyPracownik.Imie = pracownik["imie"].ToString();

            wczytanyPracownik.Nazwisko = pracownik["nazwisko"].ToString();
            wczytanyPracownik.PESEL = pracownik["pesel"].ToString();
            wczytanyPracownik.Numeread = pracownik["numeread"].ToString();
            wczytanyPracownik.Kraj = pracownik["kraj"].ToString();
            wczytanyPracownik.NazwiskoRodowe = pracownik["nazwiskorodowe"].ToString();
            wczytanyPracownik.ImieMatki = pracownik["imiematki"].ToString();
            wczytanyPracownik.ImieOjca = pracownik["imieojca"].ToString();
            wczytanyPracownik.PeselInny = pracownik["peselinny"].ToString();
            wczytanyPracownik.IdOper = pracownik["idoper"].ToString();
            wczytanyPracownik.IdAkcept = pracownik["idakcept"].ToString();
            wczytanyPracownik.DataModify = DateTime.Parse(pracownik["datamodify"].ToString());
            wczytanyPracownik.DataAkcept = DateTime.Parse(pracownik["dataakcept"].ToString());
            wczytanyPracownik.DataUrodzenia = pracownik["dataurodzenia"].ToString();
            wczytanyPracownik.Imie2 = pracownik["imie2"].ToString();

            return wczytanyPracownik;
        }

    }
}

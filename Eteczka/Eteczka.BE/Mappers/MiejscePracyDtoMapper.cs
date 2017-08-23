using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Mappers
{
    public class MiejscePracyDtoMapper
    {
        public MiejscePracyDTO mapuj (MiejscePracy zrodlo)
        {
            MiejscePracyDTO miejsce = new MiejscePracyDTO();

            miejsce.Firma = zrodlo.Firma;
            miejsce.Rejon = zrodlo.Rejon;
            miejsce.Wydzial = zrodlo.Wydzial;
            miejsce.Podwydzial = zrodlo.Podwydzial;
            miejsce.Konto5 = zrodlo.Konto5;
            miejsce.DataPocz = zrodlo.DataPocz;
            miejsce.DataKoniec = zrodlo.DataKoniec;
            miejsce.IdOper = zrodlo.IdOper;
            miejsce.IdAkcept = zrodlo.IdAkcept;
            miejsce.DataModify = zrodlo.DataModify;
            miejsce.DataAkcept = zrodlo.DataAkcept;
            miejsce.NumerEad = zrodlo.NumerEad;
            miejsce.SystemBazowy = zrodlo.SystemBazowy;
            miejsce.Usuniety = zrodlo.Usuniety;


            return null;
        }

    }
}

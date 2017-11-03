using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Mappers
{
    public class PodWydzialDtoMapper : IPodWydzialDtoMapper
    {
        public PodWydzialDTO Mapuj(KatPodWydzialy zrodlo)
        {
            PodWydzialDTO podwydzial = new PodWydzialDTO();

            podwydzial.Podwydzial = zrodlo.Podwydzial;
            podwydzial.Nazwa = zrodlo.Nazwa;
            podwydzial.Datamodify = zrodlo.Datamodify;
            podwydzial.Idoper = zrodlo.Idoper;
            podwydzial.Idakcept = zrodlo.Idakcept;
            podwydzial.Dataakcept = zrodlo.Dataakcept;
            podwydzial.Firma = zrodlo.Firma;
            podwydzial.SystemBazowy = zrodlo.SystemBazowy;
            podwydzial.Usuniety = zrodlo.Usuniety;
            podwydzial.Wydzial = zrodlo.Wydzial;

            return podwydzial;
        }
    }
}

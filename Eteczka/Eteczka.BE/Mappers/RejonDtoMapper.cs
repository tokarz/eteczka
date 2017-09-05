using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Mappers
{
   public class RejonDtoMapper : IRejonDtoMapper
    {
        public RejonDTO mapuj(KatRejony zrodlo)
        {
            RejonDTO rejon = new RejonDTO();
            rejon.Rejon = zrodlo.Rejon;
            rejon.Nazwa = zrodlo.Nazwa;
            rejon.Idoper = zrodlo.Idoper;
            rejon.Idakcept = zrodlo.Idakcept;
            rejon.Firma = zrodlo.Firma;
            rejon.Datamodify = zrodlo.Datamodify;
            rejon.Dataakcept = zrodlo.Dataakcept;
            rejon.Mnemonik = zrodlo.Mnemonik;
            rejon.Systembazowy = zrodlo.Systembazowy;
            rejon.Usuniety = zrodlo.Usuniety;

            return rejon; 
        }
    }
}

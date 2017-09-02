using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Mappers
{
    public class MapowalnyDoWydzialDto : IMapowalnyDoWydzialDto
    {
        public WydzialDTO Mapper(KatWydzialy zrodlo)
        {
            WydzialDTO dzialDTO = new WydzialDTO();

            dzialDTO.Wydzial = zrodlo.Wydzial;
            dzialDTO.Nazwa = zrodlo.Nazwa;
            dzialDTO.Datamodify = zrodlo.Datamodify;
            dzialDTO.Idoper = zrodlo.Idoper;
            dzialDTO.Idakcept = zrodlo.Idakcept;
            dzialDTO.Dataakcept = zrodlo.Dataakcept;
            dzialDTO.Firma = zrodlo.Firma;
            dzialDTO.Systembazowy = zrodlo.Systembazowy;
            dzialDTO.Usuniety = zrodlo.Usuniety;

            return dzialDTO;
        }

    }
}

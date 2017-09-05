using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using Eteczka.BE.DTO;

namespace Eteczka.BE.Mappers
{
    public interface IRejonDtoMapper
    {
        RejonDTO mapuj(KatRejony zrodlo);
    }
}

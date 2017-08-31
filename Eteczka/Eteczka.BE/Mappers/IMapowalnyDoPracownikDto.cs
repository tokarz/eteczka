using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;

namespace Eteczka.BE.Mappers
{
    public interface IMapowalnyDoPracownikDto
    {
        PracownikDTO mapuj(Pracownik zrodlo);
        Pracownik mapuj(PracownikDTO zrodlo);
    }
}

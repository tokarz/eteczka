using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System.Collections.Generic;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public interface IKatLoginyMapper
    {
        KatLoginy Map(DataTable result);
        List<KatLoginy> MapList(DataTable result);
        List<KatLoginyDetale> MapDetails(DataTable result);
        KatLoginy MapujKatLoginy(AddKatLoginyDto user);
        List<KatLoginyDetale> MapujKatLoginyDetale(AddKatLoginyDto user);
    }
}

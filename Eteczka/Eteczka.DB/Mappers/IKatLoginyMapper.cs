using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Mappers
{
    public interface IKatLoginyMapper
    {
        KatLoginy Map(DataTable result);
        List<KatLoginyDetale> MapDetails(DataTable result);
    }
}

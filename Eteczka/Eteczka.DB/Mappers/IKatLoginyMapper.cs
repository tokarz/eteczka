using Eteczka.DB.Entities;
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
        KatLoginyDetale MapDetails(DataTable result);
    }
}

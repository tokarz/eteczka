using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public interface IKatKonto5Mapper
    {
        KatKonto5 MapujZSql(DataRow result);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public interface IKatWydzialMapper
    {
        KatWydzialy MapujzSql(DataRow row);
    }
}

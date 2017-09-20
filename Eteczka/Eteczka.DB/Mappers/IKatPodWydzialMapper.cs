using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Eteczka.Model.Entities;

namespace Eteczka.DB.Mappers
{
    public interface IKatPodWydzialMapper
    {
        KatPodWydzialy MapujZSql(DataRow row);
    }
}

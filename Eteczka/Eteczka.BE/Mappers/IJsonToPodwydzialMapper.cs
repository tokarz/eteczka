using Eteczka.DB.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToPodwydzialMapper
    {
        KatPodWydzialy Map(JToken token);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public interface IPodWydzialService
    {
        List<KatPodWydzialy> PobranaListaPodWydzialow(SessionDetails sesja, string wydzial); 
    }
}

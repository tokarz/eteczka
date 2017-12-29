using Eteczka.BE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Services
{
    public interface IRaportyPdfService
    {
        bool SkorowidzTeczkiPracownika(SessionDetails sesja, string numeread);
        
    }
}

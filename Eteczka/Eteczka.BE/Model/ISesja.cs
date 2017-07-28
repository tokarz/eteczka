using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Model
{
    public interface ISesja
    {
        StanSesji PobierzStanSesji(string sessionID);
        void UtworzLubAktualizujSesje(string sessionID);
        void ZamknijSesje(string sessionID);
    }
}

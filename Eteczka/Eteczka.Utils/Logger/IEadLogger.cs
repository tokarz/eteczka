using Eteczka.BE.Model;
using Eteczka.Utils.Common;
using Eteczka.Utils.Common.DTO;

namespace Eteczka.Utils.Logger
{
    public interface IEadLogger
    {
        void LOG(PoziomLogowania poziom, Akcja akcja, string wiadomosc, bool sucess = false, SessionDetails sesja = null , string numerEad = "");
        void LOG_ZMIANY_W_TABELACH(PoziomLogowania poziom, Akcja akcja, SessionDetails sesja, bool sucess, string NazwaTabeli, object TabelaPo, object TabelaPrzed = null, string message = null);
        void LOG_EMAIL_SENDING(EmailLog emailLog);
    }
}

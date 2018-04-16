
namespace Eteczka.BE.Model
{
    public interface ISesja
    {
        StanSesji PobierzStanSesji(string sessionID);
        void UtworzLubAktualizujSesje(string sessionID);
        void ZamknijSesje(string sessionID);
    }
}

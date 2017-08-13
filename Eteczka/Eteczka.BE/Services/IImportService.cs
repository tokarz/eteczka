using Eteczka.BE.DTO;

namespace Eteczka.BE.Services
{
    public interface IImportService
    {
        ImportResult ImportFiles(bool nadpisz);
        ImportResult ImportKatLokalPapier(bool nadpisz);
        ImportResult ImportFirms(bool nadpisz);
        ImportResult ImportAreas(bool nadpisz);
        ImportResult ImportujPracownikow(string sessionId);
    }
}

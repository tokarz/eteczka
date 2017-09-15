﻿using Eteczka.BE.DTO;

namespace Eteczka.BE.Services
{
    public interface IImportService
    {
        ImportResult ImportFiles(bool nadpisz);
        ImportResult ImportKatLokalPapier(bool nadpisz);
        ImportResult ImportFirms(bool nadpisz);
        ImportResult ImportAreas(bool nadpisz);
        ImportResult ImportujPracownikow(string sessionId);
        ImportResult CheckImportStatus(string type);
        ImportResult ImportWorkplaces(string sessionId);
        ImportResult ImportSubDepartments(string sessionId);
        ImportResult ImportDepartments(string sessionId);
        ImportResult ImportAccounts5(string sessionId);
        bool CreateSourceFolder(string folder);
        ImportResult WczytajDokZExcela(bool nadpisz);
        bool DoesFolderExist(string folder);
    }
}

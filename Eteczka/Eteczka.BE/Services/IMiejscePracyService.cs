using Eteczka.Model.DTO;
using System.Collections.Generic;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public interface IMiejscePracyService
    {
        List<MiejscePracyDlaPracownika> PobierzMiejscaPracyDlaPracownika(string numerEad, string firma);
        InsertResult DodajMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoDodania);
        InsertResult EdytujMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoEdycji);
        InsertResult UsunMiejscePracy(SessionDetails sesja, MiejscePracy miejsceDoUsuniecia);
    }
}

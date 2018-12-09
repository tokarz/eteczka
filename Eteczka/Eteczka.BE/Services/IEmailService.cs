using Eteczka.BE.DTO;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Services
{
    public interface IEmailService
    {
        void SendAdminQuestion(string sessionId, AdminQuestion email);
        bool WyslijPlikiMailem(SessionDetails sesja, DaneEmail email);
    }
}

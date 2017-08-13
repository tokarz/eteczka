using Eteczka.BE.DTO;
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
    }
}

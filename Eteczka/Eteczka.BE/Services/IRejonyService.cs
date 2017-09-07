using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;


namespace Eteczka.BE.Services
{
    public interface IRejonyService
    {
        List<RejonDTO> PobierzRejony();
        List<RejonDTO> PobierzRejonyDlaFirmy(string firma);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;

namespace Eteczka.DB.Mappers
{
    public interface IKatRodzajeDokumentowExcelMapper
    {
        List <KatDokumentyRodzaj> PobierzRodzajeDokZExcela (string sciezka);
    }
}

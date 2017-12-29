using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Utils
{
    public interface IPdfUtils
    {
        bool SavePdf(List<string> PlikiDoSpakowania, string temp);
        bool GenerujIZapiszRaportPdf(Document doc, string nazwaRaportu);
    }
}

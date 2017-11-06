using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Eteczka.Model.Entities;

namespace Eteczka.DB.Mappers
{
    public class KatRodzajeDokumentowMapper : IKatRodzajeDokumentowMapper
    {
        public KatDokumentyRodzaj MapujZSql(DataRow row)
        {
            KatDokumentyRodzaj fetchedDokument = new KatDokumentyRodzaj();

            fetchedDokument.Symbol = row[0].ToString();
            fetchedDokument.Nazwa = row[1].ToString();
            fetchedDokument.Dokwlasny = bool.Parse(row[2].ToString());
            fetchedDokument.Jrwa = row[3].ToString();
            fetchedDokument.Teczkadzial = row[4].ToString();
            fetchedDokument.Typedycji = row[5].ToString();
            fetchedDokument.Idoper = row[6].ToString();
            fetchedDokument.Idakcept = row[7].ToString();
            fetchedDokument.Datamodify = DateTime.Parse(row[8].ToString());
            fetchedDokument.Dataakcept = DateTime.Parse(row[9].ToString());
            fetchedDokument.SystemBazowy = row[10].ToString();
            fetchedDokument.Usuniety = bool.Parse(row [11].ToString());
            int confidential = -1;
            if (int.TryParse(row[12].ToString(), out confidential))
            {
                fetchedDokument.Confidential = confidential;
            }
            fetchedDokument.SymbolEad = row["symbolead"].ToString();
            fetchedDokument.Audyt = bool.Parse(row["audyt"].ToString());

            return fetchedDokument;

                

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;


namespace Eteczka.DB.Mappers
{
    public class KatRodzajeDokumentowExcelMapper : IKatRodzajeDokumentowExcelMapper
    {
        public List<KatDokumentyRodzaj> PobierzRodzajeDokZExcela (string plik)
        {

            List<KatDokumentyRodzaj> PobraneRodzajeDok = new List<KatDokumentyRodzaj>();

            
            if (File.Exists(plik))
            {
                Application xlApp = null;
                Workbook xlWorkbook = null;
                Worksheet xlWorksheet = null;
                Range xlRange = null;
                try
                {
                    xlApp = new Application();
                    xlWorkbook = xlApp.Workbooks.Open(Path.GetFullPath(plik));
                    xlWorksheet = xlWorkbook.Sheets[1];
                    xlRange = xlWorksheet.UsedRange;


                    for (int y = 1; y <= xlRange.Rows.Count; y++)
                    {
                        KatDokumentyRodzaj pobranyDokument = new KatDokumentyRodzaj();
                        pobranyDokument.Symbol = (xlRange.Cells[y, 1].Value);
                        pobranyDokument.Nazwa = (xlRange.Cells[y, 2].Value);
                        pobranyDokument.Teczkadzial = (xlRange.Cells[y, 3].Value);
                        pobranyDokument.Typedycji = (xlRange.Cells[y, 4].Value);
                        pobranyDokument.SystemBazowy = (xlRange.Cells[y, 5].Value);

                        PobraneRodzajeDok.Add(pobranyDokument);
                    }
                }

                catch (Exception ex)
                {
                    //
                }
                finally
                {
                    ZamknijPlik(xlApp, xlWorkbook, xlWorksheet, xlRange);
                }

            }

            
            return PobraneRodzajeDok;
        }
        private void ZamknijPlik(Application xlApp, Workbook xlWorkbook, Worksheet xlWorksheet, Range xlRange)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (xlApp != null && xlWorkbook != null && xlWorksheet != null && xlRange != null)
            {
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
        }
    }
}

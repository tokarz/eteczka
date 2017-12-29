using System;
using System.Collections.Generic;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using System.IO;
using System.Diagnostics;
using Eteczka.BE.Utils;
using MigraDoc.Rendering;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public class RaportyPdfService : IRaportyPdfService
    {
        private PlikiDAO _PlikiDAO;
        private IDirectoryWrapper _Wrapper;
        private PracownikDAO _PracownikDAO;

        public RaportyPdfService(PlikiDAO plikiDAO, IDirectoryWrapper wrapper, PracownikDAO pracownikDAO)
        {
            this._PlikiDAO = plikiDAO;
            this._Wrapper = wrapper;
            this._PracownikDAO = pracownikDAO;
        }

        public bool SkorowidzTeczkiPracownika(SessionDetails sesja, string numeread)
        {
            bool result = true;

            List<Pliki> Dokumenty = _PlikiDAO.PobierzPlikPoNumerzeEad(numeread, sesja.AktywnaFirma, "nrdokumentu asc", "teczkadzial asc, ");
            Pracownik pracownik = _PracownikDAO.PobierzPracownikaPoId(numeread);

            //dodać null checka!
            
            List<Pliki> DokumentyA = Dokumenty.FindAll((xx) => { return xx.TeczkaDzial.Contains("A"); });
            List<Pliki> DokumentyB = Dokumenty.FindAll((xx) => { return xx.TeczkaDzial.Contains("B"); });
            List<Pliki> DokumentyC = Dokumenty.FindAll((xx) => { return xx.TeczkaDzial.Contains("C"); });

            Document doc = new Document();

                Section sec = doc.AddSection();
                sec.PageSetup.TopMargin = 40;
                sec.PageSetup.LeftMargin = 40;
                sec.PageSetup.RightMargin = 40;
                Paragraph paragraph = sec.AddParagraph();

                //stopka:
                paragraph = sec.Footers.Primary.AddParagraph("Teczka pracownika " + sesja.AktywnaFirma.Trim() + ": " + pracownik.Imie + " " + pracownik.Nazwisko + ", ur. " + pracownik.DataUrodzenia   + ". Wygenerowano: " + sesja.AktywnyUser.Identyfikator.Trim()  + ", " + DateTime.Now.ToShortDateString() + ":" + DateTime.Now.ToLongTimeString() + ". Program: eAD");
                paragraph.Format.Font.Size = 7;

                //Tytuł dokumentu:
                paragraph = sec.AddParagraph();
                paragraph.Format.Font.Bold = true;
                paragraph.Format.Font.Size = 14;
                paragraph.Format.Alignment = ParagraphAlignment.Center;
                paragraph.AddFormattedText("Skorowidz teczki akt osobowych");

                paragraph = sec.AddParagraph();
                paragraph = sec.AddParagraph();

                // Podsumowanie teczki
                paragraph = sec.AddParagraph("Pracownik: " + pracownik.Imie + " " + pracownik.Nazwisko + ", data urodzenia: " + pracownik.DataUrodzenia + " r.");
                paragraph = sec.AddParagraph("Miejsce pracy: " + sesja.AktywnaFirma);
                paragraph = sec.AddParagraph();
                paragraph = sec.AddParagraph();
                paragraph = sec.AddParagraph("Liczba dokumentów w dziale A: " + DokumentyA.Count);
                paragraph = sec.AddParagraph("Liczba dokumentów w dziale B: " + DokumentyB.Count);
                paragraph = sec.AddParagraph("Liczba dokumentów w dziale C: " + DokumentyC.Count);
                paragraph = sec.AddParagraph();

                paragraph = sec.AddParagraph("Łączna dokumentów w teczce: " + Dokumenty.Count);
                paragraph = sec.AddParagraph();

            //Tworzymy i definiujemy tabelę:
            if (Dokumenty.Count !=0)
            {
                Table table = new Table();
                table.Borders.Width = 0.5;
                table.Rows.LeftIndent = 2;

                //Dodajemy i formatujemy kolumny:
                Column column = table.AddColumn(Unit.FromCentimeter(1.5));
                column.Format.Alignment = ParagraphAlignment.Center;

                column = table.AddColumn(Unit.FromCentimeter(1.2));
                column.Format.Alignment = ParagraphAlignment.Center;

                column = table.AddColumn(Unit.FromCentimeter(5));
                column.Format.Alignment = ParagraphAlignment.Center;

                column = table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Center;

                column = table.AddColumn(Unit.FromCentimeter(6.5));
                column.Format.Alignment = ParagraphAlignment.Center;

                //Definiujemy nagłówek tabeli:
                Row row = table.AddRow();
                row.HeadingFormat = true; //Powtarzanie nagłówka na każdej nowej stronie.

                Cell cell = row.Cells[0];
                cell.AddParagraph("Dział teczki");
                cell.Format.Font.Bold = true;

                cell = row.Cells[1];
                cell.AddParagraph("Nr dok.");
                cell.Format.Font.Bold = true;

                cell = row.Cells[2];
                cell.AddParagraph("Rodzaj dokumentu");
                cell.Format.Font.Bold = true;

                cell = row.Cells[3];
                cell.AddParagraph("Data dodania do systemu");
                cell.Format.Font.Bold = true;

                cell = row.Cells[4];
                cell.AddParagraph("Uwagi");
                cell.Format.Font.Bold = true;

                foreach (Pliki dokument in Dokumenty)
                {
                    row = table.AddRow();

                    cell = row.Cells[0];
                    cell.AddParagraph(dokument.TeczkaDzial);
                    cell.Format.Alignment = ParagraphAlignment.Center;

                    cell = row.Cells[1];
                    cell.AddParagraph(dokument.NrDokumentu.ToString());
                    cell.Format.Alignment = ParagraphAlignment.Center;

                    cell = row.Cells[2];
                    cell.AddParagraph(dokument.Symbol);
                    cell.Format.Alignment = ParagraphAlignment.Left;

                    cell = row.Cells[3];
                    cell.AddParagraph(dokument.DataAkcept.ToString("yyyy-MM-dd"));
                    cell.Format.Alignment = ParagraphAlignment.Center;

                    cell = row.Cells[4];
                    cell.AddParagraph(dokument.OpisDodatkowy);
                    cell.Format.Alignment = ParagraphAlignment.Left;
                }
                doc.LastSection.Add(table);
            }
            else
            {
                paragraph = sec.AddParagraph("Teczka pracownika nie zawiera dokumentów.");
                paragraph.Format.Font.Bold = true;
            }
            
            try
            {
                result = true;
                //Generujemy PDF:
                PdfDocumentRenderer docRend = new PdfDocumentRenderer(true);
                docRend.Document = doc;
                docRend.RenderDocument();
                //Zapis pliku
                string eadRoot = _Wrapper.GetEnvironmentVariable("EAD_DIR");
                string raportyPdfFolder = Path.Combine(eadRoot, "RaportyPdf\\");
                if (!_Wrapper.CzyKatalogIstnieje(raportyPdfFolder))
                {
                    _Wrapper.UtworzKatalog(raportyPdfFolder);
                }
                string nazwaPliku = raportyPdfFolder + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_Skorowidz_teczki.pdf";
                docRend.PdfDocument.Save(nazwaPliku);

                //Uruchamianie podglądu pliku
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = nazwaPliku;
                Process.Start(processInfo);

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        


    }     
        }
 
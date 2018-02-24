using Eteczka.BE.Model;
using Eteczka.BE.Utils;
using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.IO;

namespace Eteczka.BE.Services
{
    public class RaportyExcellService : IRaportyExcellService
    {

        private PlikiDAO _PlikiDAO;
        private IDirectoryWrapper _Wrapper;
        private PracownikDAO _PracownikDAO;
        private IExcellUtils _ExcellUtils;

        public RaportyExcellService(PlikiDAO plikiDao, IDirectoryWrapper wrapper, PracownikDAO PracownikDAO, IExcellUtils excellUtils)
        {
            this._PlikiDAO = plikiDao;
            this._Wrapper = wrapper;
            this._PracownikDAO = PracownikDAO;
            this._ExcellUtils = excellUtils;
        }

        public bool SkorowidzTeczkiExcellPelny(SessionDetails sesja, string numeread)
        {
            List<Pliki> Dokumenty = _PlikiDAO.PobierzPlikPoNumerzeEad(numeread, sesja.AktywnaFirma.Firma, "nrdokumentu asc", "\"Pliki\".teczkadzial asc, ");
            Pracownik pracownik = _PracownikDAO.PobierzPracownikaPoId(numeread);

            bool result = false;

            string sciezka = _ExcellUtils.WygenerujSciezkeZapisuExcell("Pełny skorowidz teczki", sesja.AktywnaFirma.Identyfikator.Trim());
            try
            {
                result = true;
                using (FileStream stream = new FileStream(sciezka, FileMode.Create, FileAccess.Write))
                {
                    //Tworzymy dokument z arkuszem
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet arkusz = workbook.CreateSheet("Skorowidz teczki");


                    //Definiujemy styl tytułu dokumentu: 
                    ICellStyle stylTytulu = workbook.CreateCellStyle();
                    IFont czcionkaTytulu = workbook.CreateFont();
                    czcionkaTytulu.FontHeightInPoints = 14;
                    czcionkaTytulu.FontName = "Calibri";
                    czcionkaTytulu.Boldweight = (short)700;
                    stylTytulu.SetFont(czcionkaTytulu);

                    //Definiujemy styl nagłówka tabeli:
                    ICellStyle stylNaglowka = workbook.CreateCellStyle();
                    IFont czcionkaNaglowka = workbook.CreateFont();
                    czcionkaNaglowka.FontHeightInPoints = 12;
                    czcionkaNaglowka.FontName = "Calibri";
                    czcionkaNaglowka.Boldweight = (short)700;
                    stylNaglowka.SetFont(czcionkaNaglowka);

                    //Definiujemy styl zawartości tabeli:
                    ICellStyle glownyStyl = workbook.CreateCellStyle();
                    glownyStyl.Alignment = HorizontalAlignment.Left;
                    IHeaderFooter stopka = arkusz.Footer;
                    stopka.Left = (HSSFFooter.FontSize((short)9) + "Teczka pracownika " + sesja.AktywnaFirma.Firma.Trim() + ": " + pracownik.Imie + " " + pracownik.Nazwisko + ", ur. " + pracownik.DataUrodzenia + ". Wygenerowano: " + sesja.AktywnaFirma.Identyfikator.Trim() + ", " + DateTime.Now.ToShortDateString() + ":" + DateTime.Now.ToLongTimeString() + ". Program: eAD");
                   
                    IRow rzad = arkusz.CreateRow(0);
                    ICell komorka = rzad.CreateCell(0);
                    komorka.SetCellValue("Skorowidz teczki akt osobowych");
                    arkusz.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));
                    komorka.CellStyle = stylTytulu;

                    rzad = arkusz.CreateRow(2);
                    komorka = rzad.CreateCell(0);
                    komorka.SetCellValue("Pracownik: " + pracownik.Imie + " " + pracownik.Nazwisko + ", data urodzenia: " + pracownik.DataUrodzenia + " r.");
                    arkusz.AddMergedRegion(new CellRangeAddress(2, 2, 0, 5));

                    rzad = arkusz.CreateRow(3);
                    komorka = rzad.CreateCell(0);
                    komorka.SetCellValue("Miejsce pracy: " + sesja.AktywnaFirma.Firma);
                    arkusz.AddMergedRegion(new CellRangeAddress(3, 3, 0, 5));

                    rzad = arkusz.CreateRow(4);
                    komorka = rzad.CreateCell(0);
                    komorka.SetCellValue("Data wygenerowania raportu: " + DateTime.Now.ToString("dd-MM-yyyy") + "r.");
                    arkusz.AddMergedRegion(new CellRangeAddress(4, 4, 0, 5));

                    //Wiersz z nagłówkiem:
                    rzad = arkusz.CreateRow(6);
                    arkusz.SetAutoFilter(new CellRangeAddress(6, 6, 0, 10));

                    komorka = rzad.CreateCell(0);
                    komorka.SetCellValue("Dział teczki");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(1);
                    komorka.SetCellValue("Nr dokumentu");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(2);
                    komorka.SetCellValue("Rodzaj dokumentu");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(3);
                    komorka.SetCellValue("Opis dodatkowy");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(4);
                    komorka.SetCellValue("Data dokumentu");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(5);
                    komorka.SetCellValue("Data pocz");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(6);
                    komorka.SetCellValue("Data koniec");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(7);
                    komorka.SetCellValue("Data dodania");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(8);
                    komorka.SetCellValue("Dodany przez");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(9);
                    komorka.SetCellValue("Nazwa pliku");
                    komorka.CellStyle = stylNaglowka;

                    komorka = rzad.CreateCell(10);
                    komorka.SetCellValue("Dokument własny");
                    komorka.CellStyle = stylNaglowka;

                    //Wiersze z właściwymi danymi:
                    int row = 7;
                    foreach (Pliki dokument in Dokumenty)

                    {
                        rzad = arkusz.CreateRow(row);

                        komorka = rzad.CreateCell(0);
                        komorka.SetCellValue(dokument.TeczkaDzial.ToString().Trim());
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(1);
                        komorka.SetCellValue(dokument.NrDokumentu);
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(2);
                        komorka.SetCellValue(dokument.Symbol.ToString().Trim());
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(3);
                        komorka.SetCellValue(dokument.OpisDodatkowy.ToString().Trim());
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(4);
                        komorka.SetCellValue(dokument.DataDokumentu.ToString("yyyy-MM-dd"));
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(5);
                        komorka.SetCellValue(dokument.DataPocz.ToString("yyyy-MM-dd"));
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(6);
                        komorka.SetCellValue(dokument.DataKoniec.ToString("yyyy-MM-dd"));
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(7);
                        komorka.SetCellValue(dokument.DataDodania.ToString("yyyy-MM-dd"));
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(8);
                        komorka.SetCellValue(dokument.IdOper.ToString().Trim());
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(9);
                        komorka.SetCellValue(dokument.NazwaEad.ToString().Trim());
                        komorka.CellStyle = glownyStyl;

                        komorka = rzad.CreateCell(10);
                        komorka.SetCellValue(dokument.DokumentWlasny.ToString());
                        komorka.CellStyle = glownyStyl;

                        row++;
                    }

                    arkusz.AutoSizeColumn(0);//Autodopasowanie szerokości kolumny
                    arkusz.AutoSizeColumn(1);
                    arkusz.AutoSizeColumn(2);
                    arkusz.AutoSizeColumn(3);
                    arkusz.AutoSizeColumn(4);
                    arkusz.AutoSizeColumn(5);
                    arkusz.AutoSizeColumn(6);
                    arkusz.AutoSizeColumn(7);
                    arkusz.AutoSizeColumn(8);
                    arkusz.AutoSizeColumn(9);
                    arkusz.AutoSizeColumn(10);


                    workbook.Write(stream);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
                //logi
            }
                return result;
            }
        }
    }



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Security;
using PdfSharp.Pdf.IO;
using System.Net.Mail;
using NLog;
using Eteczka.BE.Model;
using System.Configuration;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.Utils.Common;
using Eteczka.Utils.Logger;
using System.Globalization;

namespace Eteczka.BE.Utils
{
    public class PlikiUtils
    {
        private IDirectoryWrapper _Wrapper;
        private IPdfUtils _PdfUtils;
        private IZipUtils _ZipUtils;
        private PlikiDAO _PlikiDAO;
        IEadLogger LOGER = LoggerFactory.GetLogger();

        Logger LOGGER = LogManager.GetLogger("default");

        public PlikiUtils(IDirectoryWrapper wrapper, IPdfUtils pdfUtils, IZipUtils zipUtils, PlikiDAO PlikiDAO)
        {
            this._Wrapper = wrapper;
            this._PdfUtils = pdfUtils;
            this._ZipUtils = zipUtils;
            this._PlikiDAO = PlikiDAO;
        }
        //Przykladowe dzialanie metody:
        //Uruchomiona z argumentem c:/katalog/plik.txt
        //Powinna zwrocic plik.txt
        //Jesli sciezka nie ma na koncu zadnego pliku (np c:\costam) powinna zwrocic pusty string ""

        //Przydatne podpowiedzi:
        //Metody : string.split, string lastIndexOf
        //Testy sa juz napisane i swieca sie na czerwono!:)

        public string WezNazweFolderuZeSciezki(string sciezka)
        {
            int lastSlash = sciezka.LastIndexOf("/");
            if (lastSlash == -1)
            {
                lastSlash = sciezka.LastIndexOf("\\");
            }

            int ostatniSlash = (lastSlash + 1);


            string nazwaFolderu = (sciezka.Substring(ostatniSlash));


            return nazwaFolderu;
        }

        public List<string> WezNazweFolderowZeSciezek(List<string> sciezki)
        {

            List<string> znalezioneNazwyFolderow = new List<string>();
            if (sciezki != null)
            {
                foreach (string sciezka in sciezki)
                {
                    znalezioneNazwyFolderow.Add(this.WezNazweFolderuZeSciezki(sciezka));
                }
            }


            return znalezioneNazwyFolderow;
        }

        public string WezNazwePlikuZeSciezki(string sciezka)
        {
            int lastSlash = sciezka.LastIndexOf("/");
            if (lastSlash == -1)
            {
                lastSlash = sciezka.LastIndexOf("\\");
            }

            int ostatniSlash = (lastSlash + 1);


            string nazwaPliku = (sciezka.Substring(ostatniSlash));

            int bezPliku = nazwaPliku.LastIndexOf(".");
            if (bezPliku == -1)
            {
                nazwaPliku = "";
            }


            return nazwaPliku;
        }

        public List<string> WezNazwePlikowZeSciezek(List<string> sciezki)
        {
            //Tu stworzyles Liste na wyniki ale dales jej ta sama nazwe co parametr funkcji - "sciezki"
            //Od razu wiec przesloniles sobie Liste ktora dostales
            //List<string> sciezki = new List<string>();
            List<string> znalezioneNazwyPlikow = new List<string>();

            foreach (string sciezka in sciezki)
            {
                int ostatniSlash = sciezka.LastIndexOf("/") + 1;
                if (ostatniSlash == 0)
                {
                    ostatniSlash = sciezka.LastIndexOf("\\") + 1;
                }
                string nazwaPliku = sciezka.Substring(ostatniSlash);
                int bezPliku = nazwaPliku.LastIndexOf(".");
                //Do tego miejsca algorytm jest dobry, robi to co ma, czyli wyciaga nazwe pliku ze sciezki
                if (bezPliku != -1)
                {
                    //tu wchodzimy tylko wtedy gdy plik ma w nazwie kropke.
                    //Ale w tym przypadku nie chcemy zwrocic 1 znajezionej nazwy pliku piszac:
                    //return nazwaPliku
                    //ale tym razem chcemy dodac znaleziony plik do Listy wynikow a nie zwrocic tylko jeden:)
                    //Metoda Add dodaje nazwe pliku do listy
                    znalezioneNazwyPlikow.Add(nazwaPliku);
                }
            }

            //dopiero kiedy petla sie skonczy, chcemy zwrocic wyniki
            return znalezioneNazwyPlikow;
        }

        //Metoda dostaje argument : liste plikow, i zwraca tylko te, ktore maja podane rozszerzenie (zip, txt, i tak dalej)
        public List<string> WezPlikiZRozszerzeniem(List<string> sciezki, string rozszerzenie)
        {
            List<string> PlikiZRozszerzeniem = new List<string>();
            foreach (string sciezka in sciezki)
            {
                int ostatniSlash = sciezka.LastIndexOf("/");
                if (ostatniSlash == -1)
                {
                    ostatniSlash = sciezka.LastIndexOf("\\");
                }
                string nazwaPlikuZRozszerzeniem = sciezka.Substring(ostatniSlash + 1);
                //int kropka = nazwaPlikuZRozszerzeniem.LastIndexOf(".");
                int kropka = sciezka.LastIndexOf(".");
                string rozszerzeniePliku = sciezka.Substring(kropka + 1).ToLower();
                string rozszerzenieZParametru = rozszerzenie.Trim().ToLower();  //to podpowiedź Paszcza :))
                if (kropka != -1)  //Tutaj zabezpieczam się przed zwróceniem ścieżki np. D:/dane/pdf
                {
                    //if (rozszerzenieZParametru. == rozszerzeniePliku)
                    if (rozszerzenieZParametru.Equals(rozszerzeniePliku))  //Czy zamiast "==" mogę użyć metody Equals?
                    {
                        PlikiZRozszerzeniem.Add(nazwaPlikuZRozszerzeniem);
                    }
                }

            }
            return PlikiZRozszerzeniem;
        }


        //Ta Metoda ma dostac 2 listy pelnych sciezek A (d:\a\b.txt) i B(c:/costam) i zwrocic liste
        // Tych PLIKOW (samych ich nazw) ktore powtarzaja sie w jednej i drugiej liscie

        public List<string> WezSpolneElementy(List<string> plikiA, List<string> plikiB)
        {
            List<string> WspolneNazwyPlikow = new List<string>();

            List<string> NazwyPlikowA = WezNazwePlikowZeSciezek(plikiA);
            List<string> NazwyPlikowB = WezNazwePlikowZeSciezek(plikiB);
            var porownaniePlikow = NazwyPlikowA.Intersect(NazwyPlikowB);
            // Tę zmienna Visual pozwolił zadeklarować tylko jako var - czy dlatego, żeby metoda była uniwersalna dla różnych typów danych?
            foreach (var pliki in porownaniePlikow)
            {
                WspolneNazwyPlikow.Add(pliki.ToString());
            }

            return WspolneNazwyPlikow;
        }

        public Dictionary<string, int> PoliczRozszerzenia(List<string> pliki)
        {
            //Przygotowujemy sobie pusty slownik.
            Dictionary<string, int> rozszerzeniaPlikow = new Dictionary<string, int>();

            //Zaczynamy analizowac liste wejsciowa sciezka po sciezce
            foreach (string sciezkaPliku in pliki)
            {
                //Korzystajac z napisanej przez Ciebie metory wyciagamy sama nazwe pliku ze sciezki
                string nazwaPliku = WezNazwePlikuZeSciezki(sciezkaPliku);

                //Teraz wyciagamy nazwe rozszerzenia z pliku (txt, zip, rar, itd)
                string aktualneRozszerzenie = nazwaPliku.Substring(nazwaPliku.LastIndexOf(".") + 1);

                //Sprawdzamy, czy aktualnie sprawdzane rozszerzenie bylo juz kiedys znalezione

                if (rozszerzeniaPlikow.ContainsKey(aktualneRozszerzenie))
                {
                    //Jesli rozszerzenie bylo juz przez nas wczesniej analizowane, musimy wyciagnac poprzednia wartosc licznika
                    //i zwiekszyc go o 1. Potem nadpisac stara wartosc i umiescic w slowniku aktualna:
                    ////  rozszerzeniaPlikow["txt"] = 2
                    int iloscPlikow = rozszerzeniaPlikow[aktualneRozszerzenie];
                    iloscPlikow = iloscPlikow + 1;
                    rozszerzeniaPlikow[aktualneRozszerzenie] = iloscPlikow;
                }
                else
                {
                    //Jelsi nie - wrzucamy do slownika pod haslem rozszerzenie - wartosc startowa - 1
                    //Czyli teraz wyglada to tak :
                    //  rozszerzeniaPlikow["txt"] = 1

                    rozszerzeniaPlikow.Add(aktualneRozszerzenie, 1);
                }

            }

            //Co do zapisu : rozszerzeniaPlikow[aktualneRozszerzenie];
            //Moze to byc mylace na poczatku. Klamry [] odnosza sie to tablicy. Jezeli masz typ List
            //To element lista[0], lista[1] odnosza sie do kolejnych indeksow elementow
            //Jezeli masz typ Slownika (Dictionary) to nie masz indeksow, tylko klucze
            //np stolice("Polska") = "Warszawa";
            //np stolice("Wlochy") = "Rzym";
            //I piszac rozszerzeniaPlikow[aktualneRozszerzenie] wyciagamy wartosc dla klucza, ktorym jest rozszerzenie

            return rozszerzeniaPlikow;
        }

        public Dictionary<string, List<string>> PoliczPlikiWKatalogach(List<string> sciezkiPlikow)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            foreach (string sciezka in sciezkiPlikow)
            {
                string nazwaPliku = WezNazwePlikuZeSciezki(sciezka);
                int lastSlash = sciezka.LastIndexOf("/");
                if (lastSlash == -1)
                {
                    lastSlash = sciezka.LastIndexOf("\\");
                }
                string nazwaSciezki = sciezka.Substring(0, lastSlash + 1);

                if (result.ContainsKey(nazwaSciezki))
                {
                    List<string> Pliki = result[nazwaSciezki];
                    Pliki.Add(nazwaPliku);
                    result[nazwaSciezki] = Pliki;
                }

                else
                {
                    List<string> Pliki = new List<string>();
                    Pliki.Add(nazwaPliku);
                    result.Add(nazwaSciezki, Pliki);
                }

            }


            return result;
        }

        public List<string> ZnajdzPlikiPoNazwie(string nazwa, List<string> sciezkiPlikow)
        {
            List<string> SciezkiZPlikami = new List<string>();
            foreach (string sciezka in sciezkiPlikow)
            {

                string nazwaPliku = WezNazwePlikuZeSciezki(sciezka);
                if (nazwaPliku.Contains(nazwa))
                {
                    SciezkiZPlikami.Add(sciezka);
                }

            }

            return SciezkiZPlikami;
        }



        public List<string> WczytajPlikiZFolderu(string sciezkaDoKatalogu, string rozszerzenie)
        {
            List<string> osoby = new List<string>();

            List<string> pliki = this._Wrapper.PobierzPlikiZKatalogu(sciezkaDoKatalogu);
            foreach (string plik in pliki)
            {
                string zawartoscPliku = this._Wrapper.WczytajPlik(plik, rozszerzenie);

                if (plik.EndsWith(rozszerzenie))
                {
                    osoby.Add(zawartoscPliku);
                }
            }

            return osoby;
        }

        public List<String> ExcellWczytajWiersz(string sciezka, int arkusz, int wiersz)
        {
            List<String> result = new List<string>();
            if (File.Exists(sciezka))
            {
                LOGGER.Debug("Wczytanie Typow pliki: " + sciezka);
                Application xlApp = new Application();
                // Path.GetFullPath(sciezka) trzeba wywolac we wrapperze
                Workbook xlWorkbook = xlApp.Workbooks.Open(Path.GetFullPath(sciezka));
                Worksheet xlWorksheet = xlWorkbook.Sheets[arkusz];
                Range xlRange = xlWorksheet.UsedRange;

                for (int i = 1; i <= xlRange.Columns.Count; i++)
                {
                    result.Add(xlRange.Cells[wiersz, i].Value);
                }

                ZamknijPlik(xlApp, xlWorkbook, xlWorksheet, xlRange);
            }


            return result;
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

        public ExcelKatDok ExcellWczytajKatDok(string sciezka, int arkusz)
        {
            ExcelKatDok result = new ExcelKatDok();

            if (File.Exists(sciezka))
            {
                LOGGER.Debug("Wczytanie Typow pliki: " + sciezka);
                Application xlApp = null;
                Workbook xlWorkbook = null;
                Worksheet xlWorksheet = null;
                Range xlRange = null;
                try
                {
                    xlApp = new Application();
                    xlWorkbook = xlApp.Workbooks.Open(Path.GetFullPath(sciezka));
                    xlWorksheet = xlWorkbook.Sheets[arkusz];
                    xlRange = xlWorksheet.UsedRange;

                    result.Naglowek = new List<string>();
                    result.CalyPlik = new List<ExcelKatDokPola>();
                    for (int i = 1; i <= xlRange.Columns.Count; i++)
                    {
                        result.Naglowek.Add(xlRange.Cells[1, i].Value);
                    }

                    for (int y = 1; y <= xlRange.Rows.Count; y++)
                    {
                        ExcelKatDokPola Pola = new ExcelKatDokPola();
                        Pola.Symbol = (xlRange.Cells[y, 1].Value);
                        Pola.Nazwa = (xlRange.Cells[y, 2].Value);
                        Pola.TeczkaDzial = (xlRange.Cells[y, 3].Value);
                        Pola.TypEdycji = (xlRange.Cells[y, 4].Value);
                        Pola.SystemBazowy = (xlRange.Cells[y, 5].Value);

                        result.CalyPlik.Add(Pola);
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("BŁĄD ODCZYTU PLIKU!");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    ZamknijPlik(xlApp, xlWorkbook, xlWorksheet, xlRange);
                }

            }

            return result;
        }


        public ExcelKatDok ExcellWczytajKatDok_ZDAJETEST_DLA_MALEJ_POMOCY(string sciezka, int arkusz)
        {
            ExcelKatDok result = new ExcelKatDok();
            result.Naglowek = new List<string>();


            if (File.Exists(sciezka))
            {
                Application xlApp = new Application();
                Workbook xlWorkbook = xlApp.Workbooks.Open(Path.GetFullPath(sciezka));
                Worksheet xlWorksheet = xlWorkbook.Sheets[arkusz];
                Range xlRange = xlWorksheet.UsedRange;
                for (int i = 1; i <= xlRange.Columns.Count; i++)
                {
                    result.Naglowek.Add(xlRange.Cells[1, i].Value);
                    //Do wczytania nagłówka rozważałem użycie metody ExcellWczytajWiersz, 
                    //ale mówiłeś, że otwieranie pliku to kosztowna impreza,
                    //więc zdecydowałem się na przepisanie kodu w ramach jednego otwarcia.

                }

                result.CalyPlik = new List<ExcelKatDokPola>();
                for (int y = 1; y <= xlRange.Rows.Count; y++)
                {
                    ExcelKatDokPola Pola = new ExcelKatDokPola();

                    Pola.Symbol = (xlRange.Cells[y, 1].Value);
                    Pola.Nazwa = (xlRange.Cells[y, 2].Value);
                    Pola.TeczkaDzial = (xlRange.Cells[y, 3].Value);
                    Pola.TypEdycji = (xlRange.Cells[y, 4].Value);
                    Pola.SystemBazowy = (xlRange.Cells[y, 5].Value);


                    result.CalyPlik.Add(Pola);
                }

                ZamknijPlik(xlApp, xlWorkbook, xlWorksheet, xlRange);
            }


            return result;
        }

        public string PobierzZaszyfrowanaZawartoscPliku(string sciezka, string sessionId)
        {
            LOGGER.Info("Plik [" + sciezka + "] Wczytany przez [" + Sesja.PobierzStanSesji().PobierzSesje(sessionId).AktywnaFirma.Identyfikator + "] o [" + DateTime.Now + "]");

            string result = "";
            try
            {
                // 
                PdfDocument document = PdfReader.Open(sciezka, "#Blk8dr#Blk8dr#");
                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream, true);
                    byte[] filedata = stream.ToArray();
                    result = Convert.ToBase64String(filedata, 0, filedata.Length);
                }

            }
            catch (Exception ex)
            {
                result = "ERROR";
            }

            return result;
        }

        public bool ZmienHasloPlikow(List<string> files, string oldPassword, string newPassword)
        {
            bool result = false;

            foreach (string file in files)
            {
                try
                {
                    PdfDocument document = PdfReader.Open(file, oldPassword);

                    PdfSecuritySettings securitySettings = document.SecuritySettings;

                    // Setting one of the passwords automatically sets the security level to 
                    // PdfDocumentSecurityLevel.Encrypted128Bit.
                    securitySettings.UserPassword = newPassword;
                    securitySettings.OwnerPassword = "#Blk8dr#Blk8dr#";

                    // Don't use 40 bit encryption unless needed for compatibility reasons
                    //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

                    // Restrict some rights.
                    securitySettings.PermitAccessibilityExtractContent = false;
                    securitySettings.PermitAnnotations = false;
                    securitySettings.PermitAssembleDocument = false;
                    securitySettings.PermitExtractContent = false;
                    securitySettings.PermitFormsFill = true;
                    securitySettings.PermitFullQualityPrint = false;
                    securitySettings.PermitModifyDocument = true;
                    securitySettings.PermitPrint = false;

                    // Save the document...
                    document.Save(file);
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }

        public bool ZaszyfrujIPrzeniesPlikPdf(string file)
        {
            bool result = false;
            try
            {
                PdfDocument document = PdfReader.Open(file);

                PdfSecuritySettings securitySettings = document.SecuritySettings;

                // Setting one of the passwords automatically sets the security level to 
                // PdfDocumentSecurityLevel.Encrypted128Bit.

                securitySettings.UserPassword = "#Blk8dr#Blk8dr#";
                securitySettings.OwnerPassword = "#Blk8dr#Blk8dr#";

                // Don't use 40 bit encryption unless needed for compatibility reasons
                //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

                // Restrict some rights.
                securitySettings.PermitAccessibilityExtractContent = false;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = false;
                securitySettings.PermitFormsFill = true;
                securitySettings.PermitFullQualityPrint = false;
                securitySettings.PermitModifyDocument = true;
                securitySettings.PermitPrint = false;

                // Save the document...
                document.Save(file);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public string SpakujPliki(string firma, List<string> plikiDoSpakowania, string haslo)
        {
            string eadRoot = ConfigurationManager.AppSettings["rootdir"];
            string dataFormat = "yyyyMMddHHmmssfff";

            string tempZrodloKatalog = Path.Combine(eadRoot, DateTime.Now.ToString(dataFormat) + "tempsource");
            _Wrapper.UtworzKatalog(tempZrodloKatalog);
            //Directory.CreateDirectory(tempZrodloKatalog);

            string tempZipKatalog = Path.Combine(eadRoot, DateTime.Now.ToString(dataFormat) + "tempzip");
            _Wrapper.UtworzKatalog(tempZipKatalog);
            //Directory.CreateDirectory(tempZipKatalog);

            string tempZipSaveSciezka = tempZipKatalog + "\\" + DateTime.Now.ToString(dataFormat) + ".zip";
            string archiwumZipFolder = Path.Combine(eadRoot, "ArchiwumZip\\", firma);

            if (!_Wrapper.CzyKatalogIstnieje(archiwumZipFolder))
            //if (!Directory.Exists)
            {
                _Wrapper.UtworzKatalog(archiwumZipFolder);
                //Directory.CreateDirectory(archiwumZipFolder);
            }
            string sciezkaDoZipa = (eadRoot + "\\ArchiwumZip\\" + firma + "\\" + DateTime.Now.ToString(dataFormat) + ".zip");

            try
            {
                _PdfUtils.SavePdf(plikiDoSpakowania, tempZrodloKatalog);

                List<string> listaPlikow = _Wrapper.PobierzPlikiZKatalogu(tempZrodloKatalog);
                //List<string> ListaPlikow = Directory.GetFiles(tempZrodloKatalog).ToList();

                _ZipUtils.SpakujPlikiZHaslem(listaPlikow, haslo, tempZipSaveSciezka, sciezkaDoZipa);

                _Wrapper.UsunKatalog(tempZipKatalog, true);
                //Directory.Delete(tempZipKatalog, true);

                _Wrapper.UsunKatalog(tempZrodloKatalog, true);
                //Directory.Delete(tempZrodloKatalog, true);
            }
            catch (Exception ex)
            {
                // logi
            }

            return sciezkaDoZipa;
        }


        public bool WyslijPlikiMailem(string firma, string user, string adresaci, string adresaciCc, List<string> Zalaczniki, string hasloDoZip, string temat, string wiadomosc)
        {
            bool result = false;

            MailMessage mail = new MailMessage();
            SmtpClient Client = new SmtpClient();
            try
            {
                //TODO: Szyfrowanie/odszyfrowywanie hasła baza-aplikacja.
                SerwerSmtp daneKonfiguracyjneSerwera = _PlikiDAO.PobierzKonfiguracjeSerwera("smtp-topfarms.ogicom.pl");
     
                Client.Port = daneKonfiguracyjneSerwera.SmtpPort;
                Client.Host = daneKonfiguracyjneSerwera.SmtpSerwer;
                Client.EnableSsl = true;
                Client.Timeout = 10000;
                Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Client.UseDefaultCredentials = false;
                Client.Credentials = new System.Net.NetworkCredential(daneKonfiguracyjneSerwera.MailUsername, daneKonfiguracyjneSerwera.MailPassword);

                mail.From = new MailAddress(daneKonfiguracyjneSerwera.MailSender);

                string[] listaAdresatowMaila = adresaci.Replace(" ", "").Split(',');
                foreach (string adresEmail in listaAdresatowMaila)
                {
                    if (new Osys().ProstyWalidatorMaila(adresEmail))
                    {
                        mail.To.Add(new MailAddress(adresEmail));
                    }
                }

                if (adresaciCc != null)
                {
                    string[] listaAdresatowMailaCc = adresaciCc.Replace(" ", "").Split(',');
                    foreach (string adresEmailCc in listaAdresatowMailaCc)
                    {
                        if (new Osys().ProstyWalidatorMaila(adresEmailCc))
                        {
                            mail.CC.Add(new MailAddress(adresEmailCc));
                        }
                    }
                }

                mail.Subject = temat ?? daneKonfiguracyjneSerwera.MailSubject;
                mail.Body = wiadomosc ?? daneKonfiguracyjneSerwera.MailBody;

                if (Zalaczniki != null)
                {
                    string zalacznik = SpakujPliki(firma.Trim(), Zalaczniki, hasloDoZip);
                    Attachment attachment = new System.Net.Mail.Attachment(zalacznik);
                    if (File.Exists(zalacznik))
                    {
                        mail.Attachments.Add(attachment);
                    }
                }

                Client.Send(mail);

                LOGER.LOG_EMAIL_SENDING(new EmailLog()
                {
                    CzasWiadomosci = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    Firma = firma,
                    UserId = user,
                    Adresaci = adresaci,
                    AdresaciCc = adresaciCc ?? "",
                    Temat = temat ?? "",
                    Tresc = wiadomosc ?? "",
                    Zalaczniki = this.StworzStringListaZalacznikow(Zalaczniki),
                    Status = "Próba wysłania wiadomości."

                });
                result = true;
            }
            catch (Exception ex)
            {
                result = false;  
            }
            finally
            {
                Client.Dispose();
                mail.Dispose();
            }
          
            LOGER.LOG_EMAIL_SENDING(new EmailLog()
            {
                CzasWiadomosci = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                Firma = firma,
                UserId = user,
                Adresaci = adresaci,
                AdresaciCc = adresaciCc ?? "",
                Temat = temat ?? "",
                Tresc = wiadomosc ?? "",
                Zalaczniki = this.StworzStringListaZalacznikow(Zalaczniki),
                Status = result == true ? "Wiadomość wysłana." : "Wiadomośc nie została wysłana."
            });
            return result;

        }
        public string StworzSciezkeZListy(List<string> Lista)
        {
            StringBuilder s = new StringBuilder();
            string sciezka = "";
            if (Lista != null)
            {
                foreach (string l in Lista)
                {
                    s.Append("\\" + l);
                    sciezka = s.ToString().Substring(1);
                }
            }
            return sciezka;
        }

        public string StworzStringListaZalacznikow(List<string>Zalaczniki)
        {
            StringBuilder s = new StringBuilder();
            string listaZalacznikow = "";

            if (Zalaczniki != null)
            {
                foreach (string x in Zalaczniki)
                {
                    listaZalacznikow = s.Append(", " + x.Substring(x.LastIndexOf("\\") + 1).Trim()).ToString().Substring(1);
                }
            }
            return listaZalacznikow;
        }
    }
}



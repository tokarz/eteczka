using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using Eteczka.BE.DTO;
using System.Text;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Services
{
    public class ImportService : IImportService
    {
        public ImportResult ImportFiles(bool nadpisz)
        {
            ImportResult result = new ImportResult();
            Dictionary<string, Plik> wczytanePliki = new Dictionary<string, Plik>();
            List<string> znalezionePdf = new List<string>();
            List<string> znalezioneMetaDane = new List<string>();

            PlikiUtils plikiUtils = new PlikiUtils();


            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "pliki");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            foreach (string plik in pliki)
            {
                if (plik.EndsWith(".json"))
                {
                    znalezioneMetaDane.Add(plik);
                }
                else
                {
                    znalezionePdf.Add(plik);
                }
            }

            foreach (string metaDanePliku in znalezioneMetaDane)
            {
                string metaDaneBezRozszerzenia = metaDanePliku.Substring(0, metaDanePliku.Length - 5);
                foreach (string dokument in znalezionePdf)
                {
                    string dokumentBezRozszerzenia = dokument.Substring(0, dokument.Length - 4);
                    if (metaDaneBezRozszerzenia.Equals(dokumentBezRozszerzenia))
                    {
                        Plik metaDaneJson = new Plik();
                        StringBuilder jsonMetadataFile = new StringBuilder();

                        using (StreamReader reader = new StreamReader(metaDanePliku))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                jsonMetadataFile.Append(line);
                            }


                            var rootJson = JObject.Parse(jsonMetadataFile.ToString());
                            var parsedJson = rootJson["file"];
                            metaDaneJson.Id = parsedJson["id"].ToString();
                            metaDaneJson.Jrwa = parsedJson["jrwa"].ToString();
                            metaDaneJson.Nazwa = parsedJson["name"].ToString() + "." + parsedJson["ext"].ToString();
                            metaDaneJson.TypDokumentu = parsedJson["type"].ToString();
                            metaDaneJson.DataUtworzenia = DateTime.Parse(parsedJson["createdate"].ToString());
                            metaDaneJson.DataModyfikacji = DateTime.Parse(parsedJson["lastchangedate"].ToString());
                            metaDaneJson.Komentarz = parsedJson["comment"].ToString();

                        }


                        wczytanePliki.Add(dokument, metaDaneJson);
                    }
                }
            }

            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IDbConnectionFactory factory = new DbConnectionFactory(connectionDetails);

            PlikiDAO dao = new PlikiDAO(factory);
            result.ImportSukces = dao.ImportujPliki(wczytanePliki);



            return result;
        }
    }
}

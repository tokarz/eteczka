namespace Eteczka.Utils.Common.DTO
{
    // format json
    public class Log
    {
        public string CzasWiadomosci { get; set; }
        //public string Id { get; set; }
        public string UserId { get; set; }
        //public string NumerEad { get; set; }
        public string Firma { get; set; }
        public string Wiadomosc { get; set; }
        public Akcja Akcja { get; set; }
        //public string InformacjeDodatkowe { get; set; }
        public bool Sucess { get; set; }
        public string System { get; set; }
       


        public override string ToString()
        {
          return ($"\"ActionTime\" : \" {CzasWiadomosci}\",  \"UserId\" : \"{UserId}\",  \"Company\" : \"{Firma}\", \"Action\" : \"{Akcja.ToString()}\", \"TableName\" : \"\", \"Changes\" : \"\", \"TableBefore\" : \"\", \"Sucess\" : {Sucess.ToString().ToLower()}, \"Message\" : \"{Wiadomosc}\", \"System\" : \"EAD\";");  
        }
    }
}

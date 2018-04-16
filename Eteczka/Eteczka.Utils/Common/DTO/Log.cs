namespace Eteczka.Utils.Common.DTO
{
    // format json
    public class Log
    {
        public string CzasWiadomosci { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string NumerEad { get; set; }
        public string Firma { get; set; }
        public string Wiadomosc { get; set; }
        public Akcja Akcja { get; set; }
        public string InformacjeDodatkowe { get; set; }

        public override string ToString()
        {
            return string.Format(@"Czas: {0}, Id: {1}, UserId: {2}, NumerEad: {3}, Firma:{4}, Wiadomosc: {5}, Akcja: {6}, Dodatkowe: {7}",
                CzasWiadomosci,
                Id,
                UserId,
                NumerEad,
                Firma,
                Wiadomosc,
                Akcja.ToString(),
                InformacjeDodatkowe);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Entities
{
    public class KatTeczki
    {
        public string Id { get; set; }
        public string Nazwa { get; set; }
        public string PelnaSciezka { get; set; }
        public string Jrwa { get; set; }
        public string JrwaId { get; set; }
        public DateTime DataUtworzenia { get; set; }
        public DateTime DataModyfikacji { get; set; }
        public string DataUtworzeniaText
        {
            get
            {
                return this.DataUtworzenia.ToString();
            }
        }
        public string DataModyfikacjiText
        {
            get
            {
                return this.DataModyfikacji.ToString();
            }
        }
        public string TypDokumentu { get; set; }
        public string Pesel { get; set; }
    }
}

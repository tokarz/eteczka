using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class MetaDanePliku
    {
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string ModificationDateText
        {
            get
            {
                return this.ModificationDate.ToString();
            }
            
        }
        public string CreationDateText
        {
            get
            {
                return this.CreationDate.ToString();
            }

        }
        public string Size { get; set; }
        public string PhysicalLocation { get; set; }
        public string Type { get; set; }
        public string Jrwa { get; set; }
    }
}

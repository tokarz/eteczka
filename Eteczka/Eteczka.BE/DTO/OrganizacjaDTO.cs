﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class OrganizacjaDTO
    {
        public string Name { get; set; }
        public List<PlikJrwaDTO> Substruktury { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.DTO
{
    public class AdminQuestion
    {
        public string Topic { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Remarks { get; set; }
    }
}

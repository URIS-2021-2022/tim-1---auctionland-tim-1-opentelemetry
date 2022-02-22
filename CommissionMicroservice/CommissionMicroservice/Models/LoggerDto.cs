﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    public class LoggerDto
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Service { get; set; }
        public string HttpMethodName { get; set; }
        public string Response { get; set; }
    }
}

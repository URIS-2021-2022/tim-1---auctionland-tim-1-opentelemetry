﻿using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.ServiceCalls
{
    public interface IPublicBiddingMicroservice
    {
        public bool CreateLog(LoggerDto loggerDto);
    }
}


using LoggerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.ServiceCalls
{
    public interface ILoggerMicroservice
    {
        public bool CreateLog(LoggerDto loggerDto);
    }
}

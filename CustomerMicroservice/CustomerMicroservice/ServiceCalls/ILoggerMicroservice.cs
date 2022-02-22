using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.ServiceCalls
{
    public interface ILoggerMicroservice
    {
        public bool CreateLog(LoggerDto loggerDto);
    }
}

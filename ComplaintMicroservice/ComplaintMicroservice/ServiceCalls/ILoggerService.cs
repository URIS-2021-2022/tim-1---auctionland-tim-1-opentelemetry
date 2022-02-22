using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.ServiceCalls
{
    public interface ILoggerService
    {
        public bool CreateLog(LoggerDto loggerDto);
    }
}

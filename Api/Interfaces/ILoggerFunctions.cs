using Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Interfaces
{
    public interface ILoggerFunctions
    {
        void Report(LogMessage message);
    }
}

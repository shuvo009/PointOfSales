using System.Collections.Generic;
using whostpos.Core.Helpers;

namespace whostpos.Core.Interface
{
    public interface IPrinter
    {
        void Printer<T>(ReportType reportType, T printingData);
    }
}

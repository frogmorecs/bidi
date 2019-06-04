using System;
using IBidiSpl;
// ReSharper disable SuspiciousTypeConversion.Global

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var bidiRequest = Activator.CreateInstance<BidiRequest>();
            var iBidiRequest = (IBidiRequest) bidiRequest;

            iBidiRequest.SetSchema("\\Printer");
        }
    }
}

using System;
using System.Runtime.InteropServices;
using Bidi;
using Bidi.Com;
using CommandLine;

// ReSharper disable SuspiciousTypeConversion.Global

namespace Test
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Parser
                .Default
                .ParseArguments<GetOptions, GetAllOptions, SetOptions, GetWithArgumentOptions, EnumSchemaOptions>(args)
                .WithParsed<GetOptions>(options => GetRequest(options, RequestType.Get))
                .WithParsed<GetAllOptions>(options => GetRequest(options, RequestType.GetAll))
                .WithParsed<SetOptions>(SetRequest)
                .WithParsed<GetWithArgumentOptions>(GetWithArgumentRequest)
                .WithParsed<EnumSchemaOptions>(EnumSchemaRequest);
        }

        private static void GetRequest(Options options, RequestType requestType)
        {
            throw new NotImplementedException();
        }


        private static void EnumSchemaRequest(EnumSchemaOptions options)
        {
            using (var connection = new PrinterConnection())
            {
                var access = options.Admin ? BIDI_ACCESS.BIDI_ACCESS_ADMINISTRATOR : BIDI_ACCESS.BIDI_ACCESS_USER;
                connection.Bind(options.PrinterName, access);

                var result = connection.SendRequest("\\Printer", RequestType.EnumSchema);
                foreach (var datum in result)
                {
                    Console.WriteLine(datum.Value);
                }
            }
        }

        private static void GetWithArgumentRequest(GetWithArgumentOptions obj)
        {
            throw new NotImplementedException(nameof(GetWithArgumentRequest));
        }

        private static void SetRequest(SetOptions options)
        {
            throw new NotImplementedException(nameof(SetRequest));
        }
    }
}

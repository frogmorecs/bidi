using System;
using System.Runtime.InteropServices;
using CommandLine;
using IBidiSpl;
// ReSharper disable SuspiciousTypeConversion.Global

namespace Test
{

    public class Options
    {
        [Option('p', "printername", Required = true, HelpText = "Name of the printer to query.")]
        public string PrinterName { get; set; }

        [Option('s', "schema", Required = true, HelpText = "Schema to request from the printer.")]
        public string Schema { get; set; }

        [Option('a', "admin", Required = false, HelpText = "Request Administrator access.")]
        public bool Admin { get; set; } = false;
    }
    class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Parser
                .Default
                .ParseArguments<Options>(args)
                .WithParsed(QueryPrinter);
        }

        private static void QueryPrinter(Options options)
        {
            var bidiSpl = Activator.CreateInstance<BidiSpl>();
            var bidiInterface = (IBidiSpl.IBidiSpl) bidiSpl;

            var bidiRequest = Activator.CreateInstance<BidiRequest>();
            var bidiRequestInterface = (IBidiRequest) bidiRequest;

            bidiRequestInterface.SetSchema(options.Schema);

            var access = options.Admin ? BIDI_ACCESS.BIDI_ACCESS_ADMINISTRATOR : BIDI_ACCESS.BIDI_ACCESS_USER;
            bidiInterface.BindDevice(options.PrinterName, (uint) access);

            try
            {
                bidiInterface.SendRecv("Get", bidiRequestInterface);

                bidiRequestInterface.GetResult(out var hResult);
                if (hResult != 0)
                {
                    Marshal.ThrowExceptionForHR(hResult);
                }

                bidiRequestInterface.GetEnumCount(out var count);

                Console.WriteLine($"Bidi request has {count} output results.");

                for (UInt32 i = 0; i < count; i++)
                {
                    bidiRequestInterface.GetOutputData(i, out var schemaPtr, out var type, out var dataPtr, out var size);

                    try
                    {
                        var schema = Marshal.PtrToStringUni(schemaPtr);
                        Console.WriteLine($"Schema: {schema}");

                        Console.WriteLine($"Data size: {size}");

                        switch ((BIDI_DATA_TYPE)type)
                        {
                            case BIDI_DATA_TYPE.BIDI_NULL:
                                Console.WriteLine("Data type NULL");
                                Console.WriteLine("Value: null");
                                break;

                            case BIDI_DATA_TYPE.BIDI_INT:
                                Console.WriteLine("Data type INT");
                                UInt32 intResult = (UInt32) Marshal.PtrToStructure(dataPtr, typeof(UInt32));
                                Console.WriteLine($"Value: {intResult}");
                                break;

                            case BIDI_DATA_TYPE.BIDI_FLOAT:
                                Console.WriteLine("Data type FLOAT");
                                float floatResult = (float)Marshal.PtrToStructure(dataPtr, typeof(float));
                                Console.WriteLine($"Value: {floatResult}");
                                break;

                            case BIDI_DATA_TYPE.BIDI_BOOL:
                                Console.WriteLine("Data type BOOL");
                                var boolResult = (bool) Marshal.PtrToStructure(dataPtr, typeof(bool));
                                Console.WriteLine($"Value: {boolResult}");
                                break;

                            case BIDI_DATA_TYPE.BIDI_STRING:
                                Console.WriteLine("Data type STRING");
                                var stringResult = Marshal.PtrToStringUni(dataPtr);
                                Console.WriteLine($"Value: {stringResult}");
                                break;

                            case BIDI_DATA_TYPE.BIDI_TEXT:
                                Console.WriteLine("Data type TEXT");
                                stringResult = Marshal.PtrToStringUni(dataPtr);
                                Console.WriteLine($"Value: {stringResult}");
                                break;

                            case BIDI_DATA_TYPE.BIDI_ENUM:
                                Console.WriteLine("Data type ENUM");
                                stringResult = Marshal.PtrToStringUni(dataPtr);
                                Console.WriteLine($"Value: {stringResult}");
                                break;

                            case BIDI_DATA_TYPE.BIDI_BLOB:
                                Console.WriteLine("Data type BLOB");
                                var bytes = new byte[size];
                                Marshal.Copy(dataPtr, bytes, 0, (int)size);
                                foreach (var b in bytes)
                                {
                                    Console.Write("{0:X2} ", b);
                                }
                                Console.WriteLine();
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }


                        Console.WriteLine();
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(schemaPtr);
                        Marshal.FreeCoTaskMem(dataPtr);
                    }
                }
            }
            finally
            {
                bidiInterface.UnbindDevice();

                Marshal.FinalReleaseComObject(bidiRequest);
                Marshal.FinalReleaseComObject(bidiSpl);
            }
        }
    }
}

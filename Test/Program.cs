using System;
using System.Runtime.InteropServices;
using CommandLine;
using IBidiSpl.Com;

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
                .WithParsed<GetOptions>(options => GetRequest(options, "Get"))
                .WithParsed<GetAllOptions>(options => GetRequest(options, "GetAll"))
                .WithParsed<SetOptions>(SetRequest)
                .WithParsed<GetWithArgumentOptions>(GetWithArgumentRequest)
                .WithParsed<EnumSchemaOptions>(EnumSchemaRequest);
        }

        private static void GetRequest(Options options, string verb)
        {
            var bidiSpl = (IBidiSpl.Com.IBidiSpl) Activator.CreateInstance<BidiSpl>();

            var access = options.Admin ? BIDI_ACCESS.BIDI_ACCESS_ADMINISTRATOR : BIDI_ACCESS.BIDI_ACCESS_USER;
            bidiSpl.BindDevice(options.PrinterName, (uint) access);

            var bidiRequestContainer = (IBidiRequestContainer)Activator.CreateInstance<BidiRequestContainer>();

            try
            {
                foreach (var schema in options.Schemas)
                {
                    var bidiRequest = (IBidiRequest)Activator.CreateInstance<BidiRequest>();
                    try
                    {
                        bidiRequest.SetSchema(schema.Trim());
                        bidiRequestContainer.AddRequest(bidiRequest);
                    }
                    finally
                    {
                        Marshal.FinalReleaseComObject(bidiRequest);
                    }
                }

                bidiSpl.MultiSendRecv(verb, bidiRequestContainer);

                bidiRequestContainer.GetEnumObject(out var enumerator);
                try
                {
                    while (true)
                    {
                        enumerator.Next(1, out var request, out var retrieved);
                        if (retrieved == 0)
                        {
                            break;
                        }

                        var bidiRequestInterface = (IBidiRequest)request;
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
                }
                finally
                {
                    Marshal.FinalReleaseComObject(enumerator);
                }


            }
            finally
            {
                bidiSpl.UnbindDevice();
                Marshal.FinalReleaseComObject(bidiRequestContainer);
                Marshal.FinalReleaseComObject(bidiSpl);
            }
        }

        private static void EnumSchemaRequest(EnumSchemaOptions obj)
        {
            throw new NotImplementedException(nameof(EnumSchemaRequest));
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

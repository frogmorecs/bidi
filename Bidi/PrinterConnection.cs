using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using IBidiSpl.Com;
// ReSharper disable SuspiciousTypeConversion.Global

namespace IBidiSpl
{
    public class PrinterConnection : IDisposable
    {
        private readonly Com.IBidiSpl _bidiSpl;
        private bool _bound;

        public PrinterConnection()
        {
            _bidiSpl = (Com.IBidiSpl) Activator.CreateInstance<Com.BidiSpl>();
        }

        public Dictionary<string, object> SendRequest(string schema, RequestType type)
        {
            var bidiRequest = (IBidiRequest) Activator.CreateInstance<BidiRequest>();

            try
            {
                bidiRequest.SetSchema(schema.Trim());

                _bidiSpl.SendRecv(type.ToString(), bidiRequest);

                bidiRequest.GetResult(out int hResult);
                if (hResult != 0)
                {
                    Marshal.ThrowExceptionForHR(hResult);
                }

                if (type == RequestType.EnumSchema)
                {
                    return GetData(bidiRequest).ToDictionary(t => (string)t.Item2, t => t.Item2);
                }
                else
                {
                    return GetData(bidiRequest).ToDictionary(t => t.Item1, t => t.Item2);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(bidiRequest);
            }
        }

        public void Bind(string printerName, BIDI_ACCESS access)
        {
            _bidiSpl.BindDevice(printerName, (uint)access);
            _bound = true;
        }

        public void UnBind()
        {
            if (_bound)
            {
                _bidiSpl.UnbindDevice();
            }
            _bound = false;
        }

        public void Dispose()
        {
            UnBind();
            Marshal.FinalReleaseComObject(_bidiSpl);
        }

        private IEnumerable<Tuple<string, object>> GetData(IBidiRequest bidiRequest)
        {
            bidiRequest.GetEnumCount(out uint count);
            for (uint i = 0; i < count; i++)
            {
                bidiRequest.GetOutputData(i, out var schemaPtr, out var type, out var dataPtr, out var size);
                try
                {
                    var sc = Marshal.PtrToStringUni(schemaPtr) ?? throw new NullReferenceException("sc");

                    switch ((BIDI_DATA_TYPE)type)
                    {
                        case BIDI_DATA_TYPE.BIDI_NULL:
                            yield return new Tuple<string, object>(sc, null);
                            break;

                        case BIDI_DATA_TYPE.BIDI_INT:
                            yield return new Tuple<string, object>(sc, (UInt32)Marshal.PtrToStructure(dataPtr, typeof(UInt32)));
                            break;

                        case BIDI_DATA_TYPE.BIDI_FLOAT:
                            yield return new Tuple<string, object>(sc, (float)Marshal.PtrToStructure(dataPtr, typeof(float)));
                            break;

                        case BIDI_DATA_TYPE.BIDI_BOOL:
                            yield return new Tuple<string, object>(sc, (bool)Marshal.PtrToStructure(dataPtr, typeof(bool)));
                            break;

                        case BIDI_DATA_TYPE.BIDI_STRING:
                            yield return new Tuple<string, object>(sc, Marshal.PtrToStringUni(dataPtr));
                            break;

                        case BIDI_DATA_TYPE.BIDI_TEXT:
                            yield return new Tuple<string, object>(sc, Marshal.PtrToStringUni(dataPtr));
                            break;

                        case BIDI_DATA_TYPE.BIDI_ENUM:
                            yield return new Tuple<string, object>(sc, Marshal.PtrToStringUni(dataPtr));
                            break;

                        case BIDI_DATA_TYPE.BIDI_BLOB:
                            var bytes = new byte[size];
                            Marshal.Copy(dataPtr, bytes, 0, (int)size);
                            yield return new Tuple<string, object>(sc, bytes);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(type));
                    }
                }
                finally
                {
                    Marshal.FreeCoTaskMem(schemaPtr);
                    Marshal.FreeCoTaskMem(dataPtr);
                }
            }
        }
    }

    public enum RequestType
    {
        Get,
        GetAll,
        EnumSchema
    }
}

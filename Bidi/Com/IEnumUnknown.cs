using System.Runtime.InteropServices;

namespace Bidi.Com
{
    [Guid("00000100-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumUnknown
    {
        int Next(
            int celt,
            [MarshalAs(UnmanagedType.IUnknown)] out object rgelt,
            out int pceltFetched
        );

        int Skip([MarshalAs(UnmanagedType.U4)] int celt);
        void Reset();
        void Clone([MarshalAs(UnmanagedType.IUnknown)] out IEnumUnknown ppenum);
    }
}
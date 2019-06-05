using System.Runtime.InteropServices;

namespace IBidiSpl
{
    [ComImport, Guid("00000100-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumUnknown
    {
        int Next(
            [In] int celt,
            [Out, MarshalAs(UnmanagedType.IUnknown)] object rgelt,
            [Out] int pceltFetched
        );

        int Skip([In, MarshalAs(UnmanagedType.U4)] int celt);
        void Reset();
        void Clone([Out] IEnumUnknown ppenum);
    }
}
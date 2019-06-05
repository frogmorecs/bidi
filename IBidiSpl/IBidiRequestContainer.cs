using System;
using System.Runtime.InteropServices;

namespace IBidiSpl
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport, Guid("D752F6C0-94A8-4275-A77D-8F1D1A1121AE")]
    interface IBidiRequestContainer
    {
        // virtual HRESULT STDMETHODCALLTYPE AddRequest(
        // /* [in] */ __RPC__in_opt IBidiRequest *pRequest) = 0;

        Int32 AddRequest([In, MarshalAs(UnmanagedType.Interface)] IBidiRequest pRequest);

        // virtual HRESULT STDMETHODCALLTYPE GetEnumObject(
        // /* [out] */ __RPC__deref_out_opt IEnumUnknown **ppenum) = 0;

        Int32 GetEnumObject([Out, MarshalAs(UnmanagedType.Interface)] IEnumUnknown ppenum);

        // virtual HRESULT STDMETHODCALLTYPE GetRequestCount(
        // /* [out] */ __RPC__out ULONG *puCount) = 0;

        Int32 GetRequestCount([Out] UInt32 puCount);
    }
}

using System;
using System.Runtime.InteropServices;

namespace IBidiSpl.Com
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("D752F6C0-94A8-4275-A77D-8F1D1A1121AE")]
    public interface IBidiRequestContainer
    {
        // virtual HRESULT STDMETHODCALLTYPE AddRequest(
        // /* [in] */ __RPC__in_opt IBidiRequest *pRequest) = 0;

        Int32 AddRequest([MarshalAs(UnmanagedType.Interface)] IBidiRequest pRequest);

        // virtual HRESULT STDMETHODCALLTYPE GetEnumObject(
        // /* [out] */ __RPC__deref_out_opt IEnumUnknown **ppenum) = 0;

        Int32 GetEnumObject([MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppenum);

        // virtual HRESULT STDMETHODCALLTYPE GetRequestCount(
        // /* [out] */ __RPC__out ULONG *puCount) = 0;

        Int32 GetRequestCount(out UInt32 puCount);
    }
}

using System;
using System.Runtime.InteropServices;

namespace IBidiSpl
{
    [ComImport, Guid("D580DC0E-DE39-4649-BAA8-BF0B85A03A97")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IBidiSpl
    {
        // virtual HRESULT STDMETHODCALLTYPE BindDevice(
        // /* [in] */ __RPC__in const LPCWSTR pszDeviceName,
        // /* [in] */ const DWORD dwAccess) = 0;

        Int32 BindDevice(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszDeviceName,
            [In] UInt32 dwAccess
        );

        // virtual HRESULT STDMETHODCALLTYPE UnbindDevice(void) = 0;
        Int32 UnbindDevice();

        // virtual HRESULT STDMETHODCALLTYPE SendRecv(
        // /* [in] */ __RPC__in const LPCWSTR pszAction,
        // /* [in] */ __RPC__in_opt IBidiRequest* pRequest) = 0;
        Int32 SendRecv(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAction, 
            [In, MarshalAs(UnmanagedType.Interface)] IBidiRequest pRequest
        );

        // virtual HRESULT STDMETHODCALLTYPE MultiSendRecv(
        // /* [in] */ __RPC__in const LPCWSTR pszAction,
        // /* [in] */ __RPC__in_opt IBidiRequestContainer* pRequestContainer) = 0;
        Int32 MultiSendRecv(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAction,
            [In, MarshalAs(UnmanagedType.Interface)] IBidiRequestContainer pRequestContainer
        );
    }
}
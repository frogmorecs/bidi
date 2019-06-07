using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace IBidiSpl.Com
{
    [Guid("D580DC0E-DE39-4649-BAA8-BF0B85A03A97")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBidiSpl
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

    public enum BIDI_ACCESS: uint
    {
        BIDI_ACCESS_ADMINISTRATOR = 0x1,
        BIDI_ACCESS_USER = 0x2,
    }

    public enum BIDI_DATA_TYPE: uint
    {
        BIDI_NULL = 0,
        BIDI_INT = 1,
        BIDI_FLOAT = 2,
        BIDI_BOOL = 3,
        BIDI_STRING = 4,
        BIDI_TEXT = 5,
        BIDI_ENUM = 6,
        BIDI_BLOB = 7
    }
}
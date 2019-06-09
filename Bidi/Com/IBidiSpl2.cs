using System;
using System.Runtime.InteropServices;

namespace Bidi.Com
{
    [Guid("0E8F51B8-8273-4906-8E7B-BE453FFD2E2B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IBidiSpl2
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

        // virtual HRESULT STDMETHODCALLTYPE SendRecvXMLString(
        // /* [in] */ __RPC__in BSTR bstrRequest,
        // /* [out] */ __RPC__deref_out_opt BSTR *pbstrResponse) = 0;

        Int32 SendRecvXMLString(
            [In] string bstrRequest,
            [Out] string pbstrResponse
        );

        // virtual HRESULT STDMETHODCALLTYPE SendRecvXMLStream(
        // /* [in] */ __RPC__in_opt IStream *pSRequest,
        // /* [out] */ __RPC__deref_out_opt IStream **ppSResponse) = 0;

        Int32 SendRecvXMLStream(
            [In, MarshalAs(UnmanagedType.Interface)] IStream pSRequest,
            [Out, MarshalAs(UnmanagedType.Interface)] IStream ppSResponse
        );
    }
}
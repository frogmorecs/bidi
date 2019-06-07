using System;
using System.Runtime.InteropServices;

// ReSharper disable CommentTypo

namespace IBidiSpl.Com
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("8F348BD7-4B47-4755-8A9D-0F422DF3DC89")]
    public interface IBidiRequest
    {
        // virtual HRESULT STDMETHODCALLTYPE SetSchema(
        // /* [in] */ __RPC__in const LPCWSTR pszSchema) = 0;

        Int32 SetSchema([MarshalAs(UnmanagedType.LPWStr)] string pszSchema);

        // virtual HRESULT STDMETHODCALLTYPE SetInputData(
        // /* [in] */ const DWORD dwType,
        // /* [in] */ __RPC__in const BYTE* pData,
        // /* [in] */ const UINT uSize) = 0;

        Int32 SetInputData([In] UInt32 dwType, [In] IntPtr pData, [In] UInt32 uSize);

        // virtual HRESULT STDMETHODCALLTYPE GetResult(
        // /* [out] */ __RPC__out HRESULT *phr) = 0;

        Int32 GetResult(out Int32 phr);

        // virtual HRESULT STDMETHODCALLTYPE GetOutputData(
        // /* [in] */ const DWORD dwIndex,
        // /* [out] */ __RPC__deref_out_opt LPWSTR* ppszSchema,
        // /* [out] */ __RPC__out DWORD* pdwType,
        // /* [out] */ __RPC__deref_out_opt BYTE** ppData,
        // /* [out] */ __RPC__out ULONG* uSize) = 0;

        Int32 GetOutputData(UInt32 dwIndex,
                            out IntPtr ppszSchema,
                            out UInt32 pdwType,
                            out IntPtr ppData,
                            out UInt32 uSize);

        //virtual HRESULT STDMETHODCALLTYPE GetEnumCount(
        //    /* [out] */ __RPC__out DWORD *pdwTotal) = 0;
        Int32 GetEnumCount(out UInt32 pdwTotal);
    }
}
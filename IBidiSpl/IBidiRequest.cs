using System;
using System.Runtime.InteropServices;
// ReSharper disable CommentTypo

namespace IBidiSpl
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport, Guid("8F348BD7-4B47-4755-8A9D-0F422DF3DC89")]
    public interface IBidiRequest
    {
        // virtual HRESULT STDMETHODCALLTYPE SetSchema(
        // /* [in] */ __RPC__in const LPCWSTR pszSchema) = 0;

        Int32 SetSchema([In, MarshalAs(UnmanagedType.LPWStr)] string pszSchema);

        // virtual HRESULT STDMETHODCALLTYPE SetInputData(
        // /* [in] */ const DWORD dwType,
        // /* [in] */ __RPC__in const BYTE* pData,
        // /* [in] */ const UINT uSize) = 0;

        Int32 SetInputData([In] UInt32 dwType, [In] IntPtr pData, [In] UInt32 uSize);

        // virtual HRESULT STDMETHODCALLTYPE GetResult(
        // /* [out] */ __RPC__out HRESULT *phr) = 0;

        Int32 GetResult([Out] Int32 phr);

        // virtual HRESULT STDMETHODCALLTYPE GetOutputData(
        // /* [in] */ const DWORD dwIndex,
        // /* [out] */ __RPC__deref_out_opt LPWSTR* ppszSchema,
        // /* [out] */ __RPC__out DWORD* pdwType,
        // /* [out] */ __RPC__deref_out_opt BYTE** ppData,
        // /* [out] */ __RPC__out ULONG* uSize) = 0;

        Int32 GetOutputData([In] UInt32 dwIndex,
                            [Out] string ppszSchema,
                            [Out] UInt32 pdwType,
                            [Out] IntPtr ppData,
                            [Out] UInt32 uSize);

        //virtual HRESULT STDMETHODCALLTYPE GetEnumCount(
        //    /* [out] */ __RPC__out DWORD *pdwTotal) = 0;
        Int32 GetEnumCount([Out] UInt32 pdwTotal);
    }
}
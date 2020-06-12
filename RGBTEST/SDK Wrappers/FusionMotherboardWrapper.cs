using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBTEST.SDK_Wrappers
{
    class FusionMotherboardWrapper
    {
        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern uint dllexp_GetSdkVersion(byte[] lpBuf, int bufSize);
        ///<summary>
        ///Returs buffer with unicode for SDK version 
        ///</summary>
        ///<param name="lpBuf">Buffer to store sdk version</param>
        ///<param name="bufSize">Size of buffer, should not be less than 16</param>
        ///<returns>
        ///<para>0 - Success</para>
        ///<para>122 - insufficient buffer</para>
        ///</returns>
        public static uint GetSdkVersion(byte[] lpBuf, int bufSize) => dllexp_GetSdkVersion(lpBuf, bufSize);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_InitAPI();
        /// <summary>
        /// Initialises the SDK library on the current application
        /// </summary>
        /// <returns>
        /// <para>0 - Success</para>
        /// <para>4317 - Invalid Operation</para>
        /// </returns>
        public static uint InitAPI() => dllexp_InitAPI();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int dllexp_GetMaxDivision();
        /// <summary>
        /// Gets the total amount of RGB zones
        /// </summary>
        /// <returns>
        /// Returns int number of zones
        /// </returns>
        public static int GetMaxDivision() => dllexp_GetMaxDivision();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_GetLedLayout(byte[] bytArray, int arySize);
        /// <summary>
        /// Gets the layout of LEDs on the motherboard
        /// </summary>
        /// <param name="bytArray">Byte array to collect output</param>
        /// <param name="arySize">Size of bytArray, must be GetDivision() return value</param>
        /// <returns>
        /// <para>0 - Success</para>
        /// <para>4317 - Fail</para>
        /// <para>122 - Insufficient Buffer</para>
        /// <para>50 - Not Supported</para>
        /// </returns>
        public static uint GetLedLayout(byte[] bytArray, int arySize) => dllexp_GetLedLayout(bytArray, arySize);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_SetLedData(byte[] bytArray, int arySize);
        /// <summary>
        /// Sets the LED data for each zone, call Apply() to apply the LED data
        /// </summary>
        /// <param name="bytArray">
        /// Byte array storing data for each zone, size = 16 * GetMaxDivision()
        /// </param>
        /// <param name="arySize">
        /// Int size of bytArray
        /// </param>
        /// <returns>
        /// <para>0 - Success</para>
        /// <para>4317 - Invalid Operation</para>
        /// </returns>
        public static uint SetLedData(byte[] bytArray, int arySize) => dllexp_SetLedData(bytArray, arySize);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_Apply(int iApplyBit);
        /// <summary>
        /// Applies the LED data to the selected zones
        /// </summary>
        /// <param name="iApplyBit">Binary to select zone</param>
        /// <returns>
        /// <para>0 - Success</para>
        /// <para>4317 - Invalid Operation</para>
        /// </returns>
        /// <remarks>
        /// <para>1 for zone 0</para>
        /// <para>2 for zone 1</para>
        /// <para>3 for zone 0 and 1</para>
        /// <para>etc</para>
        /// <para>-1 for all zones</para>
        /// </remarks>
        public static uint Apply(int iApplyBit) => dllexp_Apply(iApplyBit);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_BeatInput(int iCtrl);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_IT8295_Reset();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_Get_IT8295_FwVer(byte[] bytArray, int arySize);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_RGBCalibration_Step1(int cal_div);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void dllexp_RGBCalibration_Step2();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void dllexp_RGBCalibration_Step3();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_RGBCalibration_Done(int cal_div);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_GetCalibrationValue();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int dllexp_MonocLedCtrlSupport();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int dllexp_GetRGBPinType();

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool dllexp_SetMonocLedMode(int mnoLedMode);

        [DllImport(@"lib\GLedApi.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_RGBPin_Type1(int pin1, int pin2, int pin3);

    }
}

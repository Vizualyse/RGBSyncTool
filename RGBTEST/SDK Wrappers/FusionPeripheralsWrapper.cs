using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RGBTEST.SDK_Wrappers
{
    class FusionPeripheralsWrapper
    {
        [DllImport(@"lib\GvLedLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_GvLedInitial(out int iDeviceCount, [In, Out] int[] iDeviceIdArray);
        /// <summary>
        /// Initialises the fusion peripherals api to be used with this program
        /// </summary>
        /// <param name="iDeviceCount">
        /// Int number of connected devices (+1 has 0x5001 termination device(?))
        /// </param>
        /// <param name="iDeviceIdArray">
        /// Int[] array of devices, see docs for device IDs
        /// </param>
        /// <returns></returns>
        public static uint InitAPI(out int iDeviceCount, [In, Out] int[] iDeviceIdArray) => dllexp_GvLedInitial(out iDeviceCount, iDeviceIdArray);
        [DllImport(@"lib\GvLedLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_GvLedGetVersion(out int iMajorVersion, out int iMinorVersion);

        [DllImport(@"lib\GvLedLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_GvLedSave(int nIndex, GVLED_CFG config);
        /// <summary>
        /// Calls the GvLedSave() method, which sets LEDs
        /// </summary>
        /// <param name="nIndex">
        /// Device ID you want to apply to, -1 for all devices
        /// </param>
        /// <param name="config"></param>
        /// <returns>
        /// <para>0 - Success</para>
        /// <para>2 - Device not available</para>
        /// <para>3 - Parameter error</para>
        /// </returns>
        public static uint SetLed(int nIndex, GVLED_CFG config) => dllexp_GvLedSave(nIndex, config);

        [DllImport(@"lib\GvLedLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_GvLedSet(int nIndex, GVLED_CFG config);

        [DllImport(@"lib\GvLedLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint dllexp_GvLedGetVgaModelName(out byte[] pVgaModelName);

    }
}

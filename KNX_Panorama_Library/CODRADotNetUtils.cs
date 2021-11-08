/*
  SDK/IncludeNG/CODRADotNetUtils.cs
 
 V6.0.0.0   2015-06-02
  
  Constants, interfaces and utility methods in C# to be used to create user objects 
  for PANORAMA. See "Advanced Use" manual.
 
  File provided to Panorama users to create their C# user objects.
  
  Définition des constantes, des interfaces et des méthodes utilitaires en C#
  pour créer des objets utilisateurs pour PANORAMA. Voir manuel "utilisation avancée".
  
  Fichier fourni aux utilisateurs Panorama pour créer leurs	objets utilisateur en C#.

 */

using System;
using System.Runtime.InteropServices;
using System.Globalization;

// Should be CODRA.Panorama.SDK.DotNetUtils, but won't be changed now as it may imply
// too many compatibility problems in case clients projects have not copied this file
// into their solutions or if the client upgrades its copy.
namespace CODRA.SDK.DotNetUtils     // Do not change
{
	namespace OPC
	{
		// OPC error codes
        internal static class OPCError
        {
			public const int OPC_E_INVALIDHANDLE    = unchecked((int) 0xC0040001);
			public const int OPC_E_BADTYPE          = unchecked((int) 0xC0040004);
			public const int OPC_E_PUBLIC           = unchecked((int) 0xC0040005);
			public const int OPC_E_BADRIGHTS        = unchecked((int) 0xC0040006);
			public const int OPC_E_UNKNOWNITEMID    = unchecked((int) 0xC0040007);
			public const int OPC_E_INVALIDITEMID    = unchecked((int) 0xC0040008);
			public const int OPC_E_INVALIDFILTER    = unchecked((int) 0xC0040009);
			public const int OPC_E_UNKNOWNPATH      = unchecked((int) 0xC004000A);
			public const int OPC_E_RANGE            = unchecked((int) 0xC004000B);
			public const int OPC_E_DUPLICATENAME    = unchecked((int) 0xC004000C);
			public const int OPC_S_UNSUPPORTEDRATE  = unchecked((int) 0x0004000D);
			public const int OPC_S_CLAMP            = unchecked((int) 0x0004000E);
			public const int OPC_S_INUSE            = unchecked((int) 0x0004000F);
			public const int OPC_E_INVALIDCONFIGFILE= unchecked((int) 0xC0040010);
			public const int OPC_E_NOTFOUND         = unchecked((int) 0xC0040011);
			public const int OPC_E_INVALID_PID      = unchecked((int) 0xC0040203);
		}

		// OPC qualities
        internal static class OPCQuality
		{
			// Masks used to extract quality subfields
			// (note 'status' mask also includes 'Quality' bits)
			//
			public const ushort OPC_QUALITY_MASK = 0x00C0;
			public const ushort OPC_STATUS_MASK = 0x00FC;
			public const ushort OPC_LIMIT_MASK = 0x0003;

			// Values for QUALITY_MASK bit field
			//
			public const ushort OPC_QUALITY_BAD = 0x0000;
			public const ushort OPC_QUALITY_UNCERTAIN = 0x0040;
			public const ushort OPC_QUALITY_GOOD = 0x00C0;

			// STATUS_MASK Values for Quality = BAD
			//
			public const ushort OPC_QUALITY_CONFIG_ERROR = 0x0004;
			public const ushort OPC_QUALITY_NOT_CONNECTED = 0x0008;
			public const ushort OPC_QUALITY_DEVICE_FAILURE = 0x000c;
			public const ushort OPC_QUALITY_SENSOR_FAILURE = 0x0010;
			public const ushort OPC_QUALITY_LAST_KNOWN = 0x0014;
			public const ushort OPC_QUALITY_COMM_FAILURE = 0x0018;
			public const ushort OPC_QUALITY_OUT_OF_SERVICE = 0x001C;
            public const ushort OPC_QUALITY_WAITING_FOR_INITIAL_DATA = 0x0020;

			// STATUS_MASK Values for Quality = UNCERTAIN
			//
			public const ushort OPC_QUALITY_LAST_USABLE = 0x0044;
			public const ushort OPC_QUALITY_SENSOR_CAL = 0x0050;
			public const ushort OPC_QUALITY_EGU_EXCEEDED = 0x0054;
			public const ushort OPC_QUALITY_SUB_NORMAL = 0x0058;

			// STATUS_MASK Values for Quality = GOOD
			//
			public const ushort OPC_QUALITY_LOCAL_OVERRIDE = 0x00D8;

			// LIMIT_MASK Values for Quality = GOOD
			//
			public const ushort OPC_LIMIT_LOW = 0x0001;
			public const ushort OPC_LIMIT_HIGH = 0x0002;
			public const ushort OPC_LIMIT_CONST = 0x0003;
		}
	}


	namespace COM
	{
		// COM Error Codes
        internal static class COMError
		{
			// This list provides some COM error codes that can be used with:
			// System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(CODRA.SDK.DotNetUtils.COM.COMError.E_POINTER);

            // winerror.h :
			public const int S_OK						= 0x00000000;
			public const int S_FALSE					= 0x00000001;
            public const int E_UNEXPECTED               = unchecked((int) 0x8000FFFF);
            public const int E_NOTIMPL                  = unchecked((int) 0x80004001);
            public const int E_OUTOFMEMORY              = unchecked((int) 0x8007000E);
            public const int E_INVALIDARG               = unchecked((int) 0x80070057);
            public const int E_NOINTERFACE              = unchecked((int) 0x80004002);
            public const int E_POINTER                  = unchecked((int) 0x80004003);
            public const int E_HANDLE                   = unchecked((int) 0x80070006);
            public const int E_ABORT                    = unchecked((int) 0x80004004);
            public const int E_FAIL                     = unchecked((int) 0x80004005);
            public const int E_ACCESSDENIED             = unchecked((int) 0x80070005);

            public const int CLASS_E_NOTLICENSED        = unchecked((int) 0x80040112);

            public const int DISP_E_UNKNOWNINTERFACE    = unchecked((int) 0x80020001);
            public const int DISP_E_MEMBERNOTFOUND      = unchecked((int) 0x80020003);
            public const int DISP_E_PARAMNOTFOUND       = unchecked((int) 0x80020004);
            public const int DISP_E_TYPEMISMATCH        = unchecked((int) 0x80020005);
            public const int DISP_E_UNKNOWNNAME         = unchecked((int) 0x80020006);
            public const int DISP_E_NONAMEDARGS         = unchecked((int) 0x80020007);
            public const int DISP_E_BADVARTYPE          = unchecked((int) 0x80020008);
            public const int DISP_E_EXCEPTION           = unchecked((int) 0x80020009);
            public const int DISP_E_OVERFLOW            = unchecked((int) 0x8002000A);
            public const int DISP_E_BADINDEX            = unchecked((int) 0x8002000B);
            public const int DISP_E_BADPARAMCOUNT       = unchecked((int) 0x8002000E);
            public const int DISP_E_PARAMNOTOPTIONAL    = unchecked((int) 0x8002000F);

            public const int TYPE_E_LIBNOTREGISTERED    = unchecked((int) 0x8002801D);
            public const int TYPE_E_TYPEMISMATCH        = unchecked((int) 0x80028CA0);
            public const int TYPE_E_CANTLOADLIBRARY     = unchecked((int) 0x80029C4A);
            public const int REGDB_E_CLASSNOTREG        = unchecked((int) 0x80040154);
            public const int REGDB_E_IIDNOTREG          = unchecked((int) 0x80040155);

            public const int STG_E_FILENOTFOUND         = unchecked((int) 0x80030002);
            public const int STG_E_PATHNOTFOUND         = unchecked((int) 0x80030003);
            public const int STG_E_ACCESSDENIED         = unchecked((int) 0x80030005);
            public const int STG_E_SHAREVIOLATION       = unchecked((int) 0x80030020);
            public const int STG_E_LOCKVIOLATION        = unchecked((int) 0x80030021);
            public const int STG_E_FILEALREADYEXISTS    = unchecked((int) 0x80030050);

            // olectl.h :
			public const int CONNECT_E_NOCONNECTION     = unchecked((int) 0x80040200 );
			public const int CONNECT_E_ADVISELIMIT	    = unchecked((int) 0x80040201 );
		}


		/// <summary>
        /// IObjectWithSite interface.
		/// </summary>
		[ComVisible(true)]
		[ComImport]
		[Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352") ]
		[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IObjectWithSite
		{
			void SetSite([MarshalAs(UnmanagedType.IUnknown)]
				[In] object pSite);
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
            void GetSite([In] ref Guid riid, [Out] out IntPtr pvSite);
		}
	}

    // Utility class used to retrieve the current configured application.
    // To get the running application, use the Application property on the object site instead.
    // Usage:
    //    string sAppPath = AppInfo.GetCurrentApplication();
    internal static class AppInfo
    {
        private delegate bool ReadStringDelegate(string FatherNodeName, string ChildNodeName, string ValueName, ref string Value);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static string GetCurrentApplication()
        {
            string sApp = "";
            try
            {
                Object objConfigManager = Activator.CreateInstance("ConfigManager", "CODRA.Panorama.SDK.ConfigManager.Manager");
                if (objConfigManager != null)
                {
                    objConfigManager = (objConfigManager as System.Runtime.Remoting.ObjectHandle).Unwrap();
                    ReadStringDelegate ReadString = Delegate.CreateDelegate(typeof(ReadStringDelegate), objConfigManager, "ReadString") as ReadStringDelegate;
                    ReadString("NG", "", "CurrentApplication", ref sApp);
                    (objConfigManager as IDisposable).Dispose();
                }
            }
            catch
            { ; }
            return sApp;
        }
    }

    // Utility class used to retrieve the installation directory and language of Panorama.
    // Usage:
    //    string sPanoramaInstallPath = PanoramaInfo.GetInstallPath();
    //    uint uiPanoramaInstallLanguauge = PanoramaInfo.GetInstallLanguage();
    internal static class PanoramaInfo
    {
        private const string REG_PANORAMA_NG_INSTALL_KEY = "SOFTWARE\\Codra\\Panorama\\NG_INSTALL";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static string GetInstallPath()
        {
            string sPanoramaPath = "";
            try
            {
                using (Microsoft.Win32.RegistryKey rkKeyNG = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_PANORAMA_NG_INSTALL_KEY))
                {
                    sPanoramaPath = rkKeyNG.GetValue("Path").ToString();
                }
            }
            catch
            { ; }
            return sPanoramaPath;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static uint GetInstallLanguage()
        {
            uint uiLanguage = 0;
            try
            {
                using (Microsoft.Win32.RegistryKey rkKeyNG = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_PANORAMA_NG_INSTALL_KEY))
                {
                    uiLanguage = Convert.ToUInt32(rkKeyNG.GetValue("Language"),CultureInfo.InvariantCulture);
                }
            }
            catch
            { ; }
            return uiLanguage;
        }
    }
}

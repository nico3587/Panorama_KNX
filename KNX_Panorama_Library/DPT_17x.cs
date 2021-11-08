using System;
using System.ComponentModel;
using CODRA.SDK.DotNetUtils.COM;
using CODRA.Panorama.CommonPanorama;
using System.Runtime.InteropServices;

namespace Panorama_KNX
{
    public class DPT_17x : IObjectWithSite
    {
        private object m_Lock = new object();
        private object m_pSite;

        const int valueId = 1;

        private KNX_Panorama m_pParent = null;
        internal void SetParent(KNX_Panorama pParent)
        {
            m_pParent = pParent;
        }

        private string m_groupAddress;
        [Description("Adresse de groupe format x/x/x"), Category("Configuration")]
        public string groupAddress
        {
            get
            {
                return m_groupAddress;
            }
            set
            {
                m_groupAddress = value;
            }
        }

        private string m_objectName;
        [Description("Nom de l'objet"), Category("Configuration")]
        public string objectName
        {
            get
            {
                return m_objectName;
            }
            set
            {
                m_objectName = value;
            }
        }

        private string m_key;
        [Description("Clef unique identifiant feedback"), Category("Configuration")]
        public string key
        {
            get
            {
                return m_key;
            }
            set
            {
                m_key = value;
            }
        }

        private string m_dpt;
        [Description("Data point type"), Category("Configuration")]
        public string dpt
        {
            get
            {
                return m_dpt;
            }
            set
            {
                m_dpt = value;
            }
        }

        private string m_dptName;
        [Description("Data point type name"), Category("Configuration")]
        public string dptName
        {
            get
            {
                return m_dptName;
            }
            set
            {
                m_dptName = value;
            }
        }

        private int m_value;
        [Description("Valeur de l'adresse de groupe"), Category("Valeur")]
        [DispId(valueId)]
        public int value
        {
            get
            {
                return m_value;
            }
            set
            {
                if (value >=0 && value < 64)
                {
                    m_value = value;
                    m_pParent.oneByteCommand(m_groupAddress, m_value);
                }
            }
        }
        internal void Feedback(int value)
        {
            m_value = value;
            (m_pSite as IPNGNotify).NotifyValue(valueId, m_value);
        }

        // Implementation of IObjectWithSite
        static void InternalReleaseSite(object pSite)
        {
            Marshal.ReleaseComObject(pSite);
        }

        public void SetSite([MarshalAs(UnmanagedType.IUnknown)][In] object pSite)
        {
            // Eventually, free previous site
            if (m_pSite != null && m_pSite.GetType().IsCOMObject)
                InternalReleaseSite(m_pSite);

            // Take new site
            m_pSite = pSite;
        }

        public void GetSite([In] ref Guid riid, [Out] out IntPtr pvSite)
        {
            // Initialize return value to NULL
            pvSite = System.IntPtr.Zero;

            // If no site, return E_FAIL
            if (m_pSite == null)
                Marshal.ThrowExceptionForHR(CODRA.SDK.DotNetUtils.COM.COMError.E_FAIL);

            // Otherwise, get requested interface
            IntPtr pUnk = Marshal.GetIUnknownForObject(m_pSite);
            Marshal.QueryInterface(pUnk, ref riid, out pvSite);
            Marshal.Release(pUnk);
            if (pvSite.Equals(IntPtr.Zero))
                Marshal.ThrowExceptionForHR(CODRA.SDK.DotNetUtils.COM.COMError.E_NOINTERFACE);
        }
    }
}

using System;
using System.Text;
using System.ComponentModel;
using CODRA.SDK.DotNetUtils.COM;
using CODRA.Panorama.CommonPanorama;
using System.Runtime.InteropServices;

namespace Panorama_KNX
{
    public class DPT_4x : IObjectWithSite
    {
        private object m_Lock = new object();
        private object m_pSite;

        const int   valueId = 1,
                    readValueId = 2;

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

        private string m_value;
        [Description("Valeur de l'adresse de groupe"), Category("Valeur")]
        [DispId(valueId)]
        public string value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value.Substring(0, 1);
                if (dpt == "DPST-4-1") //ASCII encoding
                    m_pParent.CharCommand(m_groupAddress, m_value, 1);
                else if (dpt == "DPST-4-2") //ISO8859 encoding
                    m_pParent.CharCommand(m_groupAddress, m_value, 2);
            }
        }

        [DispId(readValueId)]
        private bool m_readValue;
        [Description("Lecture de la valeur de l'adresse de groupe"), Category("Valeur")]
        public bool readValue
        {
            get
            {
                return m_readValue;
            }
            set
            {
                m_readValue = value;
                if (m_readValue)
                {
                    m_pParent.ReadValue(m_groupAddress);
                    m_readValue = false;
                    (m_pSite as IPNGNotify).NotifyValue(readValueId, m_readValue);
                }
            }
        }

        internal void Feedback(byte[] value)
        {
            string valueString = "";
            if (dpt == "DPST-4-1") //ASCII encoding
            {
                valueString = ((char)value[0]).ToString();
            }
            else if (dpt == "DPST-4-2") //ISO8859 encoding
            {
                Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                valueString = iso.GetString(value);
            }

           m_value = valueString;
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

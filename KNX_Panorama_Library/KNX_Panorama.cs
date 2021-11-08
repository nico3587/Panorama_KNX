using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CODRA.SDK.DotNetUtils.COM;
using CODRA.Panorama.CommonPanorama;
using CODRA.Panorama.Persist;
using System.Runtime.InteropServices;
using Knx.Falcon.Sdk;
using Knx.Bus.Common.Configuration;
using Knx.Bus.Common.GroupValues;
using Knx.Bus.Common;
using System.Collections;

namespace Panorama_KNX
{

    public class KNX_Panorama : IObjectWithSite
    {
        private Bus bus;
        private object m_pSite;

        internal IPrsObject MyPrsObject;
        internal IPrsApplication MyPrsApplication;
        internal string MyFullName;

        private static Import import = new Import();

        const int isConnectedId = 1;

        #region Define property

        private static string m_ipAddress;
        public string ipAddress
        {
            set
            {
                m_ipAddress = value;
                bus = new Bus(new KnxIpTunnelingConnectorParameters(value, 0x0e57, false));
            }
        }
        public bool connect
        {
            set
            {
                if (value == true)
                {
                    Connect();
                }
                else
                {
                    Disconnect();
                }
            }
        }

        private bool m_isConnected;
        [DispId(isConnectedId)]
        public bool isConnected
        {
            get
            {
                return m_isConnected;
            }
        }

        private Hashtable m_dpt1xObjects = new Hashtable();
        public IDictionary dpt1xObjects
        {
            get
            {
                return m_dpt1xObjects;
            }
        }

        private Hashtable m_dpt2xObjects = new Hashtable();
        public IDictionary dpt2xObjects
        {
            get
            {
                return m_dpt2xObjects;
            }
        }

        private Hashtable m_dpt3xObjects = new Hashtable();
        public IDictionary dpt3xObjects
        {
            get
            {
                return m_dpt3xObjects;
            }
        }

        private Hashtable m_dpt4xObjects = new Hashtable();
        public IDictionary dpt4xObjects
        {
            get
            {
                return m_dpt4xObjects;
            }
        }

        private Hashtable m_dpt5xObjects = new Hashtable();
        public IDictionary dpt5xObjects
        {
            get
            {
                return m_dpt5xObjects;
            }
        }

        private Hashtable m_dpt6xObjects = new Hashtable();
        public IDictionary dpt6xObjects
        {
            get
            {
                return m_dpt6xObjects;
            }
        }

        private Hashtable m_dpt7xObjects = new Hashtable();
        public IDictionary dpt7xObjects
        {
            get
            {
                return m_dpt7xObjects;
            }
        }

        private Hashtable m_dpt17xObjects = new Hashtable();
        public IDictionary dpt17xObjects
        {
            get
            {
                return m_dpt17xObjects;
            }
        }

        private Hashtable m_dpt18xObjects = new Hashtable();
        public IDictionary dpt18xObjects
        {
            get
            {
                return m_dpt18xObjects;
            }
        }

        private bool m_bRunning;
        public bool Running
        {
            get
            {
                return m_bRunning;
            }
            set
            {
                if (m_bRunning != value)
                {
                    m_bRunning = value;
                    if (m_bRunning)
                    {
                        //Add Object with DPT 1x and set parent link
                        foreach (DPT_1x Obj in m_dpt1xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt1x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 2x and set parent link
                        foreach (DPT_2x Obj in m_dpt2xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt2x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 3x and set parent link
                        foreach (DPT_3x Obj in m_dpt3xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt3x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 4x and set parent link
                        foreach (DPT_4x Obj in m_dpt4xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt4x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 5x and set parent link
                        foreach (DPT_5x Obj in m_dpt5xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt5x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 6x and set parent link
                        foreach (DPT_6x Obj in m_dpt6xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt6x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 7x and set parent link
                        foreach (DPT_7x Obj in m_dpt7xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt7x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 17x and set parent link
                        foreach (DPT_17x Obj in m_dpt17xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt17x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                        //Add Object with DPT 18x and set parent link
                        foreach (DPT_18x Obj in m_dpt18xObjects.Values)
                        {
                            if (Obj != null)
                            {
                                Obj.SetParent(this);
                                gaDpt18x.Add(Obj.key, Obj.groupAddress);
                            }
                        }
                    }
                    else
                    {
                        bus.Disconnect();
                        bus.Dispose();
                    }
                }
            }
        }
        #endregion

        private Dictionary<string, string> gaDpt1x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt2x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt3x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt4x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt5x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt6x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt7x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt17x = new Dictionary<string, string>();
        private Dictionary<string, string> gaDpt18x = new Dictionary<string, string>();

        private void Connect()
        {
            try
            {
                bus.Connect();
                bus.GroupValueReceived += MessageReceived;
            }
            catch
            {
                bus.Disconnect();
            }
            m_isConnected = bus.IsConnected;
            (m_pSite as IPNGNotify).NotifyValue(isConnectedId, m_isConnected);
        }

        private void Disconnect()
        {
            if (bus != null)
            {
                bus.Disconnect();
                m_isConnected = bus.IsConnected;
                (m_pSite as IPNGNotify).NotifyValue(isConnectedId, m_isConnected);
            }
        }

        private void MessageReceived(GroupValueEventArgs e)
        {
            string value = e.Value.ToString();

            //DTP 1x feedback
            if (gaDpt1x.ContainsValue(e.Address.ToString()))
            {
                string myKey = gaDpt1x.FirstOrDefault(x => x.Value == e.Address.ToString()).Key;

                DPT_1x Obj = (DPT_1x)m_dpt1xObjects[myKey];

                int pos = value.IndexOf("Value=") + 6;
                if (value.Substring(pos, 1) == "1")
                {
                    Obj.Feedback(true);
                }
                else if (value.Substring(pos, 1) == "0")
                {
                    Obj.Feedback(false);
                }
            }
            //DTP 2x feedback
            else if (gaDpt2x.ContainsValue(e.Address.ToString()))
            {
                string myKey = gaDpt2x.FirstOrDefault(x => x.Value == e.Address.ToString()).Key;

                DPT_2x Obj = (DPT_2x)m_dpt3xObjects[myKey];

                int pos = value.IndexOf("Value=") + 7;

                Obj.Feedback(int.Parse(value.Substring(pos, 2), System.Globalization.NumberStyles.HexNumber));
            }
            //DTP 3x feedback
            else if (gaDpt3x.ContainsValue(e.Address.ToString()))
            {
                string myKey = gaDpt3x.FirstOrDefault(x => x.Value == e.Address.ToString()).Key;

                DPT_3x Obj = (DPT_3x)m_dpt3xObjects[myKey];

                int pos = value.IndexOf("Value=") + 7;

                Obj.Feedback(int.Parse(value.Substring(pos, 2), System.Globalization.NumberStyles.HexNumber));
            }
            //DTP 4x feedback
            else if (gaDpt4x.ContainsValue(e.Address.ToString()))
            {
                string myKey = gaDpt4x.FirstOrDefault(x => x.Value == e.Address.ToString()).Key;

                DPT_3x Obj = (DPT_3x)m_dpt3xObjects[myKey];

                int pos = value.IndexOf("Value=") + 7;

                Obj.Feedback(int.Parse(value.Substring(pos, 2), System.Globalization.NumberStyles.HexNumber));
            }
            //DTP 5x feedback
            else if (gaDpt5x.ContainsValue(e.Address.ToString()))
            {
                string myKey = gaDpt5x.FirstOrDefault(x => x.Value == e.Address.ToString()).Key;

                DPT_5x Obj = (DPT_5x)m_dpt5xObjects[myKey];

                int pos = value.IndexOf("Value=") + 7;

                Obj.Feedback(int.Parse(value.Substring(pos, 2), System.Globalization.NumberStyles.HexNumber));
            }
            //DTP 6x feedback
            else if (gaDpt6x.ContainsValue(e.Address.ToString()))
            {
                string myKey = gaDpt6x.FirstOrDefault(x => x.Value == e.Address.ToString()).Key;

                DPT_6x Obj = (DPT_6x)m_dpt6xObjects[myKey];

                int pos = value.IndexOf("Value=") + 7;

                Obj.Feedback(int.Parse(value.Substring(pos, 2), System.Globalization.NumberStyles.HexNumber));
            }
            //DTP 7x feedback
            else if (gaDpt7x.ContainsValue(e.Address.ToString()))
            {
                string myKey = gaDpt7x.FirstOrDefault(x => x.Value == e.Address.ToString()).Key;

                DPT_7x Obj = (DPT_7x)m_dpt7xObjects[myKey];

                int pos = value.IndexOf("Value=") + 7;
                string valueString = value.Substring(pos, 2) + value.Substring(pos + 5, 2);

                Obj.Feedback(int.Parse(valueString, System.Globalization.NumberStyles.HexNumber));
            }
        }

        internal void oneBitCommand(string groupAddress, bool value)
        {
            if (bus.IsConnected)
            {
                bus.WriteValue(groupAddress, new GroupValue(value));
            }
        }
        internal void oneByteCommand(string groupAddress, int value)
        {
            if (bus.IsConnected)
            {
                Byte valueByte = Convert.ToByte(value);
                bus.WriteValue(groupAddress, new GroupValue(valueByte));
            }
        }
        internal void twoByteCommand(string groupAddress, int value)
        {
            if (bus.IsConnected)
            {
                Byte[] valueByte = BitConverter.GetBytes(value);
                bus.WriteValue(groupAddress, new GroupValue(valueByte));
            }
        }

        internal void CharCommand(string groupAddress, string value, int type)
        {
            if (bus.IsConnected)
            {
                Byte valueByte = new byte();
                char valueChar = Convert.ToChar(value);
                if (type == 1)
                {
                    valueByte = Convert.ToByte((int)valueChar);
                }
                else if (type == 2)
                {
                    Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                    Encoding utf8 = Encoding.UTF8;
                    byte[] utfBytes = utf8.GetBytes(value);
                    byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
                    valueByte = isoBytes[0];
                }

                bus.WriteValue(groupAddress, new GroupValue(valueByte));
            }
        }

        internal void ReadValue(string groupAddress)
        {
            if (bus.IsConnected)
            {
                bus.ReadValue(groupAddress);
            }
        }

        public bool PropertiesEx(bool bIsInstance, int iWnd)
        {
            import.SetParent(this);
            import.OpenImport();
            return true;
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

            if (pSite != null)
            {
                MyPrsObject = pSite as IPrsObject;
                if (MyPrsObject != null)
                {
                    MyPrsApplication = MyPrsObject.Application;
                    MyFullName = MyPrsObject.FullName;
                }
            }
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
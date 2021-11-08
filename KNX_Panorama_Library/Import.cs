using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CODRA.Panorama.Persist;
using CODRA.SDK.DotNetUtils.COM;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Panorama_KNX
{
    class Import
    {
        public static importForm importForm = new importForm();

        private KNX_Panorama m_pParent = null;

        public Import()
        {
            importForm.buttonGenerate_ClickEvent += buttonGenerate_Click;
        }

        #region enum list
        enum DptName1
        {
            Switch = 1,
            Bool = 2,
            Enable = 3,
            Ramp = 4,
            Alarm = 5,
            BinaryValue = 6,
            Step = 7,
            UpDown = 8,
            OpenClose = 9,
            Start = 10,
            State = 11,
            Invert = 12,
            DimSendStyle = 13,
            InputSource = 14,
            Reset = 15,
            Ack = 16,
            Trigger = 17,
            Occupancy = 18,
            Windows_Door = 19,
            LogicalFunction = 20,
            Scene_AB = 22,
            ShutterBlinds_Mode = 23,
            HeatCool = 100
        };

        enum DptName2 : int
        {
            Switch_Control = 1,
            Bool_Control = 2,
            Enable_Control = 3,
            Ramp_Control = 4,
            Alarm_Control = 5,
            BinaryValue_Control = 6,
            Step_Control = 7,
            Direction1_Control = 8,
            Direction2_Control = 9,
            Start_Control = 10,
            State_Control = 11,
            Invert_Control = 12
        }

        enum DptName3 : int
        {
            Control_Dimming = 7,
            Control_Blinds = 8
        }
        enum DptName4 : int
        {
            Char_ASCII = 1,
            Char_8859_1 = 2
        }

        enum DptName5 : int
        {
            Scaling = 1,
            Angle = 3,
            Percent_U8 = 4,
            DecimalFactor = 5,
            Tariff = 6,
            Value_1_Ucount = 10
        }

        enum DptName6 : int
        {
            Percent_V8 = 1,
            Value_1_Count = 10,
            Status_Mode3 = 20,
        }
        enum DptName7 : int
        {
            Value_2_Ucount = 1,
            TimePeriodMsec = 2,
            TimePeriod10Msec = 3,
            TimePeriod100Msec = 4,
            TimePeriodSec = 5,
            TimePeriodMin = 6,
            TimePeriodHrs = 7,
            PropDataType = 10,
            Length_mm = 11,
            UEICurrentmA = 12,
            Brightness = 13
        }

        enum DptName17 : int
        {
            SceneNumber = 1
        }

        enum DptName18 : int
        {
            SceneControl = 1
        }
        #endregion

        internal void SetParent(KNX_Panorama pParent)
        {
            m_pParent = pParent;
        }

        internal void OpenImport()
        {
            importForm.ShowDialog();
        }

        private void buttonGenerate_Click(object source, EventArgs e)
        {

            XmlDocument doc = new XmlDocument();

            try
            {
                if (File.Exists(@importForm.xmlFilePath))
                {
                    importForm.AddLineFeedback("Ouverture du fichier XML");
                    doc.Load(@importForm.xmlFilePath);
                    importForm.AddLineFeedback("Fichier XML ouvert");

                    XmlNodeList nodeList = doc.GetElementsByTagName("GroupAddress");

                    foreach (XmlNode element in nodeList)
                    {
                        IPrsObject MySubObj;
                        string dpt = "", groupAddress = "", name = "", objectName = "";
                        XmlAttributeCollection attributes = element.Attributes;

                        foreach (XmlAttribute attribute in attributes)
                        {
                            switch (attribute.Name)
                            {
                                case "DPTs": dpt = attribute.Value; break;
                                case "Address": groupAddress = attribute.Value; break;
                                case "Name":
                                    {
                                        name = attribute.Value;
                                        objectName = CleanInput(name);
                                        objectName = objectName.Replace('é', 'e');
                                        objectName = objectName.Replace('è', 'e');
                                        objectName = objectName.Replace('ê', 'e');
                                        objectName = objectName.Replace('à', 'a');
                                        objectName = objectName.Replace('â', 'a');
                                        objectName = objectName.Replace('ç', 'c');

                                        break;
                                    }
                                default: break;
                            }
                        }
                        objectName = "GA_" + groupAddress.Replace(@"/", "_") + "_" + objectName;
                        if (dpt != "")
                        {
                            string[] dptSplit = dpt.Split('-');
                            if (dptSplit[1] == "1" || dptSplit[1] == "2" || dptSplit[1] == "3" || dptSplit[1] == "4" || dptSplit[1] == "5" || dptSplit[1] == "6" || dptSplit[1] == "7" || dptSplit[1] == "17")
                            {
                                try
                                {
                                    MySubObj = m_pParent.MyPrsObject.CollectionEx["DPT_" + dptSplit[1] + "x_Objects"].get_ItemEx(objectName);
                                    importForm.AddLineFeedback("Ouverture sous-objet " + objectName);
                                }
                                catch
                                {
                                    MySubObj = m_pParent.MyPrsObject.CollectionEx["DPT_" + dptSplit[1] + "x_Objects"].AddEx(objectName);
                                    MySubObj.ModuleName = "KNX";
                                    MySubObj.ClassName = "DPT_" + dptSplit[1] + "x";
                                    MySubObj.Value["DPT", 0] = dpt;
                                    switch (dptSplit[1])
                                    {
                                        case "1": MySubObj.Value["DPT_Name", 0] = ((DptName1)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "2": MySubObj.Value["DPT_Name", 0] = ((DptName2)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "3": MySubObj.Value["DPT_Name", 0] = ((DptName4)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "4": MySubObj.Value["DPT_Name", 0] = ((DptName3)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "5": MySubObj.Value["DPT_Name", 0] = ((DptName5)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "6": MySubObj.Value["DPT_Name", 0] = ((DptName5)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "7": MySubObj.Value["DPT_Name", 0] = ((DptName5)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "17": MySubObj.Value["DPT_Name", 0] = ((DptName17)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        case "18": MySubObj.Value["DPT_Name", 0] = ((DptName18)Convert.ToInt32(dptSplit[2])).ToString(); break;
                                        default: break;
                                    }
                                    MySubObj.Value["Group_Address", 0] = groupAddress;
                                    MySubObj.Value["Object_Name", 0] = name;
                                    importForm.AddLineFeedback("Création sous-objet " + objectName);                                    
                                }
                            }
                            else
                            {
                                importForm.AddLineFeedback("Type d'objet non pris en charge " + objectName);
                            }
                        }
                        else
                        {
                            importForm.AddLineFeedback("Data point type manquant " + objectName);
                        }
                    }
                    m_pParent.MyPrsApplication.Save();
                    importForm.AddLineFeedback("Import terminé");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur création application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                m_pParent.MyPrsApplication.Unload();
            }
        }

        static string CleanInput(string strIn)
        {
            // Replace invalid characters with "_" strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w]", "_", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}
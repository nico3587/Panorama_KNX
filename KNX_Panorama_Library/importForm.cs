using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panorama_KNX
{
    public partial class importForm : Form
    {
        public delegate void GenerateEventHandler(object source, EventArgs e);

        public event GenerateEventHandler buttonGenerate_ClickEvent;

        internal string xmlFilePath;

        public importForm()
        {
            InitializeComponent();
        }

        private void importForm_Load(object sender, EventArgs e)
        {
            xmlFilePath = textBoxXmlPath.Text;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            textBoxFeedback.Text = "";
            buttonGenerate_ClickEvent?.Invoke(this, EventArgs.Empty);
        }

        private void buttonFolderXml_Click(object sender, EventArgs e)
        {
            openFileDialogXml.ShowDialog();
            textBoxXmlPath.Text = openFileDialogXml.FileName;
        }

        internal void AddLineFeedback(string value)
        {
            textBoxFeedback.Text = textBoxFeedback.Text + value + "\x0d\x0a";
        }

        private void textBoxXmlPath_TextChanged(object sender, EventArgs e)
        {
            xmlFilePath = textBoxXmlPath.Text;
        }
    }
}

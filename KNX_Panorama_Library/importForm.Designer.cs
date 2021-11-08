
namespace Panorama_KNX
{
    partial class importForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(importForm));
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.textBoxFeedback = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonFolderXml = new System.Windows.Forms.Button();
            this.textBoxXmlPath = new System.Windows.Forms.TextBox();
            this.openFileDialogXml = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(1151, 49);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(208, 70);
            this.buttonGenerate.TabIndex = 15;
            this.buttonGenerate.Text = "Importation";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // textBoxFeedback
            // 
            this.textBoxFeedback.AcceptsReturn = true;
            this.textBoxFeedback.Location = new System.Drawing.Point(21, 164);
            this.textBoxFeedback.Multiline = true;
            this.textBoxFeedback.Name = "textBoxFeedback";
            this.textBoxFeedback.ReadOnly = true;
            this.textBoxFeedback.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFeedback.Size = new System.Drawing.Size(1338, 436);
            this.textBoxFeedback.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 37);
            this.label2.TabIndex = 13;
            this.label2.Text = "ETS XML file";
            // 
            // buttonFolderXml
            // 
            this.buttonFolderXml.Location = new System.Drawing.Point(1047, 73);
            this.buttonFolderXml.Name = "buttonFolderXml";
            this.buttonFolderXml.Size = new System.Drawing.Size(58, 46);
            this.buttonFolderXml.TabIndex = 12;
            this.buttonFolderXml.Text = "...";
            this.buttonFolderXml.UseVisualStyleBackColor = true;
            this.buttonFolderXml.Click += new System.EventHandler(this.buttonFolderXml_Click);
            // 
            // textBoxXmlPath
            // 
            this.textBoxXmlPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxXmlPath.Location = new System.Drawing.Point(21, 73);
            this.textBoxXmlPath.Name = "textBoxXmlPath";
            this.textBoxXmlPath.Size = new System.Drawing.Size(990, 49);
            this.textBoxXmlPath.TabIndex = 11;
            this.textBoxXmlPath.TextChanged += new System.EventHandler(this.textBoxXmlPath_TextChanged);
            // 
            // openFileDialogXml
            // 
            this.openFileDialogXml.FileName = "openFileDialogXml";
            this.openFileDialogXml.Filter = "\"Xml file\"|*.xml";
            // 
            // importForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 624);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.textBoxFeedback);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonFolderXml);
            this.Controls.Add(this.textBoxXmlPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "importForm";
            this.Text = "Import KNX";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TextBox textBoxFeedback;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonFolderXml;
        private System.Windows.Forms.TextBox textBoxXmlPath;
        private System.Windows.Forms.OpenFileDialog openFileDialogXml;
    }
}
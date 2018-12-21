namespace PASS
{
    partial class FormPASS
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
            this.txtMain = new System.Windows.Forms.TextBox();
            this.lblAccess = new System.Windows.Forms.Label();
            this.txtAccess = new System.Windows.Forms.TextBox();
            this.btnAccess = new System.Windows.Forms.Button();
            this.lblSave = new System.Windows.Forms.Label();
            this.txtSave = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRandomize = new System.Windows.Forms.Button();
            this.cboSaveHash = new System.Windows.Forms.ComboBox();
            this.lblOut = new System.Windows.Forms.Label();
            this.pnlOut = new System.Windows.Forms.Panel();
            this.lblOutText = new System.Windows.Forms.Label();
            this.pnlOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMain
            // 
            this.txtMain.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMain.Location = new System.Drawing.Point(386, 12);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(797, 664);
            this.txtMain.TabIndex = 0;
            this.txtMain.Text = "Here is where the text will appear.";
            // 
            // lblAccess
            // 
            this.lblAccess.AutoSize = true;
            this.lblAccess.Location = new System.Drawing.Point(12, 18);
            this.lblAccess.Name = "lblAccess";
            this.lblAccess.Size = new System.Drawing.Size(73, 13);
            this.lblAccess.TabIndex = 1;
            this.lblAccess.Text = "Access Code:";
            // 
            // txtAccess
            // 
            this.txtAccess.Location = new System.Drawing.Point(15, 35);
            this.txtAccess.Name = "txtAccess";
            this.txtAccess.Size = new System.Drawing.Size(365, 20);
            this.txtAccess.TabIndex = 2;
            // 
            // btnAccess
            // 
            this.btnAccess.Location = new System.Drawing.Point(15, 61);
            this.btnAccess.Name = "btnAccess";
            this.btnAccess.Size = new System.Drawing.Size(136, 23);
            this.btnAccess.TabIndex = 3;
            this.btnAccess.Text = "Gain Access";
            this.btnAccess.UseVisualStyleBackColor = true;
            // 
            // lblSave
            // 
            this.lblSave.AutoSize = true;
            this.lblSave.Location = new System.Drawing.Point(12, 107);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(104, 13);
            this.lblSave.TabIndex = 4;
            this.lblSave.Text = "Save with this Code:";
            // 
            // txtSave
            // 
            this.txtSave.Location = new System.Drawing.Point(15, 124);
            this.txtSave.Name = "txtSave";
            this.txtSave.Size = new System.Drawing.Size(362, 20);
            this.txtSave.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 178);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnRandomize
            // 
            this.btnRandomize.Location = new System.Drawing.Point(237, 149);
            this.btnRandomize.Name = "btnRandomize";
            this.btnRandomize.Size = new System.Drawing.Size(140, 23);
            this.btnRandomize.TabIndex = 7;
            this.btnRandomize.Text = "Generate Random Code";
            this.btnRandomize.UseVisualStyleBackColor = true;
            // 
            // cboSaveHash
            // 
            this.cboSaveHash.FormattingEnabled = true;
            this.cboSaveHash.Location = new System.Drawing.Point(15, 151);
            this.cboSaveHash.Name = "cboSaveHash";
            this.cboSaveHash.Size = new System.Drawing.Size(133, 21);
            this.cboSaveHash.TabIndex = 8;
            this.cboSaveHash.Text = "Select Hash Function...";
            // 
            // lblOut
            // 
            this.lblOut.AutoSize = true;
            this.lblOut.Location = new System.Drawing.Point(12, 221);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(76, 13);
            this.lblOut.TabIndex = 9;
            this.lblOut.Text = "System Output";
            // 
            // pnlOut
            // 
            this.pnlOut.Controls.Add(this.lblOutText);
            this.pnlOut.Location = new System.Drawing.Point(15, 237);
            this.pnlOut.Name = "pnlOut";
            this.pnlOut.Size = new System.Drawing.Size(359, 439);
            this.pnlOut.TabIndex = 10;
            // 
            // lblOutText
            // 
            this.lblOutText.Location = new System.Drawing.Point(14, 14);
            this.lblOutText.Name = "lblOutText";
            this.lblOutText.Size = new System.Drawing.Size(333, 412);
            this.lblOutText.TabIndex = 0;
            this.lblOutText.Text = "Welcome to the PASS utility!";
            // 
            // FormPASS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 688);
            this.Controls.Add(this.pnlOut);
            this.Controls.Add(this.lblOut);
            this.Controls.Add(this.cboSaveHash);
            this.Controls.Add(this.btnRandomize);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSave);
            this.Controls.Add(this.lblSave);
            this.Controls.Add(this.btnAccess);
            this.Controls.Add(this.txtAccess);
            this.Controls.Add(this.lblAccess);
            this.Controls.Add(this.txtMain);
            this.Name = "FormPASS";
            this.Text = "PASS Utility";
            this.pnlOut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.Label lblAccess;
        private System.Windows.Forms.TextBox txtAccess;
        private System.Windows.Forms.Button btnAccess;
        private System.Windows.Forms.Label lblSave;
        private System.Windows.Forms.TextBox txtSave;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRandomize;
        private System.Windows.Forms.ComboBox cboSaveHash;
        private System.Windows.Forms.Label lblOut;
        private System.Windows.Forms.Panel pnlOut;
        private System.Windows.Forms.Label lblOutText;
    }
}


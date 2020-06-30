namespace MaterialView
{
    partial class View
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.TbSn = new System.Windows.Forms.TextBox();
            this.TbMake = new System.Windows.Forms.TextBox();
            this.TbModel = new System.Windows.Forms.TextBox();
            this.TbLocationId = new System.Windows.Forms.TextBox();
            this.TbPersonId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.TbOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Hinzufügen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TbSn
            // 
            this.TbSn.Location = new System.Drawing.Point(13, 42);
            this.TbSn.Name = "TbSn";
            this.TbSn.Size = new System.Drawing.Size(100, 22);
            this.TbSn.TabIndex = 1;
            // 
            // TbMake
            // 
            this.TbMake.Location = new System.Drawing.Point(136, 41);
            this.TbMake.Name = "TbMake";
            this.TbMake.Size = new System.Drawing.Size(100, 22);
            this.TbMake.TabIndex = 2;
            // 
            // TbModel
            // 
            this.TbModel.Location = new System.Drawing.Point(270, 41);
            this.TbModel.Name = "TbModel";
            this.TbModel.Size = new System.Drawing.Size(100, 22);
            this.TbModel.TabIndex = 3;
            // 
            // TbLocationId
            // 
            this.TbLocationId.Location = new System.Drawing.Point(403, 42);
            this.TbLocationId.Name = "TbLocationId";
            this.TbLocationId.Size = new System.Drawing.Size(100, 22);
            this.TbLocationId.TabIndex = 4;
            // 
            // TbPersonId
            // 
            this.TbPersonId.Location = new System.Drawing.Point(557, 41);
            this.TbPersonId.Name = "TbPersonId";
            this.TbPersonId.Size = new System.Drawing.Size(100, 22);
            this.TbPersonId.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "S/N";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Marke";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Modell";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ort ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(557, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Personen ID";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(478, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 39);
            this.button2.TabIndex = 11;
            this.button2.Text = "Get";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TbOutput
            // 
            this.TbOutput.Location = new System.Drawing.Point(13, 221);
            this.TbOutput.Name = "TbOutput";
            this.TbOutput.Size = new System.Drawing.Size(756, 207);
            this.TbOutput.TabIndex = 12;
            this.TbOutput.Text = "";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TbOutput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbPersonId);
            this.Controls.Add(this.TbLocationId);
            this.Controls.Add(this.TbModel);
            this.Controls.Add(this.TbMake);
            this.Controls.Add(this.TbSn);
            this.Controls.Add(this.button1);
            this.Name = "View";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TbSn;
        private System.Windows.Forms.TextBox TbMake;
        private System.Windows.Forms.TextBox TbModel;
        private System.Windows.Forms.TextBox TbLocationId;
        private System.Windows.Forms.TextBox TbPersonId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox TbOutput;
    }
}


namespace proxyClickerSerious
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageID = new System.Windows.Forms.TextBox();
            this.numberOfClicks = new System.Windows.Forms.TextBox();
            this.URL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.ipList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.portList = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ipAdress = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageID
            // 
            this.imageID.Location = new System.Drawing.Point(106, 11);
            this.imageID.Name = "imageID";
            this.imageID.Size = new System.Drawing.Size(172, 20);
            this.imageID.TabIndex = 0;
            // 
            // numberOfClicks
            // 
            this.numberOfClicks.Location = new System.Drawing.Point(106, 37);
            this.numberOfClicks.Name = "numberOfClicks";
            this.numberOfClicks.Size = new System.Drawing.Size(172, 20);
            this.numberOfClicks.TabIndex = 1;
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(106, 63);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(172, 20);
            this.URL.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Resim Link :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Basılacak Sayı :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "URL :";
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(152, 89);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 6;
            this.start.Text = "Başlat";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 188);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(776, 354);
            this.webBrowser1.TabIndex = 7;
            // 
            // ipList
            // 
            this.ipList.FormattingEnabled = true;
            this.ipList.Location = new System.Drawing.Point(445, 37);
            this.ipList.Name = "ipList";
            this.ipList.Size = new System.Drawing.Size(120, 95);
            this.ipList.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(17, 16);
            this.button1.TabIndex = 9;
            this.button1.Text = "I";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // portList
            // 
            this.portList.FormattingEnabled = true;
            this.portList.Location = new System.Drawing.Point(595, 37);
            this.portList.Name = "portList";
            this.portList.Size = new System.Drawing.Size(120, 95);
            this.portList.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(539, 159);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Ekle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ipAdress
            // 
            this.ipAdress.Location = new System.Drawing.Point(465, 138);
            this.ipAdress.Name = "ipAdress";
            this.ipAdress.Size = new System.Drawing.Size(100, 20);
            this.ipAdress.TabIndex = 12;
            this.ipAdress.Text = "IP Adresi";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(595, 138);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(100, 20);
            this.port.TabIndex = 13;
            this.port.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(542, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 26);
            this.label4.TabIndex = 14;
            this.label4.Text = "Proxy Adresleri\r\n\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 554);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ipAdress);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.portList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ipList);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.start);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URL);
            this.Controls.Add(this.numberOfClicks);
            this.Controls.Add(this.imageID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Proxy Ad Clicker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox imageID;
        private System.Windows.Forms.TextBox numberOfClicks;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ListBox ipList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox portList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox ipAdress;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label label4;
    }
}


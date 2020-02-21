using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace proxyClickerSerious
{
    public partial class Form1 : Form
    {
        RegistryKey reg_key;
        Uri proxyLink = new Uri("https://free-proxy-list.net");
        public Form1()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (URL.Text == "" || URL.Text == " " || numberOfClicks.Text == "" || numberOfClicks.Text == " " || imageID.Text == "" || imageID.Text == " ")
            {
                MessageBox.Show("3 kutudan hiçbiri boş olamaz!", "Proxy Clicker");
                return;
            }
            else if (Int32.Parse(numberOfClicks.Text) > ipList.Items.Count || Int32.Parse(numberOfClicks.Text) > portList.Items.Count)
            {
                MessageBox.Show("Bilgi kutusuna tıklayın!","Proxy Clicker");
                return;
            }
            else
            {
                //Verilen urlyeri string tipinden Uri tipine çevir.
                webBrowser1.Url = new Uri(URL.Text);
                URL.Text = "";
                for(int i = 0; i < Int32.Parse(numberOfClicks.Text); i++)
                {
                    // Proxy Sunucusu Değiştir

                    reg_key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",true);
                    string proxy = ipList.Items[i].ToString() +":"+ portList.Items[i].ToString();
                    reg_key.SetValue("ProxyEnable", 1);
                    reg_key.SetValue("ProxyServer", proxy);

                    // Reklama Tıkla
					
                    webBrowser1.Navigate(webBrowser1.Url);
                    webBrowser1.DocumentCompleted += wb_islemTamamlandi;
                }

                //İşlemin bittiğini yaz
                MessageBox.Show("İşlem tamamlandı! "+ imageID +" idli nesneye "+ numberOfClicks +" kez tıklandı!","Proxy Clicker");

                //İşlem bittikten sonra proxy sunucularından çık
                reg_key.SetValue("ProxyEnable", 0);
                reg_key.SetValue("ProxyServer", "");

                //Proxy Listesini Temizle
                ipList.Items.Clear();
                portList.Items.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Ekle tuşuna basıldığında proxy yapacak!
            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate(@proxyLink);
            webBrowser1.DocumentCompleted += wb_DocumentCompleted;

            // Ekle tuşunu otomatik yapmak için alttaki inputları şuanlık kaldırdım.

            /*int parsedValue;
            if (ipAdress.Text == "" || ipAdress.Text == " " || port.Text == "" || port.Text == " ")
            {
                MessageBox.Show("IP Adresi ve PORT girme zorunluluğu vardır.", "Proxy Clicker");
                return;
            }
            else
            {
                ipList.Items.Add(ipAdress.Text);
                portList.Items.Add(port.Text);
                ipAdress.Text = "";
                port.Text = "";
            }*/
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Tablonun idsini al
            var table = webBrowser1.Document.GetElementById("proxylisttable");
            
            foreach (HtmlElement elem in webBrowser1.Document.GetElementsByTagName("table"))
            {
                if (elem.GetAttribute("id") == "proxylisttable")
                {
                    foreach (HtmlElement elem2 in webBrowser1.Document.GetElementsByTagName("tr"))
                    {
                        //En fazla 3 children alabilir diyor. Burada bi sıkıntımız var.
                            ipList.Items.Add(table.Children[0].InnerText.ToString());
                            portList.Items.Add(table.Children[1].InnerText.ToString());                   
                    }
                }
                else
                {
                    //Hatayı yakalamamız lazım. Yapılacak.
                    MessageBox.Show("En güncel proxy sunucuları eklendi.", "Proxy Clicker");
                    return;
                }
            }

            //Başarıyla yapıldı fakat blank'e götürmüyor.
            MessageBox.Show("En güncel proxy sunucuları eklendi.", "Proxy Clicker");
            webBrowser1.Navigate("about:blank");
        }
        private void wb_islemTamamlandi(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Her fotoğraf
            foreach (HtmlElement elem in webBrowser1.Document.GetElementsByTagName("img"))
            {
                //Src linki verilen url olan
                if (elem.GetAttribute("src") == imageID.Text)
                {
                    //Tıkla
                    elem.InvokeMember("Click");
                }
                else
                {
                    MessageBox.Show("İşlem sırasında bir sıkıntı çıktı! Verdiğiniz link doğru olmayabilir...", "Proxy Clicker");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bilgi vermek için
            MessageBox.Show("Bu yazı yerine proxy iplerinin sayısına kadar yazılabilir.", "Proxy Clicker");
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

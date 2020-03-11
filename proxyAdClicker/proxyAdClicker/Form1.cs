using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace proxyClickerSerious
{

    public partial class Form1 : Form
    {

        #region Definitions/DLL Imports
        /// <summary>
        /// For PInvoke: Contains information about an entry in the Internet cache
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct ExemptDeltaOrReserverd
        {
            [FieldOffset(0)]
            public UInt32 dwReserved;
            [FieldOffset(0)]
            public UInt32 dwExemptDelta;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNET_CACHE_ENTRY_INFOA
        {
            public UInt32 dwStructSize;
            public IntPtr lpszSourceUrlName;
            public IntPtr lpszLocalFileName;
            public UInt32 CacheEntryType;
            public UInt32 dwUseCount;
            public UInt32 dwHitRate;
            public UInt32 dwSizeLow;
            public UInt32 dwSizeHigh;
            public FILETIME LastModifiedTime;
            public FILETIME ExpireTime;
            public FILETIME LastAccessTime;
            public FILETIME LastSyncTime;
            public IntPtr lpHeaderInfo;
            public UInt32 dwHeaderInfoSize;
            public IntPtr lpszFileExtension;
            public ExemptDeltaOrReserverd dwExemptDeltaOrReserved;
        }

        // For PInvoke: Initiates the enumeration of the cache groups in the Internet cache
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindFirstUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstUrlCacheGroup(
            int dwFlags,
            int dwFilter,
            IntPtr lpSearchCondition,
        int dwSearchCondition,
        ref long lpGroupId,
        IntPtr lpReserved);

        // For PInvoke: Retrieves the next cache group in a cache group enumeration
        [DllImport(@"wininet",
        SetLastError = true,
            CharSet = CharSet.Auto,
        EntryPoint = "FindNextUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextUrlCacheGroup(
            IntPtr hFind,
            ref long lpGroupId,
            IntPtr lpReserved);

        // For PInvoke: Releases the specified GROUPID and any associated state in the cache index file
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "DeleteUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool DeleteUrlCacheGroup(
            long GroupId,
            int dwFlags,
            IntPtr lpReserved);

        // For PInvoke: Begins the enumeration of the Internet cache
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindFirstUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstUrlCacheEntry(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern,
            IntPtr lpFirstCacheEntryInfo,
            ref int lpdwFirstCacheEntryInfoBufferSize);

        // For PInvoke: Retrieves the next entry in the Internet cache
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindNextUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextUrlCacheEntry(
            IntPtr hFind,
            IntPtr lpNextCacheEntryInfo,
            ref int lpdwNextCacheEntryInfoBufferSize);

        // For PInvoke: Removes the file that is associated with the source name from the cache, if the file exists
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "DeleteUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool DeleteUrlCacheEntry(
            IntPtr lpszUrlName);
        #endregion

        /// <summary>
        /// Clears the cache of the web browser
        /// </summary>
        public static void ClearCache()
        {
            // Indicates that all of the cache groups in the user's system should be enumerated
            const int CACHEGROUP_SEARCH_ALL = 0x0;
            // Indicates that all the cache entries that are associated with the cache group
            // should be deleted, unless the entry belongs to another cache group.
            const int CACHEGROUP_FLAG_FLUSHURL_ONDELETE = 0x2;
            const int ERROR_INSUFFICIENT_BUFFER = 0x7A;

            // Delete the groups first.
            // Groups may not always exist on the system.
            // For more information, visit the following Microsoft Web site:
            // http://msdn.microsoft.com/library/?url=/workshop/networking/wininet/overview/cache.asp            
            // By default, a URL does not belong to any group. Therefore, that cache may become
            // empty even when the CacheGroup APIs are not used because the existing URL does not belong to any group.            
            long groupId = 0;
            IntPtr enumHandle = FindFirstUrlCacheGroup(0, CACHEGROUP_SEARCH_ALL, IntPtr.Zero, 0, ref groupId, IntPtr.Zero);
            if (enumHandle != IntPtr.Zero)
            {
                bool more;
                do
                {
                    // Delete a particular Cache Group.
                    DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero);
                    more = FindNextUrlCacheGroup(enumHandle, ref groupId, IntPtr.Zero);
                } while (more);
            }

            // Start to delete URLs that do not belong to any group.
            int cacheEntryInfoBufferSizeInitial = 0;
            FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);  // should always fail because buffer is too small
            if (Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
            {
                int cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                IntPtr cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
                enumHandle = FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                if (enumHandle != IntPtr.Zero)
                {
                    bool more;
                    do
                    {
                        INTERNET_CACHE_ENTRY_INFOA internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));
                        cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;
                        DeleteUrlCacheEntry(internetCacheEntry.lpszSourceUrlName);
                        more = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                        if (!more && Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
                        {
                            cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                            cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
                            more = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                        }
                    } while (more);
                }
                Marshal.FreeHGlobal(cacheEntryInfoBuffer);
            }
        }

        RegistryKey reg_key;
        Uri proxyLink = new Uri("https://free-proxy-list.net");
        int tiklandiSayi = 0;
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
                MessageBox.Show("Bilgi kutusuna tıklayın!", "Proxy Clicker");
                return;
            }
            else
            {
                int git = int.Parse(numberOfClicks.Text);
                for (int i = 0; i < git; i++)
                {
                    webBrowser1.DocumentCompleted += wb_islemTamamlandi2;
                    ClearCache();
                    webBrowser1.Navigate(new Uri(URL.Text));

                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }

                    // Proxy Sunucusu Değiştir
                    reg_key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
                    string proxy = ipList.Items[i].ToString() + ":" + portList.Items[i].ToString();
                    reg_key.SetValue("ProxyEnable", 1);
                    reg_key.SetValue("ProxyServer", proxy);

                    // Her fotoğraf
                    foreach (HtmlElement elem in webBrowser1.Document.GetElementsByTagName("a"))
                    {
                        //Src linki verilen url olan    
                        if (elem.GetAttribute("href").ToString() == imageID.Text)
                        {
                            //Tıkla
                            elem.InvokeMember("Click");
                            tiklandiSayi++;
                        }
                        else
                        {

                        }
                    }


                }

                //İşlemin bittiğini yaz
                MessageBox.Show("İşlem tamamlandı! " + imageID.Text + " urlli nesneye " + tiklandiSayi + " kez tıklandı!", "Proxy Clicker");

                //İşlem bittikten sonra proxy sunucularından çık
                reg_key.SetValue("ProxyEnable", 0);
                reg_key.SetValue("ProxyServer", "" + ":" + "");

                //Proxy Listesini Temizle
                ipList.Items.Clear();
                portList.Items.Clear();

                boslugaGotur();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            }
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Tabloyu al.
            HtmlElement tablo = webBrowser1.Document.GetElementById("proxylisttable");
            // Satırları Al
            HtmlElementCollection satirlar = tablo.GetElementsByTagName("tbody")[0].Children;
            // Hücre Değişkenini Oluştur
            HtmlElementCollection hucreler;

            for (int i = 1; i < satirlar.Count; i++)
            {
                // Satırların inci elemanındaki hücreleri(tdleri) al
                hucreler = satirlar[i].GetElementsByTagName("td");
                ipList.Items.Add(hucreler[0].InnerText);
                portList.Items.Add(hucreler[1].InnerText);
            }

            //Başarıyla yapıldı fakat blank'e götürmüyor.
            MessageBox.Show("En güncel proxy sunucuları eklendi.", "Proxy Clicker");
            boslugaGotur();
            webBrowser1.DocumentCompleted -= wb_DocumentCompleted;

        }
        private void wb_islemYapiliyor(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Application.DoEvents();
            webBrowser1.DocumentCompleted -= wb_islemYapiliyor;
        }

        private void wb_islemTamamlandi(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Application.DoEvents();
            ClearCache();
            webBrowser1.DocumentCompleted -= wb_islemTamamlandi;
        }
        private void wb_islemTamamlandi2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
            ClearCache();
            Application.DoEvents();
            webBrowser1.DocumentCompleted -= wb_islemTamamlandi2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Bilgi vermek için
            MessageBox.Show("Bu yazı yerine proxy iplerinin sayısına kadar yazılabilir.", "Proxy Clicker");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(new Uri(URL.Text));
            webBrowser1.DocumentCompleted += wb_islemTamamlandi;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Ekle tuşuna basıldığında proxy eklenecek!
            webBrowser1.AllowNavigation = true;
            //Site tablosundan otomatik proxy listesi almak için
            webBrowser1.Navigate(@proxyLink);
            webBrowser1.Refresh();
            ClearCache();

            webBrowser1.DocumentCompleted += wb_DocumentCompleted;
            ClearCache();
        }

        private void boslugaGotur()
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate(new Uri("about:blank"));
            webBrowser1.DocumentCompleted += wb_islemYapiliyor;
            ClearCache();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bedava proxy sitesinden 20 tane proxy ipsi ve portu ekler(10 dakikada bir site yenilenir).", "Proxy Clicker");
        }
    }
}

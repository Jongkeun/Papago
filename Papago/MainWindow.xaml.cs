using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using StringEx;

namespace Papago
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        PapagoApi api = new PapagoApi();
        public MainWindow()
        {
            InitializeComponent();
            SetKey();
        }

        // set API private id and secret key.
        private void SetKey()
        {
            string key, pwd;
            uint nSize = 260;
            StringBuilder sb = new StringBuilder(260); sb.Clear();
            NativeMethods.GetPrivateProfileString(PDefine.APP_NAME, "ID", string.Empty, sb, nSize, PDefine.CONFIGURATION);
            key = sb.ToString(); sb.Clear();
            NativeMethods.GetPrivateProfileString(PDefine.APP_NAME, "SECRET", string.Empty, sb, nSize, PDefine.CONFIGURATION);
            pwd = sb.ToString(); sb.Clear();
            //set API private id and secret key.
            api.SetKey(key, pwd);
        }

        // clicking a translation button.
        private async void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            string lang = await api.fnDetectLnaguage(txtInput.Text);
            Console.WriteLine(lang);
            txtOutput.Text = await api.fnTranslateLanguage(txtInput.Text);
        }

        //
        private async void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!txtInput.Text.Trim().IsNullOrEmpty())
                txtOutput.Text = await api.fnDetectLnaguage(txtInput.Text);
        }
    }
}

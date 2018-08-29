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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using StringEx;
using NHotkey;
using NHotkey.Wpf;

namespace Papago
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        PapagoApi api = new PapagoApi();
        //HotKey hotKey = new HotKey();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            SetKey();
            HotkeyManager.HotkeyAlreadyRegistered += HotkeyManager_HotkeyAlreadyRegistered;
            RegistHotKey();
        }

        private void HotkeyManager_HotkeyAlreadyRegistered(object sender, HotkeyAlreadyRegisteredEventArgs e)
        {
            MessageBox.Show(string.Format("The hotkey {0} is already registered by another application", e.Name));
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

        private void RegistHotKey()
        {
            try
            {
               HotkeyManager.Current.AddOrReplace("ShowUp", Key.Z, ModifierKeys.Control | ModifierKeys.Alt, ShowUp);
            }
            catch (Exception ex) { }
        }

        // clicking a translation button.
        private async void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            string lang = await api.fnDetectLnaguage(txtInput.Text);
            txtOutput.Text = await api.fnTranslateLanguage(txtInput.Text, lang, lang == "ko" ? "en" : "ko");
        }

        private async void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtInput.Text.Trim().IsNullOrEmpty())
                lbCurLang.Content = await api.fnDetectLnaguage(txtInput.Text);
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.None)
            {
                // translate when you press ENTER key.
                btnTranslate.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                // move to next line when you press SHIFT + ENTER keys.
                txtInput.Text += "\r\n";
                txtInput.CaretIndex = txtInput.Text.Length;
            }
        }

        private void ShowUp(object sender, HotkeyEventArgs e)
        {
            this.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            //hotKey.UnRegist(source.Handle);
        }
    }
}

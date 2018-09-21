﻿using System;
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

namespace Papago
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        PapagoApi api = new PapagoApi();
        PLangType Lang = new PLangType();
        Hotkey hotKey;
        System.Windows.Forms.NotifyIcon ni;
        bool isClose = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Init()
        {
            SetKey();
            RegistHotKey();
            NotifyIcon();
        }

        private void NotifyIcon()
        {
            System.Windows.Forms.ContextMenu conMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem mItem = new System.Windows.Forms.MenuItem();
            ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = Properties.Resources.papago;
            ni.Visible = false;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    SetVisible(true);
                };

            mItem.Text = "Exit";
            mItem.Click += new EventHandler((sender,e) =>
            {
                isClose = true;
                this.Close();
            });
            conMenu.MenuItems.Add(mItem);
            ni.ContextMenu = conMenu;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();

            //set focus to input box
            this.txtInput.Focus();
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
            hotKey = new Hotkey(Modifiers.Ctrl | Modifiers.Shift, Keys.A, this, registerImmediately: true);
            hotKey.HotkeyPressed += ((sender, e) => {
                if(this.IsActive)
                {
                    SetVisible(false);
                }
                else
                {
                    SetVisible(true);
                    PasteClipboard();
                    // translate when you press ENTER key.
                    btnTranslate.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            });
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
                lbCurLang.Content = Lang.LangMap[await api.fnDetectLnaguage(txtInput.Text)];
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClose)
            {
                ni.Visible = true;
                SetVisible(false);
                e.Cancel = true;
            }
        }

        private void PasteClipboard()
        {
            txtInput.Text = Clipboard.GetText();
            txtInput.SelectionStart = txtInput.Text.Length;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            hotKey.Dispose();
            ni.Visible = false;

        }

        private void SetVisible(bool isVisible)
        {
            if (isVisible)
            {
                this.WindowState = WindowState.Normal;
                this.Activate();
                this.txtInput.Focus();
            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }
        }
    }
}

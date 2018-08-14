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

namespace Papago
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(PDefine.BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(PDefine.X_HEADER_ID, "id");
            client.DefaultRequestHeaders.Add(PDefine.X_HEADER_SCRETE, "screte");
            Test(client);

        }
        private async void Test(HttpClient client)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("query", txtInput.Text)
            });
            var result = await client.PostAsync(PDefine.DETECT_API, content);
            string resultContent = await result.Content.ReadAsStringAsync();
            //return resultContent;
            Console.Write(resultContent);
            txtOutput.Text = resultContent;
        }
    }
}

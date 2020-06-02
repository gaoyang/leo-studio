using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using WpfLab.Models;

namespace WpfLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static readonly string phoneNumberPrefix = "+86";
        private static readonly string phoneNumber = "17621035207";

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var json = JsonSerializer.ToString(new { phoneNumberPrefix, phoneNumber });
            HttpClient client = new HttpClient();
            var content = new StringContent(json);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await client.PostAsync("http://localhost:59635/api/Register/SendSmsCaptcha", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
            }
        }
    }
}

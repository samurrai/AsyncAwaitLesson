using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitLesson
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetGamesListButtonClick(object sender, RoutedEventArgs e)
        {
            getGamesListButton.IsEnabled = false;

            try
            {
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://xboxapi.p.rapidapi.com/json/games/djekl");
                httpWebRequest.Headers.Add("X-RapidAPI-Host", "xboxapi.p.rapidapi.com");
                httpWebRequest.Headers.Add("X-RapidAPI-Key", "7be6bad869msh4a1275c8b247fb1p1e2dfdjsna3d2728f7718");
                HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string json = await reader.ReadToEndAsync();
                    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
                    firstGameLabel.Content = rootObject.Games[new Random().Next(0, 50)].Title;
                    firstGameDescription.Text = rootObject.Games[new Random().Next(0, 50)].Description;

                    secondGameLabel.Content = rootObject.Games[new Random().Next(51, 100)].Title;
                    secondGameDescription.Text = rootObject.Games[new Random().Next(51, 100)].Description;

                    thirdGameLabel.Content = rootObject.Games[new Random().Next(101, 151)].Title;
                    thirdGameDescription.Text = rootObject.Games[new Random().Next(101, 151)].Description;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удается получить информацию");
            }
            getGamesListButton.IsEnabled = true;
        }
    }
}

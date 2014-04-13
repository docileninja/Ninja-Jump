using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace SlXnaApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        static int highscore;

        public static int Highscore
        {
            get { return highscore; }
            set { if (value > highscore) { highscore = value; } }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Highscore = 1000;
            HighScore.Text = "Highscore:" + highscore.ToString();
        }

        // Simple button Click event handler to take us to the second page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }
    }
}
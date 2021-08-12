using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MADHouse
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class SelectSize : Window
    {
        LandingScreen mainWindow;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        public SelectSize(LandingScreen mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            TitleIconRemover.RemoveIcon(this);
        }

        private void LandSizeType_Click(object sender, RoutedEventArgs e)
        {
            if (LandDropDown.Visibility == Visibility.Hidden)
                LandDropDown.Visibility = Visibility.Visible;
            else
                LandDropDown.Visibility = Visibility.Hidden;
        }

        private void Marla5_Select(object sender, RoutedEventArgs e)
        {
            LandDropDown.Visibility = Visibility.Hidden;
            LandSizeType.Text = "5 marlas";
            landWidth.Text = "25";
            landLength.Text = "45";
            landWidth.IsReadOnly = true;
            landWidth.IsReadOnly = true;
            landWidth.Background = Brushes.LightGray;
            landLength.Background = Brushes.LightGray;
        }

        private void Marla10_Select(object sender, RoutedEventArgs e)
        {
            LandDropDown.Visibility = Visibility.Hidden;
            LandSizeType.Text = "10 marlas";
            landWidth.Text = "35";
            landLength.Text = "65";
            landWidth.IsReadOnly = true;
            landLength.IsReadOnly = true;
            landWidth.Background = Brushes.LightGray;
            landLength.Background = Brushes.LightGray;
        }

        private void Kanal1_Select(object sender, RoutedEventArgs e)
        {
            LandDropDown.Visibility = Visibility.Hidden;
            LandSizeType.Text = "1 kanal";
            landWidth.Text = "50";
            landLength.Text = "90";
            landWidth.IsReadOnly = true;
            landWidth.IsReadOnly = true;
            landWidth.Background = Brushes.LightGray;
            landLength.Background = Brushes.LightGray;
        }

        private void Kanal2_Select(object sender, RoutedEventArgs e)
        {
            LandDropDown.Visibility = Visibility.Hidden;
            LandSizeType.Text = "2 kanals";
            landWidth.Text = "75";
            landLength.Text = "120";
            landWidth.IsReadOnly = true;
            landWidth.IsReadOnly = true;
            landWidth.Background = Brushes.LightGray;
            landLength.Background = Brushes.LightGray;
        }

        private void Custom_Select(object sender, RoutedEventArgs e)
        {
            LandDropDown.Visibility = Visibility.Hidden;
            LandSizeType.Text = "Custom";
            landWidth.IsReadOnly = false;
            landWidth.IsReadOnly = false;
            landWidth.Background = Brushes.White;
            landLength.Background = Brushes.White;
        }

        private void Create_Project(object sender, RoutedEventArgs e)
        {
            int width = System.Convert.ToInt32(landWidth.Text);
            int length = System.Convert.ToInt32(landLength.Text);
            string name = ProjectName.Text.ToString();
            Project project = new Project(width, length, name);
            mainWindow.Close();
            this.Close();
            project.Show();
        }
    }
}

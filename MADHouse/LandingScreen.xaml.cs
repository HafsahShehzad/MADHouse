using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MADHouse
{
    /// <summary>
    /// Interaction logic for LandingScreen.xaml
    /// </summary>
    public partial class LandingScreen : Window
    {
        public LandingScreen()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            TitleIconRemover.RemoveIcon(this);
        }

        private void CreateNewProject(object sender, RoutedEventArgs e)
        {
            SelectSize proj = new SelectSize(this);
            this.Close();
            proj.Show();

        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            //get folders
            //load existing canvas 

        }
    }
}

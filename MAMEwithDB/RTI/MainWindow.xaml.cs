using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using RTI.Properties;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RTI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var userPrefs = new UserPreferences();

            this.Height = userPrefs.WindowHeight;
            this.Width = userPrefs.WindowWidth;
            this.Top = userPrefs.WindowTop;
            this.Left = userPrefs.WindowLeft;
            this.WindowState = userPrefs.WindowState;
        }
       
        private void ModernWindow_Closed(object sender, EventArgs e)
        {
            
            Settings.Default.ChosenTheme = AppearanceManager.Current.ThemeSource;
            Settings.Default.ChosenFontSize = AppearanceManager.Current.FontSize;
            Settings.Default.ChosenAccent = AppearanceManager.Current.AccentColor;

            var userPrefs = new UserPreferences();

            userPrefs.WindowHeight = this.Height;
            userPrefs.WindowWidth = this.Width;
            userPrefs.WindowTop = this.Top;
            userPrefs.WindowLeft = this.Left;
            userPrefs.WindowState = this.WindowState;

            userPrefs.Save();

            Settings.Default.Save();
        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.ChosenTheme != null)

            AppearanceManager.Current.ThemeSource = Settings.Default.ChosenTheme;
            AppearanceManager.Current.FontSize = Settings.Default.ChosenFontSize;
            AppearanceManager.Current.AccentColor = Settings.Default.ChosenAccent;
            
        }
    }
}

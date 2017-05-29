using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
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
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RTI.Pages
{
    /// <summary>
    /// Interaction logic for landing.xaml
    /// </summary>
    public partial class landing : ModernWindow
    {
        public landing()
        {
            InitializeComponent();
            const int snugContentWidth = 300;
            const int snugContentHeight = 300;

            var horizontalBorderHeight = SystemParameters.ResizeFrameHorizontalBorderHeight;
            var verticalBorderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
            var captionHeight = SystemParameters.CaptionHeight;

            Width = snugContentWidth + 2 * verticalBorderWidth;
            Height = snugContentHeight + captionHeight + 2 * horizontalBorderHeight;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

     

        private void landing_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.ChosenTheme != null)

            AppearanceManager.Current.ThemeSource = Properties.Settings.Default.ChosenTheme;
            AppearanceManager.Current.FontSize = Properties.Settings.Default.ChosenFontSize;
            AppearanceManager.Current.AccentColor = Properties.Settings.Default.ChosenAccent;

        }


        //Connection String
        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ddoca\Desktop\Dev\MAMEwithDB\RTI\mamedb.mdf; Integrated Security=True;Connect Timeout=30";


        //btn_Submit Click event
        private void login_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text == "" || txt_Password.Password == "")
            {
                System.Windows.MessageBox.Show("Introduceti nume utilizator si parola!");
                return;
            }

            try
            {
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from login where UserName=@username and Password=@password", con);
                cmd.Parameters.AddWithValue("@username", txt_UserName.Text);
                cmd.Parameters.AddWithValue("@password", txt_Password.Password);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;
                //If count is equal to 1, than show frmMain form
                if (count == 1)
                {
                   
                    homepage fm = new homepage();
                    fm.Show();
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Autentificare esuata, numele de utilizator sau parola sunt incorecte!");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        

    }
}


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
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace RTI.Pages
{
    /// <summary>
    /// Interaction logic for add.xaml
    /// </summary>
    public partial class add : ModernWindow
    {

        public add()
        {
            InitializeComponent();

            var userPrefsAdd = new UserPreferences();

            this.Height = userPrefsAdd.WindowHeight;
            this.Width = userPrefsAdd.WindowWidth;
            this.Top = userPrefsAdd.WindowTop;
            this.Left = userPrefsAdd.WindowLeft;
            this.WindowState = userPrefsAdd.WindowState;

        }

        public string tipBeneficiar (string tipcaz)

        {

            if (activ.IsChecked == true) tipcaz = "activ";
            if (inchis.IsChecked == true) tipcaz = "inchis";
            if (social.IsChecked == true) tipcaz = "social";
            if (medical.IsChecked == true) tipcaz = "medical";

            else
            {
                System.Windows.MessageBox.Show("Selectati tip caz!");
            }

            return tipcaz;

        }

             


        /* SQL */


        //Connection String
        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\mamedb.mdf;Integrated Security=True;Connect Timeout=30";
        private int numRowsAffected;

        //    SqlCommand command = new SqlCommand("INSERT INTO Employees (firstname,lastname,photo)

        //         VALUES(@img_name, @img_name, @img_data)", connection ); 
        //    // (need to write something to first and lastname columns 
        //    // because of constraints) 
        //   SqlParameter param0 = new SqlParameter("@img_name", SqlDbType.VarChar, 50);
        //    param0.Value = imgName;
        //    command.Parameters.Add(param0);
       
            
            //    SqlParameter param1 = new SqlParameter("@img_data", SqlDbType.Image);
        //    param1.Value = imgbin;
        //    command.Parameters.Add(param1);


        //    connection.Open();
        //    int numRowsAffected = command.ExecuteNonQuery();
        //    connection.Close();
        //    return numRowsAffected;
        //}

        private void save_click(object sender, EventArgs e)
        {



            using (SqlConnection connection = new SqlConnection(cs))
            {
                // using (
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.beneficiari (nume, prenume, CNP, serie_CI, numar_CI, tip_beneficiar, IBAN) VALUES ('test123', 'plm', '1234567', 'GL','123456', 'activ', 'iban')");



               // {
                    cmd.Connection = connection;            // <== lacking
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = "INSERT INTO dbo.beneficiari (nume, prenume, CNP, serie_CI, numar_CI, tip_beneficiar, IBAN) VALUES ('test123', 'plm', '1234567', 'GL','123456', 'activ', 'iban')";
                    //cmd.CommandText= "INSERT INTO dbo.locatie (tip_locatie, oras, judet) VALUES ('neh', 'neh', 'neh', 'neh')";
                    
                    //cmd.CommandText = "INSERT INTO dbo.beneficiari (nume, prenume, CNP, serie_CI, numar_CI, data_nastere, tip_beneficiar, IBAN) VALUES ('@nume', @prenume, @cnp, @serie_CI, @numar_CI, @data_nastere, @tip_beneficiar, @iban)";
                    //cmd.Parameters.AddWithValue("@nume", bfirstname.Text);
                    //cmd.Parameters.AddWithValue("@prenume", blastname.Text);
                    //cmd.Parameters.AddWithValue("@cnp", bcnp.Text);
                    //cmd.Parameters.AddWithValue("@serie_CI", bserieci.Text);
                    //cmd.Parameters.AddWithValue("@numar_CI", bnumarci.Text);
                    //cmd.Parameters.AddWithValue("@data_nastere", bdata_nastere.SelectedDate);
                    //cmd.Parameters.AddWithValue("@tip_beneficiar", "activ");
                    //cmd.Parameters.AddWithValue("@iban", iban.Text);

                    //TRY THIS: http://www.nullskull.com/articles/20020929.asp

                    try
                    {
                        connection.Open();
                        numRowsAffected=cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        System.Windows.MessageBox.Show("nu merge");
                    }
                    connection.Close();



            }

        }


        public void searchbox_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox tb = (System.Windows.Controls.TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= searchbox_GotFocus;

        }


        private void ModernWindow_Closed(object sender, EventArgs e)
        {

            RTI.Properties.Settings.Default.ChosenTheme = AppearanceManager.Current.ThemeSource;
            RTI.Properties.Settings.Default.ChosenFontSize = AppearanceManager.Current.FontSize;
            RTI.Properties.Settings.Default.ChosenAccent = AppearanceManager.Current.AccentColor;

            var userPrefsAdd = new UserPreferences();

            userPrefsAdd.WindowHeight = this.Height;
            userPrefsAdd.WindowWidth = this.Width;
            userPrefsAdd.WindowTop = this.Top;
            userPrefsAdd.WindowLeft = this.Left;
            userPrefsAdd.WindowState = this.WindowState;

            userPrefsAdd.Save();
            
            RTI.Properties.Settings.Default.Save();

        }

        private void ModernWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (RTI.Properties.Settings.Default.ChosenTheme != null)

            AppearanceManager.Current.ThemeSource = RTI.Properties.Settings.Default.ChosenTheme;
            AppearanceManager.Current.FontSize = RTI.Properties.Settings.Default.ChosenFontSize;
            AppearanceManager.Current.AccentColor = RTI.Properties.Settings.Default.ChosenAccent;

        }



    }
}

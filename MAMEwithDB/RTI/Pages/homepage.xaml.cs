using FirstFloor.ModernUI.Windows.Controls;
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
using RTI.Properties;
using FirstFloor.ModernUI.Presentation;

namespace RTI.Pages
{
    /// <summary>
    /// Interaction logic for homepage.xaml
    /// </summary>
    public partial class homepage : ModernWindow
    {
        public homepage()
        {
            InitializeComponent();
            var userPrefsAdd = new UserPreferences();

            this.Height = userPrefsAdd.WindowHeight;
            this.Width = userPrefsAdd.WindowWidth;
            this.Top = userPrefsAdd.WindowTop;
            this.Left = userPrefsAdd.WindowLeft;
            this.WindowState = userPrefsAdd.WindowState;
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

        public void searchbox_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox tb = (System.Windows.Controls.TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= searchbox_GotFocus;
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            add win2 = new add();
            win2.Show();
        }

        /*private void searchbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter)
           {
               searchbtn.PerformClick();
               e.SuppressKeyPress = true;
               e.Handled = true;
           }
    }
    */

        //if "Select all" checkbox is used, the others will become blank

        private void allcheck_CheckedChanged(object sender, EventArgs e)
        {
            personaldatacheck.IsChecked = !allcheck.IsChecked;
            infocazcheck.IsChecked = !allcheck.IsChecked;
            contactdatacheck.IsChecked = !allcheck.IsChecked;
            infotutorheck.IsChecked = !allcheck.IsChecked;
        }

        //if any checkbox option is checked and "Select All" is already checked, it will become blank

        private void options_CheckedChanged(object sender, EventArgs e)
        {
            if (personaldatacheck.IsChecked == true
                && infocazcheck.IsChecked == true
                && contactdatacheck.IsChecked == true
                && infotutorheck.IsChecked == true
                && allcheck.IsChecked == false)

                allcheck.IsChecked = !allcheck.IsChecked;

            else if (allcheck.IsChecked == true) allcheck.IsChecked = !allcheck.IsChecked;
        }


        private void DisplayData(int checkboxresult, string lastname, string firstname)

        {
       
            
            {
                results win3 = new results(checkboxresult, lastname, firstname); //passing checkboxresult, lastname and firstname into a new window, results.xaml
                win3.Show();
            }

           
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (lastnamebox.Text == "" && firstnamebox.Text == "")
            {
                System.Windows.MessageBox.Show("Introduceti nume sau prenume de beneficiar!");
                return;
            }


            if (personaldatacheck.IsChecked == false && infocazcheck.IsChecked == false && contactdatacheck.IsChecked == false &&
                infotutorheck.IsChecked == false && allcheck.IsChecked == false)

            {
                System.Windows.MessageBox.Show("Alegeti ce informatii doriti sa afisati!");
                return;
            }

            else
                //    |all|it|dc|ic|dp|
                try
                {
                    int checkboxresult = 11111;
                    //caz 1: daca Date Personale == true si restul false
                    if (personaldatacheck.IsChecked == true)
                    {
                        checkboxresult += 1;
                    }

                    //caz 2: daca Informatii caz ==true si restul false
                    if (infocazcheck.IsChecked == true)

                    {
                        checkboxresult += 10;
                    }

                    //caz 3: daca Date contact == true restu false
                    if (contactdatacheck.IsChecked == true)

                    {
                        checkboxresult += 100;
                    }

                    //caz 4: daca Informatii tutori==true restul false

                    if (infotutorheck.IsChecked == true)

                    {
                        checkboxresult += 1000;
                    }
                    
                    //caz 5: daca Selectati tot == true
                    if (allcheck.IsChecked == true)

                    {
                        checkboxresult += 10000;
                    }

                    DisplayData(checkboxresult, lastnamebox.Text, firstnamebox.Text);

                    ////caz 5: daca Date personale==true si Info caz==true, restul false

                    //if (personaldatacheck.IsChecked == true
                    //   && infocazcheck.IsChecked == true
                    //   && contactdatacheck.IsChecked == false
                    //   && infotutorheck.IsChecked == false
                    //   && allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, b.CNP, b.serie_CI, b.numar_CI,b.data_nastere, b.tip_beneficiar, b.IBAN, 
                    //                d.data_solicitare_sprijin, d.descriere_caz, 
                    //                i.start_beneficiu, i.sfarsit_beneficiu,
                    //                s.tip_sprijin
                    //                FROM dbo.beneficiari b
                    //                INNER JOIN dbo.descriere_caz d ON b.id_beneficiar=d.id_beneficiar
                    //                INNER JOIN dbo.istoric i ON b.id_beneficiar=i.id_beneficiar
                    //                INNER JOIN dbo.beneficii s ON i.id_beneficiu=s.id_beneficiu
                    //                WHERE nume=@lastname and prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}


                    ////caz 6: daca Date personale==true si Date contact==true, restul false

                    //if (personaldatacheck.IsChecked == true
                    //   && infocazcheck.IsChecked == false
                    //   && contactdatacheck.IsChecked == true
                    //   && infotutorheck.IsChecked == false
                    //   && allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, b.CNP, b.serie_CI,b.numar_CI, b.data_nastere, b.tip_beneficiar, b.IBAN, 
                    //                t.tip_tutor,t.nume, t.prenume, t.telefon, t.e_mail, 
                    //                l.tip_locatie, l.oras, l.judet, l.strada, l.bloc, l.scara, l.numar, l.apartament, l.etaj
                    //                FROM beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                INNER JOIN dbo.locatie l ON b.id_locatie=l.id_locatie
                    //                WHERE b.nume=@lastname and b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 7: daca Date personale==true si Info tutori==true, restu false

                    //if (personaldatacheck.IsChecked == true
                    //  && infocazcheck.IsChecked == false
                    //  && contactdatacheck.IsChecked == false
                    //  && infotutorheck.IsChecked == true
                    //  && allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, b.CNP, b.serie_CI, b.numar_CI, b.data_nastere, b.tip_beneficiar, b.IBAN, 
                    //                t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                WHERE b.nume=@lastname and b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 8: daca Info caz==true si Date contact==true, restu false
                    //if (personaldatacheck.IsChecked == false
                    //&& infocazcheck.IsChecked == true
                    //&& contactdatacheck.IsChecked == true
                    //&& infotutorheck.IsChecked == false
                    //&& allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, 
                    //                d.data_solicitare_sprijin, d.descriere_caz, 
                    //                i.start_beneficiu, i.sfarsit_beneficiu, 
                    //                s.tip_sprijin, 
                    //                l.tip_locatie, l.oras, l.judet, l.strada, l.bloc, l.scara, l.numar, l.apartament, l.etaj
                    //                FROM dbo.beneficiari b
                    //                INNER JOIN dbo.descriere_caz d ON b.id_beneficiar=d.id_beneficiar
                    //                INNER JOIN dbo.istoric i ON b.id_beneficiar=i.id_beneficiar 
                    //                INNER JOIN dbo.beneficii s ON i.id_beneficiu=s.id_beneficiu
                    //                INNER JOIN dbo.locatie l ON b.id_locatie=l.id_locatie
                    //                WHERE b.nume=@lastname and b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 9: daca Info caz==true si Info Tutori==true, restul false
                    //   if (personaldatacheck.IsChecked == false
                    //&& infocazcheck.IsChecked == true
                    //&& contactdatacheck.IsChecked == false
                    //&& infotutorheck.IsChecked == true
                    //&& allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, 
                    //                d.data_solicitare_sprijin, d.descriere_caz, 
                    //                i.start_beneficiu, i.sfarsit_beneficiu, 
                    //                s.tip_sprijin,
                    //                t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                INNER JOIN dbo.descriere_caz d ON b.id_beneficiar=d.id_beneficiar
                    //                INNER JOIN dbo.istoric i ON b.id_beneficiar=i.id_beneficiar
                    //                INNER JOIN dbo.beneficii s ON i.id_beneficiu=s.id_beneficiu
                    //                WHERE b.nume=@lastname and b.prenume=@firstname;
                    //                ";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 10: daca Date contact==true si Info tutori==true, restu false
                    //   if (personaldatacheck.IsChecked == false
                    //&& infocazcheck.IsChecked == false
                    //&& contactdatacheck.IsChecked == true
                    //&& infotutorheck.IsChecked == true
                    //&& allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, 
                    //                t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail,
                    //                l.tip_locatie, l.oras, l.judet, l.strada, l.bloc, l.scara, l.numar, l.apartament, l.etaj
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                INNER JOIN dbo.locatie l ON b.id_locatie=l.id_locatie
                    //                WHERE b.nume=@lastname AND b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 11: daca Date personale==true, Info caz==true si Date contact==true, restu false
                    //   if (personaldatacheck.IsChecked == true
                    //&& infocazcheck.IsChecked == true
                    //&& contactdatacheck.IsChecked == true
                    //&& infotutorheck.IsChecked == false
                    //&& allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, b.CNP, b.serie_CI, b.numar_CI, b.data_nastere, b.tip_beneficiar, b.IBAN, 
                    //                d.data_solicitare_sprijin, d.descriere_caz, 
                    //                i.start_beneficiu, i.sfarsit_beneficiu, 
                    //                s.tip_sprijin,                                    
                    //                t.tip_tutor, t.nume, t.prenume, t.telefon, t.e_mail, 
                    //                l.tip_locatie, l.oras, l.judet, l.strada, l.bloc, l.scara, l.numar, l.apartament, l.etaj
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                INNER JOIN dbo.locatie l ON b.id_locatie=l.id_locatie
                    //                INNER JOIN dbo.descriere_caz d ON b.id_beneficiar=d.id_beneficiar
                    //                INNER JOIN dbo.istoric i ON b.id_beneficiar=i.id_beneficiar 
                    //                INNER JOIN dbo.beneficii s ON i.id_beneficiu=s.id_beneficiu
                    //                WHERE b.nume=@lastname AND b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 12: daca Date personale==true, Info caz==true si Info tutori==true, restu false
                    //   if (personaldatacheck.IsChecked == true
                    //&& infocazcheck.IsChecked == true
                    //&& contactdatacheck.IsChecked == false
                    //&& infotutorheck.IsChecked == true
                    //&& allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, b.CNP, b.serie_CI, b.numar_CI, b.data_nastere, b.tip_beneficiar, b.IBAN, 
                    //                d.data_solicitare_sprijin, d.descriere_caz, 
                    //                i.start_beneficiu, i.sfarsit_beneficiu, 
                    //                s.tip_sprijin,
                    //                t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                INNER JOIN dbo.descriere_caz d ON b.id_beneficiar=d.id_beneficiar
                    //                INNER JOIN dbo.istoric i ON b.id_beneficiar=i.id_beneficiar
                    //                INNER JOIN dbo.beneficii s ON i.id_beneficiu=s.id_beneficiu
                    //                WHERE b.nume=@lastname AND b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 13: daca Date personale==true, Date contact==true si Info tutori==true, restu false
                    //   if (personaldatacheck.IsChecked == true
                    //&& infocazcheck.IsChecked == false
                    //&& contactdatacheck.IsChecked == true
                    //&& infotutorheck.IsChecked == true
                    //&& allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, b.CNP, b.serie_CI, b.numar_CI, b.data_nastere, b.tip_beneficiar, b.IBAN, 
                    //                t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail,
                    //                l.tip_locatie, l.oras, l.judet, l.strada, l.bloc, l.scara, l.numar, l.apartament, l.etaj
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                INNER JOIN dbo.locatie l ON b.id_locatie=l.id_locatie
                    //                WHERE b.nume=@lastname AND b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 14: daca Info caz==true, Date contact==true si Info tutori==true, restu false
                    //   if (personaldatacheck.IsChecked == false
                    //&& infocazcheck.IsChecked == true
                    //&& contactdatacheck.IsChecked == true
                    //&& infotutorheck.IsChecked == true
                    //&& allcheck.IsChecked == false)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, 
                    //                d.data_solicitare_sprijin, d.descriere_caz, 
                    //                i.start_beneficiu, i.sfarsit_beneficiu, 
                    //                s.tip_sprijin,
                    //                t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail,
                    //                l.tip_locatie, l.oras, l.judet, l.strada, l.bloc, l.scara, l.numar, l.apartament, l.etaj
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                    //                INNER JOIN dbo.locatie l ON b.id_locatie=l.id_locatie
                    //                INNER JOIN dbo.descriere_caz d ON b.id_beneficiar=d.id_beneficiar 
                    //                INNER JOIN dbo.istoric i ON b.id_beneficiar=i.id_beneficiar
                    //                INNER JOIN dbo.beneficii s On i.id_beneficiu=s.id_beneficiu
                    //                WHERE b.nume=@lastname AND b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}

                    ////caz 15: daca "selectati tot" e true
                    //   if (personaldatacheck.IsChecked == false
                    //&& infocazcheck.IsChecked == false
                    //&& contactdatacheck.IsChecked == false
                    //&& infotutorheck.IsChecked == false
                    //&& allcheck.IsChecked == true)

                    //{
                    //    SqlConnection con = new SqlConnection(cs);
                    //    var sql = @"SELECT b.nume, b.prenume, b.CNP, b.serie_CI, b.numar_CI, b.data_nastere, b.tip_beneficiar, b.IBAN, 
                    //                d.data_solicitare_sprijin, d.descriere_caz,
                    //                i.start_beneficiu, i.sfarsit_beneficiu, 
                    //                s.tip_sprijin,
                    //                t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail,
                    //                l.tip_locatie, l.oras, l.judet, l.strada, l.bloc, l.scara, l.numar, l.apartament, l.etaj
                    //                FROM dbo.beneficiari b 
                    //                INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor 
                    //                INNER JOIN dbo.locatie l ON b.id_locatie=l.id_locatie
                    //                INNER JOIN dbo.descriere_caz d ON b.id_beneficiar=d.id_beneficiar
                    //                INNER JOIN dbo.istoric i ON b.id_beneficiar=i.id_beneficiar 
                    //                INNER JOIN dbo.beneficii s ON i.id_beneficiu=s.id_beneficiu
                    //                WHERE b.nume=@lastname AND b.prenume=@firstname;";

                    //    SqlCommand cmd = new SqlCommand(sql, con);
                    //    if (lastnamebox.Text != "") cmd.Parameters.AddWithValue("@lastname", lastnamebox.Text);
                    //    if (firstnamebox.Text != "") cmd.Parameters.AddWithValue("@firstname", firstnamebox.Text);
                    //    DisplayData(cmd, con);
                    //}


                }

                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

        }

    }
}
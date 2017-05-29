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
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace RTI.Pages
{

    public partial class results : ModernWindow
    {
        DataSet ALL;
        DataTable DPtable, DCtable, ICtable, ITtable;
        SqlDataAdapter DPadapter, DCadapter, ICadapter, ITadapter;
        string lastname, firstname;
        //Connection String
        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ddoca\Desktop\Dev\MAMEwithDB\RTI\mamedb.mdf; Integrated Security=True;Connect Timeout=30";


        public results(int checkboxresult, string LN, string FN)
        {
            InitializeComponent();
            //System.Windows.MessageBox.Show(checkboxresult);

            int dp, dc, it, ic, all, aux;
            //    |all|it|dc|ic|dp|   -> 5 diggit int used to carry Yes=2 No=1;
            aux = checkboxresult;
            dp = aux % 10; //datePersonale
            aux = checkboxresult;
            ic = aux / 10 % 10; //infoCaz
            aux = checkboxresult;
            dc = aux / 100 % 10; //dateCaz
            aux = checkboxresult;
            it = aux / 1000 % 10; //infoTutori
            aux = checkboxresult;
            all = aux / 10000 % 10; //selectatiTot

            lastname = LN;
            firstname = FN;
            
            try
            {
                //caz 1: daca Date Personale == true
                if (dp == 2 || all == 2)

                {
                    
                   // string sql = @"SELECT * from beneficiari where nume=@lastname and prenume=@firstname";
                    string SQLselectDP = @"SELECT id_beneficiar, nume, prenume, CNP, serie_CI, numar_CI, data_nastere, tip_beneficiar, IBAN 
                                    FROM dbo.beneficiari where nume=@lastname or prenume=@firstname";

                    string SQLupdateDP = @"UPDATE beneficiari SET nume=@nume, prenume=@prenume, CNP=@CNP, serie_CI=@serie_CI, numar_CI=@numar_CI, data_nastere=@data_nastere, tip_beneficiar=@tip_beneficiar, IBAN=@IBAN" +
                                  " WHERE nume=@lastname or prenume=@firstname";

                    DisplayDataInDataGrid(SQLselectDP, 1);

                }

                //caz 2: daca Informatii caz ==true
                if (ic == 2 || all == 2)

                {
                    string SQLselectIC = @"SELECT b.nume, b.prenume, 
                                  d.data_solicitare_sprijin, d.descriere_caz,                                    
                                  i.start_beneficiu, i.sfarsit_beneficiu, 
                                  s.tip_sprijin 
                                  FROM dbo.beneficiari b
                                  INNER JOIN dbo.descriere_caz d ON d.id_beneficiar=b.id_beneficiar
                                  INNER JOIN dbo.istoric i ON  i.id_beneficiar=b.id_beneficiar 
                                  INNER JOIN dbo.beneficii s ON s.id_beneficiu=i.id_beneficiu 
                                  WHERE nume=@lastname and prenume=@firstname";
                    
                    DisplayDataInDataGrid(SQLselectIC, 2);
                }

                //caz 3: daca Date contact == true
                if (dc == 2 || all == 2)

                {
                    string SQLselectDC = @"SELECT b.nume, b.prenume, 
                                    t.tip_tutor,t.nume, t.prenume, t.telefon, t.e_mail, 
                                    l.tip_locatie, l.oras, l.judet, l.strada, l.bloc,l.scara, l.numar, l.apartament, l.etaj
                                    FROM dbo.beneficiari b 
                                    INNER JOIN dbo.tutori t ON b.id_tutor = t.id_tutor
                                    INNER JOIN dbo.locatie l ON b.id_locatie = l.id_locatie
                                    WHERE b.nume = @lastname and b.prenume = @firstname;";

                    DisplayDataInDataGrid(SQLselectDC, 3);
                }

                //caz 4: daca Informatii tutori == true
                if (it == 2 || all == 2)

                {
                    string SQLselectIT = @"SELECT b.nume, b.prenume, 
                                    t.tip_tutor, t.nume, t.prenume, t.CNP, t.serie_CI, t.numar_CI, t.telefon, t.e_mail
                                    FROM dbo.beneficiari b 
                                    INNER JOIN dbo.tutori t ON b.id_tutor=t.id_tutor
                                    WHERE b.nume=@lastname and b.prenume=@firstname;";
                    DisplayDataInDataGrid(SQLselectIT, 4);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void DisplayDataInDataGrid(string sql, int x)
        {

            if (x == 1)
            {
                SqlConnection con = new SqlConnection(cs);

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                DPadapter = CreateCustomerAdapter(con, sql, lastname, firstname);
                DPtable = new DataTable("datePersonale");
                DPadapter.Fill(DPtable);
                con.Close();
                int count = DPtable.Rows.Count;
                if (count > 0 && x == 1)

                {
                    dataGrid1.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = DPtable });
                }
            }

            if (x == 2)
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                DCtable = new DataTable("dateContact");
                //ALLset.Tables.Add(DCtable);
               
                DCadapter = CreateCustomerAdapter(con, sql, lastname, firstname);
                DCadapter.Fill(DCtable);
                int count = DCtable.Rows.Count;
                if (count > 0 && x == 2)
                {
                    dateContact.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = DCtable });
                }
            }

            if (x == 3)
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                ITtable = new DataTable("infoTutori");
                ITadapter = CreateCustomerAdapter(con, sql, lastname, firstname);
                ITadapter.Fill(ITtable);
                int count = ITtable.Rows.Count;
                if (count > 0 && x == 3)
                {
                    infoTutori.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = ITtable });
                }
            }

            if (x == 4)
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                ICtable = new DataTable("infoCaz");

                ICadapter = CreateCustomerAdapter(con, sql, lastname, firstname);
                ICadapter.Fill(ICtable);
                int count = ICtable.Rows.Count;
                if (count > 0 && x == 4)
                {
                    infoCaz.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = ICtable });
                }
            }

        }


        public static SqlDataAdapter CreateCustomerAdapter(SqlConnection con, string sql, string lastname, string firstname)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            // Create the SelectCommand.
            SqlCommand selectcommand = new SqlCommand(sql, con);

            // Add the parameters for the SelectCommand.
            //command.Parameters.Add("@lastname", SqlDbType.Char, 20);
            //command.Parameters.Add("@firstname", SqlDbType.Char, 20);
            selectcommand.Parameters.AddWithValue("@lastname", lastname);
            selectcommand.Parameters.AddWithValue("@firstname", firstname);

            adapter.SelectCommand = selectcommand;

            //// Create the InsertCommand.
            //command = new SqlCommand("INSERT INTO" + dbtable + " (nume,prenume,CNP,serie_CI,numar_CI,data_nastere,tip_beneficiar,IBAN) " +
            //    "VALUES (@nume, @prenum, @CNP, @serie_CI, @numar_CI, @data_nastere, @tip_beneficiar, @IBAN)", connection);

            //// Add the parameters for the InsertCommand.
            //command.Parameters.Add("@", SqlDbType.NChar, 5, "CustomerID");
            //command.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 40, "CompanyName");

            //adapter.InsertCommand = command;

            // Create the UpdateCommand.

            string sqlupdate = @"UPDATE beneficiari SET nume=@nume, prenume=@prenume, CNP=@CNP, serie_CI=@serie_CI, numar_CI=@numar_CI, data_nastere=@data_nastere, tip_beneficiar=@tip_beneficiar, IBAN=@IBAN" +
                                  " WHERE nume=@lastname or prenume=@firstname";

            //string sqlupdate = @"UPDATE beneficiari SET nume=@nume, prenume=@prenume WHERE id_beneficiar=@id_beneficiar";

            SqlCommand updatecommand = new SqlCommand(sqlupdate, con);

            // Add the parameters for the UpdateCommand.
            updatecommand.Parameters.Add("@nume", SqlDbType.Char, 20, "nume");
            updatecommand.Parameters.Add("@prenume", SqlDbType.Char, 20, "prenume");
            updatecommand.Parameters.Add("@CNP", SqlDbType.Char, 13, "CNP");
            updatecommand.Parameters.Add("@serie_CI", SqlDbType.Char, 3, "serie_CI");
            updatecommand.Parameters.Add("@numar_CI", SqlDbType.Int, 6, "numar_CI");
            updatecommand.Parameters.Add("@data_nastere", SqlDbType.Date, 20, "data_nastere");
            updatecommand.Parameters.Add("@tip_beneficiar", SqlDbType.Char, 10, "tip_beneficiar");
            updatecommand.Parameters.Add("@IBAN", SqlDbType.Char, 24, "IBAN");

            SqlParameter parameter = updatecommand.Parameters.Add("@id_beneficiar", SqlDbType.Int, 6, "id_beneficiar");
            parameter.SourceVersion = DataRowVersion.Original;

            adapter.UpdateCommand = updatecommand;



            //// Create the DeleteCommand.
            //command = new SqlCommand(
            //    "DELETE FROM Customers WHERE CustomerID = @CustomerID", connection);

            //// Add the parameters for the DeleteCommand.
            //parameter = command.Parameters.Add(
            //    "@CustomerID", SqlDbType.NChar, 5, "CustomerID");
            //parameter.SourceVersion = DataRowVersion.Original;

            //adapter.DeleteCommand = command;

            return adapter;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            //Connection String
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ddoca\Desktop\Dev\MAMEwithDB\RTI\mamedb.mdf; Integrated Security=True;Connect Timeout=30";
            
            //https://msdn.microsoft.com/en-us/library/ms233819(v=vs.80).aspx

            try
            {

                using (SqlConnection con = new SqlConnection(cs))

                {
                    con.Open();
                    // using (SqlCommand cmd = new SqlCommand("UPDATE [beneficiari] SET nume=@nume, prenume=@prenume" + " WHERE id_beneficiar=@id_beneficiar", con))

                    // {

                    //System.Data.DataTable dt = new System.Data.DataTable();
                    //SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
                    //conn.Open();
                        

                        SqlCommandBuilder cb = new SqlCommandBuilder(DPadapter);

                        DPadapter.Fill(DPtable);
                        DPadapter.UpdateCommand = cb.GetUpdateCommand();
                        DPadapter.Update(DPtable);



                        //string sqlupdate = @"UPDATE [beneficiari] SET nume=@nume, prenume=@prenume, numar_CI=@CI" + "WHERE id_beneficiar=@ID";
                        //SqlCommand cmd = new SqlCommand(sqlupdate, con);
                        //string testname = "test";
                        //int CI = 1234;
                        //cmd.Parameters.AddWithValue("@nume", testname);
                        //cmd.Parameters.AddWithValue("@prenume", testname);
                        //cmd.Parameters.AddWithValue("@CI", CI);
                        //cmd.Parameters.AddWithValue("@ID", 1);
                        //cmd.Parameters.Add("@nume", SqlDbType.Char, 20, "nume");
                        //cmd.Parameters.Add("@prenume", SqlDbType.Char, 20, "prenume");
                        //SqlParameter parameter = cmd.Parameters.Add("@id_beneficiar", SqlDbType.Int, 6, "id_beneficiar");
                        //parameter.SourceVersion = DataRowVersion.Original;


                        //int rows = cmd.ExecuteNonQuery();

                       

                      //  System.Windows.MessageBox.Show("Update Successful: " + rows + " were updated");
                   // }
                }
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Update Failed" + ex.ToString());
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)

        {
           
        
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {




        }
    }

}

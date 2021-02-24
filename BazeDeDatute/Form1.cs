using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BazeDeDatute
{
    
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textComandaSQL = "SELECT * FROM Carti";
            SqlCommand command = new SqlCommand(textComandaSQL,connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //deschidere conexiune BD
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator\source\repos\BazeDeDatute\BazeDeDatute\Biblioteca.mdf;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int nrVol;
            try
            {
                nrVol=int.Parse(textBox1.Text);
                string textComandaSQL = $"SELECT * FROM Carti where nr_vol={nrVol}";
                SqlCommand command = new SqlCommand(textComandaSQL, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Ba-i inteligentule!");
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int nrvol, an;
            string isbn, autor, titlu;
            try
            {   
                //constructie comanda inserare
                isbn = textBox2.Text;
                autor = textBox3.Text;
                titlu = textBox4.Text;
                an = int.Parse(textBox5.Text);
                nrvol = int.Parse(textBox6.Text);
                string textComandaSQL = $"INSERT INTO Carti (isbn,autorul,titlul,anul_ap,nr_vol) VALUES ('{isbn}','{autor}','{titlu}','{an}','{nrvol}') ";
                SqlCommand command = new SqlCommand(textComandaSQL, connection);
                //executie comanda inserare
                command.ExecuteNonQuery();
                //afisare tabel(pt. verificare inserare)
                textComandaSQL = "SELECT * FROM Carti";
                SqlCommand command2 = new SqlCommand(textComandaSQL, connection);
                SqlDataAdapter da = new SqlDataAdapter(command2);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Toate campurile sunt obligatorii! (Eroare: {ex.Message})");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktinis2._2
{
    public partial class Regi : UserControl
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Prisijungimas.mdb;";
        private OleDbConnection myConnection;

        public Regi()
        {
            InitializeComponent();
            textBox6.PasswordChar = '*';
            textBox7.PasswordChar = '*';


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            bool q = false;
            bool w = false;
            bool es = false;
            bool pas = false;
            string patikr;
            string patikr2;
            string patikr3;
            string isvedimas = "SELECT Prisijungimo_vardas, Elektroninis_Pastas, Telefono_numeris FROM Prisijungimas ORDER BY ID;";
            OleDbCommand command = new OleDbCommand(isvedimas, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                patikr = reader[0].ToString();
                patikr2 = reader[1].ToString();
                patikr3 = reader[2].ToString();

                if (textBox1.Text == patikr)
                {
                    MessageBox.Show("Prisijungimo vardas uzimtas", "Klaida", MessageBoxButtons.OK);
                    q = true;
                    break;
                } else if (patikr2 == textBox4.Text)
                {
                    MessageBox.Show("El. Pastas jau yra nauduojamas", "Klaida", MessageBoxButtons.OK);
                    w = true;
                    break;
                } else if (patikr3 == textBox5.Text)
                {
                    MessageBox.Show("Tel. Numeris jau yra nauduojamas", "Klaida", MessageBoxButtons.OK);
                    es = true;
                    break;
                } else if (textBox6.Text != textBox7.Text)
                {
                    MessageBox.Show("Slaptazodziai nesutampa", "Klaida", MessageBoxButtons.OK);
                    pas = true;
                    break;
                } else
                {
                    q = false;
                    w = false;
                    es = false;
                    pas = false;
                }
            }
            reader.Close();
            myConnection.Close();

            if ((q == false) && (w == false) && (es == false) && (pas == false))
            {
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                string query = "INSERT INTO Prisijungimas (Prisijungimo_vardas, Vardas, Pavarde, Elektroninis_Pastas, Telefono_numeris, Slaptazodis, Administracija) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '0')";
                OleDbCommand command2 = new OleDbCommand(query, myConnection);
                command2.ExecuteNonQuery();
                MessageBox.Show("Registracija ivykdyta", "Registracija", MessageBoxButtons.OK);
                myConnection.Close();
                Hide();
            }

        }

        private void Regi_Load(object sender, EventArgs e)
        {

        }
    }
    
}
    


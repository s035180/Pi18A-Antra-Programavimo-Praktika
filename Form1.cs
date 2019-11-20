using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktinis2._2
{
    public partial class Form1 : Form
    {
        public static string connectString3 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Prisijungimas.mdb;";
        private OleDbConnection myConnection3;

        string vardas;
        bool q = false;
        public Form1()
        {
            InitializeComponent();
            stacio1.BringToFront();
            textBox2.PasswordChar = '*';


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            stacio1.BringToFront();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            nesio1.BringToFront();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(q==true)
            krepselis1.BringToFront();
            else
                MessageBox.Show("Turite prisijungti", "Klaida", MessageBoxButtons.OK);
        }

        private void Regi_Click(object sender, EventArgs e)
        {
            regi1.Visible = true;
            regi1.BringToFront();
        }

        private void Pris_Click(object sender, EventArgs e)
        {
            
            myConnection3 = new OleDbConnection(connectString3);
            myConnection3.Open();
            string patikr = textBox1.Text + " " + textBox2.Text;

            string isvedimas3 = "SELECT Prisijungimo_vardas, Slaptazodis, Administracija FROM Prisijungimas ORDER BY ID;";
            OleDbCommand command3 = new OleDbCommand(isvedimas3, myConnection3);
            OleDbDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                string patikr2 = reader3[0].ToString() + " " + reader3[1].ToString();
                if (patikr == patikr2)
                {
                    vardas = textBox1.Text.ToString();
                    q = true;
                    label3.Text = "Sveiki " + vardas;
                    panel4.Visible = true;
                    string teises = "1";
                    if(reader3[2].ToString() == teises)
                    {
                        button5.Visible = true;
                    }
                    break;
                }

            }

            reader3.Close();
            myConnection3.Close();



            if (q == false)
                MessageBox.Show("Neteisingai ivesti duomenys", "Klaida", MessageBoxButtons.OK);
        }

        private void Regi1_Load(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            q = false;
            panel4.Visible = false;
            admin1.Visible = false;
            button5.Visible = false;
        }

        private void Krepselis1_Load(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            admin1.Visible = true;
            admin1.BringToFront();
        }

        private void Admin1_Load(object sender, EventArgs e)
        {

        }
    }
}

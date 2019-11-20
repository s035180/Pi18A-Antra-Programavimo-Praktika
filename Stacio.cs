using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktinis2._2
{
    public partial class Stacio : UserControl
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Stacio.mdb;";
        private OleDbConnection myConnection;
        public static string connectString2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Krepselis.mdb;";
        private OleDbConnection myConnection2;
        Stacionarus[] S = new Stacionarus[100];
        int n;
        public Stacio()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string isvedimas = "SELECT Pavadinimas, Kiekis, Kaina, CPU, Videocard, Memory FROM Stacio ORDER BY ID;";
            OleDbCommand command = new OleDbCommand(isvedimas, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            n = 0;
            while (reader.Read())
            {
                S[n] = new Stacionarus();
                S[n].SetPav(reader[0].ToString());
                S[n].SetKiek(Convert.ToInt32(reader[1]));
                S[n].SetKaina(Convert.ToDouble(reader[2]));
                S[n].SetCPU(reader[3].ToString());
                S[n].SetGPU(reader[4].ToString());
                S[n].SetMem(Convert.ToDouble(reader[5]));
                n++;
            }
            reader.Close();
            myConnection.Close();


        }

        private void Stacio_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                listBox1.Items.Add(S[i].GetPav() + " " + S[i].GetKiek() + " " + S[i].GetKaina());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            myConnection2 = new OleDbConnection(connectString2);
            myConnection2.Open();
            int v = listBox1.SelectedIndex;
            if (S[v].GetKiek() < 1)
            {
                MessageBox.Show("Atsiprasome, preke yra ispraduota", "Prekes", MessageBoxButtons.OK);
            }
            else
            {

                string query = "INSERT INTO Krepselis (Pavadinimas, Kaina, Apmokejimo_busena) VALUES ('" + S[v].GetPav() + "', '" + S[v].GetKaina() + "', 'Neapmoketa')";
                OleDbCommand command2 = new OleDbCommand(query, myConnection2);
                command2.ExecuteNonQuery();
                MessageBox.Show("Preke sekmingai itraukta i kripseli", "Prekes", MessageBoxButtons.OK);
                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                S[v].SetKiek(S[v].GetKiek() - 1);
                string query2 = "UPDATE Stacio SET Kiekis = '" + S[v].GetKiek() + "' WHERE Pavadinimas = '" + S[v].GetPav() +"'";
                OleDbCommand command = new OleDbCommand(query2, myConnection);
                command.ExecuteNonQuery();

            }
            myConnection2.Close();
            myConnection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int v = listBox1.SelectedIndex;
            listBox2.Items.Clear();
            listBox2.Items.Add(S[v].GetCPU() + " " + S[v].GetGPU() + " " + S[v].GetMem());
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string isvedimas = "SELECT Pavadinimas, Kiekis, Kaina, CPU, Videocard, Memory FROM Stacio ORDER BY ID;";
            OleDbCommand command = new OleDbCommand(isvedimas, myConnection);
            int m = 0;
            OleDbDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString());

                if (n == m)
                {
                    S[m] = new Stacionarus();
                    S[m].SetPav(reader[0].ToString());
                    S[m].SetKiek(Convert.ToInt32(reader[1]));
                    S[m].SetKaina(Convert.ToDouble(reader[2]));
                    S[m].SetCPU(reader[3].ToString());
                    S[m].SetGPU(reader[4].ToString());
                    S[m].SetMem(Convert.ToDouble(reader[5]));
                    
                }
                m++;


            }
            n = m;
            reader.Close();
            myConnection.Close();
        }
    }
}

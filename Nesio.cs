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
    public partial class Nesio : UserControl
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Nesio.mdb;";
        private OleDbConnection myConnection;
        public static string connectString2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Krepselis.mdb;";
        private OleDbConnection myConnection2;
        Nesiojami[] N = new Nesiojami[100];
        int n;
        public Nesio()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string isvedimas = "SELECT Pavadinimas, Kiekis, Kaina, CPU, Videocard, Memory FROM Nesio ORDER BY ID;";
            OleDbCommand command = new OleDbCommand(isvedimas, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            n = 0;
            while (reader.Read())
            {
                N[n] = new Nesiojami();
                N[n].SetPav(reader[0].ToString());
                N[n].SetKiek(Convert.ToInt32(reader[1]));
                N[n].SetKaina(Convert.ToDouble(reader[2]));
                N[n].SetCPU(reader[3].ToString());
                N[n].SetGPU(reader[4].ToString());
                N[n].SetMem(Convert.ToDouble(reader[5]));
                n++;
            }
            reader.Close();
            myConnection.Close();

        }

        private void Nesio_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                listBox2.Items.Add(N[i].GetPav() + " " + N[i].GetKiek() + " " + N[i].GetKaina());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            myConnection2 = new OleDbConnection(connectString2);
            myConnection2.Open();
            int v = listBox2.SelectedIndex;
            if(N[v].GetKiek() < 1 )
            {
                MessageBox.Show("Atsiprasome, preke yra ispraduota", "Prekes", MessageBoxButtons.OK);
            }
            else
            {

                string query = "INSERT INTO Krepselis (Pavadinimas, Kaina, Apmokejimo_busena) VALUES ('" + N[v].GetPav() + "', '" + N[v].GetKaina() + "', 'Neapmoketa')";
                OleDbCommand command2 = new OleDbCommand(query, myConnection2);
                command2.ExecuteNonQuery();
                MessageBox.Show("Preke sekmingai itraukta i kripseli", "Prekes", MessageBoxButtons.OK);

                myConnection = new OleDbConnection(connectString);
                myConnection.Open();
                N[v].SetKiek(N[v].GetKiek() - 1);
                string query2 = "UPDATE Nesio SET Kiekis = '" + N[v].GetKiek() + "' WHERE Pavadinimas = '" + N[v].GetPav() + "'";
                OleDbCommand command = new OleDbCommand(query2, myConnection);
                command.ExecuteNonQuery();

            }
            myConnection2.Close();
            myConnection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int v=listBox2.SelectedIndex;
            listBox1.Items.Clear();
            listBox1.Items.Add(N[v].GetCPU() + " " + N[v].GetGPU() + " " + N[v].GetMem());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string isvedimas = "SELECT Pavadinimas, Kiekis, Kaina, CPU, Videocard, Memory FROM Nesio ORDER BY ID;";
            OleDbCommand command = new OleDbCommand(isvedimas, myConnection);
            int m = 0;
            OleDbDataReader reader = command.ExecuteReader();
            listBox2.Items.Clear();
            while (reader.Read())
            {
                listBox2.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString());

                if (n == m)
                {
                    N[m] = new Nesiojami();
                    N[m].SetPav(reader[0].ToString());
                    N[m].SetKiek(Convert.ToInt32(reader[1]));
                    N[m].SetKaina(Convert.ToDouble(reader[2]));
                    N[m].SetCPU(reader[3].ToString());
                    N[m].SetGPU(reader[4].ToString());
                    N[m].SetMem(Convert.ToDouble(reader[5]));
                    
                }
                m++;


            }
            n = m;
            reader.Close();
            myConnection.Close();
        }
    }
}

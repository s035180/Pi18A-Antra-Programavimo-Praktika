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
    public partial class Krepselis : UserControl
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Krepselis.mdb;";
        private OleDbConnection myConnection;

        Krep[] K = new Krep[100];
        int n = 0;
        double sum = 0;

        public Krepselis()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string isvedimas = "SELECT Pavadinimas, Kaina, Apmokejimo_busena FROM Krepselis ORDER BY ID;";
            OleDbCommand command = new OleDbCommand(isvedimas, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            n = 0;
            while (reader.Read())
            {
                K[n] = new Krep();
                K[n].SetPav(reader[0].ToString());
                K[n].SetKaina(Convert.ToDouble(reader[1]));
                K[n].SetBus(reader[2].ToString());
                sum = sum + K[n].GetKaina();
                n++;
            }
            
            reader.Close();
            myConnection.Close();
            label1.Text = "Bendra suma: " + sum.ToString();
        }

        private void Krepselis_Load(object sender, EventArgs e)
        {
            for(int i=0; i<n; i++)
            {
                listBox3.Items.Add(K[i].GetPav() + " " + K[i].GetKaina() + " " + K[i].GetBus());
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            apmokejimas1.Visible = true;
            apmokejimas1.BringToFront();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int v = listBox3.SelectedIndex;
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "DELETE FROM Krepselis WHERE [Pavadinimas] = '" + K[v].GetPav() + "'";
            OleDbCommand command2 = new OleDbCommand(query, myConnection);
            command2.ExecuteNonQuery();
            listBox3.Items.RemoveAt(listBox3.SelectedIndex);
            MessageBox.Show("Preke sekmingai istrinta", "Prekes", MessageBoxButtons.OK);

            myConnection.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "DELETE FROM Krepselis;";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
            listBox3.Items.Clear();
            myConnection.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            double sum = 0;
            listBox3.Items.Clear();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string isvedimas = "SELECT Pavadinimas, Kaina, Apmokejimo_busena FROM Krepselis ORDER BY ID;";
            OleDbCommand command = new OleDbCommand(isvedimas, myConnection);
            int m = 0;
            OleDbDataReader reader = command.ExecuteReader();
            listBox3.Items.Clear();
            while (reader.Read())
            {
                listBox3.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString());

                if (n == m)
                {
                    K[m] = new Krep();
                    K[m].SetPav(reader[0].ToString());
                    K[m].SetKaina(Convert.ToDouble(reader[1]));
                    K[m].SetBus(reader[2].ToString());

                }
                sum = sum + K[m].GetKaina();
                m++;



            }
            n = m;
            label1.Text = "Bendra suma: " + sum.ToString();
            reader.Close();
            myConnection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            apmokejimas1.Visible = true;
            apmokejimas1.BringToFront();

        }
    }
}

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
    public partial class Admin : UserControl
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Stacio.mdb;";
        private OleDbConnection myConnection;
        public static string connectString2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Nesio.mdb;";
        private OleDbConnection myConnection2;
        Nesiojami[] N = new Nesiojami[100];
        int n;
        Stacionarus[] S = new Stacionarus[100];
        int m;
        public Admin()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString2);
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

            myConnection2 = new OleDbConnection(connectString);
            myConnection2.Open();
            string isvedimas2 = "SELECT Pavadinimas, Kiekis, Kaina, CPU, Videocard, Memory FROM Stacio ORDER BY ID;";
            OleDbCommand command2 = new OleDbCommand(isvedimas2, myConnection2);
            OleDbDataReader reader2 = command2.ExecuteReader();
            m = 0;
            while (reader2.Read())
            {
                S[m] = new Stacionarus();
                S[m].SetPav(reader2[0].ToString());
                S[m].SetKiek(Convert.ToInt32(reader2[1]));
                S[m].SetKaina(Convert.ToDouble(reader2[2]));
                S[m].SetCPU(reader2[3].ToString());
                S[m].SetGPU(reader2[4].ToString());
                S[m].SetMem(Convert.ToDouble(reader2[5]));
                m++;
            }
            reader2.Close();
            myConnection2.Close();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m; i++)
            {
                listBox1.Items.Add(S[i].GetPav() + " " + S[i].GetKiek() + " " + S[i].GetKaina());
            }
            for (int i = 0; i < n; i++)
            {
                listBox2.Items.Add(N[i].GetPav() + " " + N[i].GetKiek() + " " + N[i].GetKaina());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            myConnection2 = new OleDbConnection(connectString2);
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            myConnection2.Open();
            if ((checkBox1.Checked) && (!checkBox2.Checked))
            {
                int parsedValue;
                if ((!int.TryParse(textBox2.Text, out parsedValue)) || (!int.TryParse(textBox3.Text, out parsedValue)) || (!int.TryParse(textBox6.Text, out parsedValue)))
                {
                    MessageBox.Show("Turite ivesti skaiciu lauke Kiekis ir Kaina", "Prekes", MessageBoxButtons.OK);
                }
                else
                {
                    string query = "INSERT INTO Stacio (Pavadinimas, Kiekis, Kaina, CPU, Videocard, Memory) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "')";
                    OleDbCommand command = new OleDbCommand(query, myConnection);
                    command.ExecuteNonQuery();
                    listBox1.Items.Add(textBox1.Text + " " + textBox2.Text + " " + textBox3.Text);
                    myConnection.Close();
                }
            }
            else if ((!checkBox1.Checked) && (checkBox2.Checked))
            {
                int parsedValue;
                if ((!int.TryParse(textBox2.Text, out parsedValue)) || (!int.TryParse(textBox3.Text, out parsedValue)) || (!int.TryParse(textBox6.Text, out parsedValue)))
                {
                    MessageBox.Show("Turite ivesti skaiciu lauke Kiekis ir Kaina", "Prekes", MessageBoxButtons.OK);
                }
                else
                {
                    string query = "INSERT INTO Nesio (Pavadinimas, Kiekis, Kaina, CPU, Videocard, Memory) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "')";
                    OleDbCommand command2 = new OleDbCommand(query, myConnection2);
                    command2.ExecuteNonQuery();
                    listBox2.Items.Add(textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " ");
                    myConnection2.Close();
                }
            }
            myConnection.Close();
            myConnection2.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int v = listBox1.SelectedIndex;
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "DELETE FROM Stacio WHERE [Pavadinimas] = '" + S[v].GetPav() + "'";
            OleDbCommand command2 = new OleDbCommand(query, myConnection);
            command2.ExecuteNonQuery();
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            MessageBox.Show("Preke sekmingai istrinta", "Prekes", MessageBoxButtons.OK);
            myConnection.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int v = listBox2.SelectedIndex;
            myConnection2 = new OleDbConnection(connectString2);
            myConnection2.Open();
            string query = "DELETE FROM Nesio WHERE [Pavadinimas] = '" + N[v].GetPav() + "'";
            OleDbCommand command2 = new OleDbCommand(query, myConnection2);
            command2.ExecuteNonQuery();
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            MessageBox.Show("Preke sekmingai istrinta", "Prekes", MessageBoxButtons.OK);
            myConnection2.Close();
        }
    }
}

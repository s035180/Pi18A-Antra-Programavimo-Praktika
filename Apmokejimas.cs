using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktinis2._2
{
    public partial class Apmokejimas : UserControl
    {
        public Apmokejimas()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Viskas gerai", "Prekes", MessageBoxButtons.OK);
            Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}

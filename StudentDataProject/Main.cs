using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDataProject
{
    public partial class Mainpage : Form
    {
        public Mainpage()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.MdiParent=this;
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void searchDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchScreen search = new searchScreen();
                search.MdiParent = this;
            search.Show();
        }
    }
}

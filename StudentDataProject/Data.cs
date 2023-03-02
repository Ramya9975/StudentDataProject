using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDataProject
{
    public partial class Data : Form
    {
        public Data()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS; database=project;Integrated security = true;");

            string StudentName = textBox1.Text;
            int StudentRollNo = Convert.ToInt32(textBox2.Text);
            string Subject = textBox3.Text;
            SqlDataAdapter da = new SqlDataAdapter($"select n.studentname, n.StudentRollNo,S.SubjectName,d.SubjectMarks  FROM student n INNER JOIN StudentMarks d ON d.StudentId = n.studentId \r\nINNER JOIN Subject S  ON d.SubjectId = d.SubjectId where Studentname='{StudentName}' And StudentRollNO='{StudentRollNo}' And SubjectName='{Subject}'", connection);
            DataTable dt = new DataTable();
            dt.Columns.Add("Student Name", typeof(string));
            dt.Columns.Add("Student Roll No", typeof(string));
            dt.Columns.Add("Subject Name", typeof(string));
            dt.Columns.Add("Subject Marks", typeof(int));
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }



                 

            

        }
    }


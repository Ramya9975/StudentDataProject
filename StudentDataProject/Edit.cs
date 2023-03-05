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
    public partial class Edit : Form
    {
        int mark;string rollno, studentName, subjectName;
        public Edit(string id,string studName,string subName,int submark)
        {
            InitializeComponent();
            rollno = id;
            studentName = studName;
            subjectName = subName;
            mark = submark;

        }
        SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS; database=project;Integrated security = true;");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Edit_Load(object sender, EventArgs e)
        {
            textBoxRollNo.Text = rollno;
            textBoxStudentname.Text = studentName;
            textBoxSubject.Text = subjectName;
            textBoxSubjectMark.Text = mark.ToString();

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string id = textBoxRollNo.Text;
            string subName = textBoxSubject.Text;
            int subjectMark = Convert.ToInt32 (textBoxSubjectMark.Text);
            SqlCommand cmd1 = new SqlCommand($"Select studentid from student where studentRollno = '{id}'", connection);
            SqlCommand cmd2 = new SqlCommand($"Select subjectid from subject where subjectName = '{subName}'", connection);
            connection.Open();
            SqlDataReader reader1 = cmd1.ExecuteReader();
            reader1.Read();
            int result1 = Convert.ToInt32(reader1["studentid"]);
            reader1.Close();

            SqlDataReader reader2 = cmd2.ExecuteReader();

            reader2.Read();
            int result2 = Convert.ToInt32(reader2["subjectid"]);
            reader2.Close();

            SqlCommand Command = new SqlCommand($" Update studentmarks Set subjectMarks = {subjectMark} where studentid = {result1} and subjectid = {result2}", connection);

            int check = Command.ExecuteNonQuery();
            if (check > 0) 
            {
                MessageBox.Show("Student Data Successfully Updated");
            }
            else 
            {
                MessageBox.Show("Student Data not updated - Error processing");
            }

            connection.Close();

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

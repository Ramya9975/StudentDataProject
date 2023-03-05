using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StudentDataProject
{
    public partial class searchScreen : Form
    {
        public searchScreen()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS; database=project;Integrated security = true;");
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            string StudentName = textBox1.Text;
            string StudentRollNo = textBox2.Text;
            string Subject = textBox3.Text;
            SqlDataAdapter da = new SqlDataAdapter($"Select student.StudentRollNo,student.StudentName,Subject.SubjectName,studentMarks.SubjectMarks from student\r\njoin studentMarks on studentMarks.StudentID = student.studentID join subject on Subject.SubjectId=studentMarks.SubjectID where Studentname='{StudentName}' And StudentRollNO='{StudentRollNo}' And SubjectName='{Subject}'", connection);
         
            DataTable dt = new DataTable();
            
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            DataGridViewButtonColumn editButtoncolumn = new DataGridViewButtonColumn();
            editButtoncolumn.HeaderText = "Edit";
            editButtoncolumn.Name = "Edit";
          

            dataGridView1.Columns.Add(editButtoncolumn);


            DataGridViewButtonColumn deleteButtoncolumn = new DataGridViewButtonColumn();
            deleteButtoncolumn.HeaderText = "Delete";
            deleteButtoncolumn.Name = "Delete";
           
            dataGridView1.Columns.Add(deleteButtoncolumn);

            foreach ( DataGridViewRow grid in dataGridView1.Rows) 
            {
                grid.Cells[4].Value = "Edit";
                grid.Cells[5].Value = "Delete";
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow gridrow = dataGridView1.Rows[e.RowIndex];
            try 
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete") 
                {
                    if(MessageBox.Show($"Are you sure you want to delete","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes) 
                    {
                        string studentid = (string)gridrow.Cells[0].Value;
                        string subjectName = (string)gridrow.Cells[2].Value;
                        if(DeleteStudent(studentid, subjectName) == "Success") 
                        {
                            MessageBox.Show("Successfully Deleted Student Data");
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                        else 
                        {
                            MessageBox.Show($"Student Data Not deleted - failed\n{DeleteStudent(studentid, subjectName)}");
                        }
                       
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name=="Edit")
                {
                    
                    string studentid = (string)gridrow.Cells[0].Value;
                    string studentName = (string)gridrow.Cells[1].Value;
                    string subjectName = (string)gridrow.Cells[2].Value;
                    int subjectmarks = (int)gridrow.Cells[3].Value;

                    Edit updateData = new Edit(studentid,studentName,subjectName,subjectmarks);
                    updateData.ShowDialog();
                }
                RefreshData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Processing the data: " +"\n"+ex.Message);
            }
            
                
            



        }

        public string DeleteStudent(string id,string subName)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand($"Select studentid from student where studentRollno = '{id}'",connection);
                SqlCommand cmd2 = new SqlCommand($"Select subjectid from subject where subjectName = '{subName}'",connection);
                connection.Open();
                SqlDataReader reader1 = cmd1.ExecuteReader();
                reader1.Read();
                int result1 = Convert.ToInt32(reader1["studentid"]);
                reader1.Close();

                SqlDataReader reader2 = cmd2.ExecuteReader();

                reader2.Read();
                int result2 = Convert.ToInt32(reader2["subjectid"]);
                reader2.Close();

                SqlCommand Command = new SqlCommand($"delete from studentmarks where studentid = {result1} and subjectid = {result2}", connection);
                
                Command.ExecuteNonQuery();

                connection.Close();

                return "Success";
            }
            catch (Exception ex)
            {
                connection.Close();
                return ex.Message;
            }
        }

        public void RefreshData() 
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            SqlDataAdapter da = new SqlDataAdapter($"Select student.StudentRollNo,student.StudentName,Subject.SubjectName,studentMarks.SubjectMarks from student\r\njoin studentMarks on studentMarks.StudentID = student.studentID join subject on Subject.SubjectId=studentMarks.SubjectID", connection);

            DataTable dt = new DataTable();

            da.Fill(dt);
            dataGridView1.DataSource = dt;

            DataGridViewButtonColumn editButtoncolumn = new DataGridViewButtonColumn();
            editButtoncolumn.HeaderText = "Edit";
            editButtoncolumn.Name = "Edit";


            dataGridView1.Columns.Add(editButtoncolumn);


            DataGridViewButtonColumn deleteButtoncolumn = new DataGridViewButtonColumn();
            deleteButtoncolumn.HeaderText = "Delete";
            deleteButtoncolumn.Name = "Delete";

            dataGridView1.Columns.Add(deleteButtoncolumn);

            foreach (DataGridViewRow grid in dataGridView1.Rows)
            {
                grid.Cells[4].Value = "Edit";
                grid.Cells[5].Value = "Delete";
            }

        }

        private void Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectDataSet.StudentMarks' table. You can move, or remove it, as needed.
            this.studentMarksTableAdapter.Fill(this.projectDataSet.StudentMarks);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = null;
            //dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();
            RefreshData();
        }
    }  

        
}


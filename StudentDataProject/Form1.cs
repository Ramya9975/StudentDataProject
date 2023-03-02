using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDataProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sourceFolderPath = "C:\\StudentData\\Unprocessed\\StudentMarks-1.csv";
            string destinationFolderPath = "C:\\StudentData\\Processed\\StudentMarks-1.csv";
            string destinationFilePath = "C:\\StudentData\\Error\\StudentMarks-1.csv";
            Student student1 = new Student();
            StudentProcess stud1 = new StudentProcess();
            string[] files = Directory.GetFiles("C:\\StudentData\\Unprocessed");
            if (files.Length == 0)
            {
                MessageBox.Show("Folder is empty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SqlConnection con1 = new SqlConnection("Server=.\\SQLEXPRESS; database=project;Integrated security = true;");
            con1.Open();

            // Process each file
            foreach (string file in files)
            {
                try
                {
                    // Read the CSV file and process the data
                    int rowCount = student1.ProcessCsvFile(file);
                    bool value = stud1.ProcessingintoSql();

                    // Move the file to the Processed folder
                    //string destinationFilePath1 = Path.Combine(destinationFolderPath, Path.GetFileName(file));
                    if (value)
                    {
                        //File.Move(sourceFolderPath, destinationFolderPath);

                        // SqlCommand Arrange = new SqlCommand("SELECT subjectid, studentid, subjectmarks FROM studentMarks GROUP BY studentid",con1);
                        //Arrange.ExecuteNonQuery();
                        MessageBox.Show("File processed successfully. Rows: " + rowCount.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con1.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error Processing the data");
                    }
                }
                catch (Exception ex)
                {

                    // File.Move(sourceFolderPath, destinationFilePath);


                    MessageBox.Show("Error while processing file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Data data = new Data();
            data.Show();

        }
    }
    }

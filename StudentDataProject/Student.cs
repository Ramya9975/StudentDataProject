using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataProject
{
    internal class Student
    {
        public int ProcessCsvFile(String S)
        {
            string Path = "C:\\StudentData\\Unprocessed\\StudentMarks-1.csv";
            string[] lines = File.ReadAllLines(Path);
            int rowCount = lines.Length - 1;
            return rowCount;

        }
    }
    public class StudentProcess
    {
        public bool ProcessingintoSql()
        {
            SqlConnection con = new SqlConnection("Server=.\\SQLEXPRESS; database=project;Integrated security = true;");
            con.Open();
            using (StreamReader reader = new StreamReader("StudentMarks-1.csv"))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {

                    string[] values = line.Split(',');
                    string studentName = values[0];
                    string studentRollNo = values[1];
                    string studentMark = values[2];
                    string subjectName = values[3];
                    if (studentName != "StudentName")
                    {

                        SqlCommand cmd1 = new SqlCommand($"Select studentid from student where studentName ='{studentName}' ", con);
                        SqlCommand cmd2 = new SqlCommand($"Select subjectid from subject where subjectName ='{subjectName}'", con);
                        SqlDataReader dr = cmd1.ExecuteReader();
                        dr.Read();
                        int studentid = Convert.ToInt32(dr["studentid"]);
                        dr.Close();
                        SqlDataReader dr1 = cmd2.ExecuteReader();
                        dr1.Read();
                        int subjectid = Convert.ToInt32(dr1["subjectid"]);
                        dr1.Close();
                        SqlCommand cmd3 = new SqlCommand($"\r\ninsert into StudentMarks(SubjectId,StudentId,SubjectMarks)\r\nvalues({subjectid},{studentid},{studentMark})", con);
                        cmd3.ExecuteNonQuery();







                    }
                }
            }
            con.Close();
            SqlCommand groupbycmd = new SqlCommand("select StudentId,SubjectId,SubjectMarks From StudentMarks group By StudentId,SubjectId,SubjectMarks \r\n");

            return true;
        }
    }
}

    


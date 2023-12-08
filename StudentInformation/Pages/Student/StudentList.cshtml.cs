using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentInformation.Pages.Student
{
    public class StudentListModel : PageModel
    {
        public List<StudentInfo> listStudents = new List<StudentInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-3GUDF32T;Initial Catalog=studentinformation;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM student";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studentInfo = new StudentInfo();
                                studentInfo.Id = reader.GetInt32(0);
                                studentInfo.Name = reader.GetString(1);
                                studentInfo.City = reader.GetString(2);
                                studentInfo.CourseId = "" + reader.GetInt32(3);

                                listStudents.Add(studentInfo);
                            }
                        }
                    }
                }
            }
            catch 
            {
                Console.WriteLine("Exception");
            }
        }
    }

    public class StudentInfo
    {
        public int Id;
        public string Name;
        public string City;
        public string CourseId;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentInformation.Pages.Course
{
    public class CourseListModel : PageModel
    {
		public List<CourseInfo> listCourses = new List<CourseInfo>();
		public void OnGet()
        {
			try
			{
				String connectionString = "Data Source=LAPTOP-3GUDF32T;Initial Catalog=studentinformation;Integrated Security=True;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM course";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								CourseInfo courseInfo = new CourseInfo();
								courseInfo.Id = reader.GetInt32(0);
								courseInfo.CourseName = reader.GetString(1);
								courseInfo.LectureName = reader.GetString(2);

								listCourses.Add(courseInfo);
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
	public class CourseInfo
	{
		public int Id;
		public string CourseName;
		public string LectureName;
	}
}

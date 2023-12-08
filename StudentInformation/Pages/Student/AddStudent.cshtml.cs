using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentInformation.Pages.Course;
using System.Data.SqlClient;

namespace StudentInformation.Pages.Student
{
    public class AddStudentModel : PageModel
    {

		private readonly ILogger<AddStudentModel> _logger;

		public StudentInfo studentInfo = new StudentInfo();
		public String successMessage = "";


		public void OnGet()
        {
        }

		public void OnPost()
		{
			studentInfo.Name = Request.Form["name"];
			studentInfo.City = Request.Form["city"];
			studentInfo.CourseId = Request.Form["cID"];

			try
			{
				String connectionString = "Data Source=LAPTOP-3GUDF32T;Initial Catalog=studentinformation;Integrated Security=True;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					//String sql = "INSERT INTO course" +
					//             "(cName, lectureName) VALUES " +
					//             "(@name, @lectureName);";

					String sql = "INSERT INTO student (sName, city, cID) VALUES (@name, @city, @cID);";


					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", studentInfo.Name);
						command.Parameters.AddWithValue("@city", studentInfo.City);
						command.Parameters.AddWithValue("@cID", studentInfo.CourseId);

						//command.ExecuteNonQuery();

						//Response.Redirect("/Course/CourseList");

						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							// Redirect only if the operation was successful
							Response.Redirect("/Student/StudentList");
						}
						else
						{
							// Handle the case where no rows were affected (optional)
						}
					}

				}
			}
			catch (Exception ex)
			{
				// Log the exception using ILogger
				_logger.LogError(ex, "An error occurred while processing the request.");

				// Optionally, set an error message for the user
				ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
			}
		}
	}
}

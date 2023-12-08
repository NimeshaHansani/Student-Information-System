using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentInformation.Pages.Course
{
    public class AddCourseModel : PageModel
    {

		private readonly ILogger<AddCourseModel> _logger;

		public CourseInfo courseInfo = new CourseInfo();
        public String successMessage = "";

		public AddCourseModel(ILogger<AddCourseModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
        {
        }

        public void OnPost() 
        {
            courseInfo.CourseName = Request.Form["name"];
			courseInfo.LectureName = Request.Form["lectureName"];

            try
            {
				String connectionString = "Data Source=LAPTOP-3GUDF32T;Initial Catalog=studentinformation;Integrated Security=True;";
				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //String sql = "INSERT INTO course" +
                    //             "(cName, lectureName) VALUES " +
                    //             "(@name, @lectureName);";

					String sql = "INSERT INTO course (cName, lectureName) VALUES (@name, @lectureName);";


					using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", courseInfo.CourseName);
                        command.Parameters.AddWithValue("@lectureName", courseInfo.LectureName);

                        //command.ExecuteNonQuery();

						//Response.Redirect("/Course/CourseList");

						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							// Redirect only if the operation was successful
							Response.Redirect("/Course/CourseList");
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Register;
using System.Configuration;

namespace RTSVolunteerSystem.Pages.Login
{
    public class IndexModel : PageModel
    {
        public VolunteerInfo volunteerInfo = new VolunteerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            volunteerInfo.email = Request.Form["email"];
            volunteerInfo.password = Request.Form["password"];

            if (volunteerInfo.email.Length == 0 || volunteerInfo.password.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            // Check login information

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=rtsvolunteersystemdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM volunteers " +
                                 "WHERE email = @email and password = @password";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", volunteerInfo.email);
                        command.Parameters.AddWithValue("@password", volunteerInfo.password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {   
                                errorMessage = "Please provide correct email and/or password";
                                return;
                            }
                            else
                            {
                                volunteerInfo.id = "" + reader.GetInt32(0);
                                volunteerInfo.fullname = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Logged In Successfully";
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(5);
            Response.Cookies.Append("id", volunteerInfo.id, cookies);
            Response.Cookies.Append("fullname", volunteerInfo.fullname, cookies);
            Response.Cookies.Append("email", volunteerInfo.email, cookies);
            volunteerInfo.email = "";
            volunteerInfo.password = "";
            
            Response.Redirect("/Index");
        }
    }
}

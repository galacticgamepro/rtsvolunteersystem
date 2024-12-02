using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace RTSVolunteerSystem.Pages.Teams
{
    public class EditModel : PageModel
    {
        public TeamInfo teamInfo = new TeamInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=rtsvolunteersystemdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM teams WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                teamInfo.id = "" + reader.GetInt32(0);
                                teamInfo.teamname = reader.GetString(1);
                                teamInfo.contactname = reader.GetString(2);
                                teamInfo.contactemail = reader.GetString(2);
                                teamInfo.contactphone = reader.GetString(3);
                                teamInfo.description = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            teamInfo.id = Request.Form["id"];
            teamInfo.teamname = Request.Form["teamname"];
            teamInfo.contactname = Request.Form["contactname"];
            teamInfo.contactemail = Request.Form["contactemail"];
            teamInfo.contactphone = Request.Form["contactphone"];
            teamInfo.description = Request.Form["description"];

            if (teamInfo.id.Length == 0 || teamInfo.teamname.Length == 0 ||
                teamInfo.contactname.Length == 0 || teamInfo.contactemail.Length == 0 ||
                teamInfo.contactphone.Length == 0 || teamInfo.description.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=rtsvolunteersystemdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE teams " +
                                 "SET teamname=@teamname, contactname=@contactname, contactemail=@contactemail, contactphone=@contactphone, description=@description " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", teamInfo.id);
                        command.Parameters.AddWithValue("@teamname", teamInfo.teamname);
                        command.Parameters.AddWithValue("@contactname", teamInfo.contactname);
                        command.Parameters.AddWithValue("@contactemail", teamInfo.contactemail);
                        command.Parameters.AddWithValue("@contactphone", teamInfo.contactphone);
                        command.Parameters.AddWithValue("@description", teamInfo.description);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Teams/Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Register;

namespace RTSVolunteerSystem.Pages.Teams
{
    public class CreateModel : PageModel
    {
        public TeamInfo teamInfo = new TeamInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            teamInfo.teamname = Request.Form["teamname"];
            teamInfo.contactname = Request.Form["contactname"];
            teamInfo.contactemail = Request.Form["contactemail"];
            teamInfo.contactphone = Request.Form["contactphone"];
            teamInfo.description = Request.Form["description"];

            if (teamInfo.teamname.Length == 0 || teamInfo.contactname.Length == 0 ||
            teamInfo.contactemail.Length == 0 || teamInfo.contactphone.Length == 0 ||
            teamInfo.description.Length == 0)
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
                    String sql = "INSERT INTO teams " +
                                 "(teamname, contactname, contactemail, contactphone, description) VALUES " +
                                 "(@teamname, @contactname, @contactemail, @contactphone, @description)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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

            teamInfo.teamname = "";
            teamInfo.contactname = "";
            teamInfo.contactemail = "";
            teamInfo.contactphone = "";
            teamInfo.description = "";
            successMessage = "New Team Added Successfully";

            Response.Redirect("/Teams/Index");
        }
    }
}

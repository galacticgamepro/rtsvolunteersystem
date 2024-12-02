using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Teams;

namespace RTSVolunteerSystem.Pages.Log_Hours
{
    public class IndexModel : PageModel
    {
        public HoursInfo hoursInfo = new HoursInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String cookieid = Request.Cookies["id"];
            if (cookieid != null)
            {
                hoursInfo.volunteerID = cookieid;
            }
        }

        public void OnPost() 
        {
            hoursInfo.volunteerID = Request.Form["volunteerID"];
            hoursInfo.date = Request.Form["date"];
            hoursInfo.teamname = Request.Form["teamname"];
            hoursInfo.hours = Request.Form["hours"];
            hoursInfo.description = Request.Form["description"];

            if (hoursInfo.volunteerID.Length == 0 || hoursInfo.date.Length == 0 ||
            hoursInfo.teamname.Length == 0 || hoursInfo.hours.Length == 0 ||
            hoursInfo.description.Length == 0)
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
                    String sql = "INSERT INTO hours " +
                                 "(volunteerID, date, teamname, hours, description) VALUES " +
                                 "(@volunteerID, @date, @teamname, @hours, @description)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@volunteerID", hoursInfo.volunteerID);
                        command.Parameters.AddWithValue("@date", hoursInfo.date);
                        command.Parameters.AddWithValue("@teamname", hoursInfo.teamname);
                        command.Parameters.AddWithValue("@hours", hoursInfo.hours);
                        command.Parameters.AddWithValue("@description", hoursInfo.description);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            hoursInfo.volunteerID = "";
            hoursInfo.date = "";
            hoursInfo.teamname = "";
            hoursInfo.hours = "";
            hoursInfo.description = "";
            successMessage = "Hours Added Successfully";
        }

        /*public void populateTeamsList()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=rtsvolunteersystemdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM teams";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, connection);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    teamnameselect.DataSource = dt;
                    teamnameselect.DataTextField = "Team Name";
                    teamnameselect.DateValueField = "Team Name";
                    teamnameselect.DateBind();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }*/
    }

    public class HoursInfo
    {
        public String volunteerID;
        public String volunteername;
        public String date;
        public String teamname;
        public String hours;
        public String description;
        public String created_at;
    }
}

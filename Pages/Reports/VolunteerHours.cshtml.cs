using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Log_Hours;

namespace RTSVolunteerSystem.Pages.Reports
{
    public class VolunteerHourscshtmlModel : PageModel
    {
        public List<HoursInfo> listHours = new List<HoursInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=rtsvolunteersystemdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT volunteers.fullname, hours.date, hours.teamname, hours.hours, hours.description, hours.created_at FROM hours\r\nINNER JOIN volunteers\r\nON hours.volunteerid = volunteers.id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HoursInfo hoursInfo = new HoursInfo();
                                hoursInfo.volunteername = reader.GetString(0);
                                hoursInfo.date = reader.GetDateTime(1).ToString();
                                hoursInfo.teamname = reader.GetString(2);
                                hoursInfo.hours = "" + reader.GetInt32(3);
                                hoursInfo.description = reader.GetString(4);
                                hoursInfo.created_at = reader.GetDateTime(5).ToString();

                                listHours.Add(hoursInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace RTSVolunteerSystem.Pages.Teams
{
    public class IndexModel : PageModel
    {
        public List<TeamInfo> listTeams = new List<TeamInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=rtsvolunteersystemdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM teams";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TeamInfo teamInfo = new TeamInfo();
                                teamInfo.id = "" + reader.GetInt32(0);
                                teamInfo.teamname = reader.GetString(1);
                                teamInfo.contactname = reader.GetString(2);
                                teamInfo.contactemail = reader.GetString(3);
                                teamInfo.contactphone = reader.GetString(4);
                                teamInfo.description = reader.GetString(5);
                                teamInfo.created_at = reader.GetDateTime(6).ToString();

                                listTeams.Add(teamInfo);
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

    public class TeamInfo
    {
        public String id;
        public String teamname;
        public String contactname;
        public String contactemail;
        public String contactphone;
        public String description;
        public String created_at;
    }
}

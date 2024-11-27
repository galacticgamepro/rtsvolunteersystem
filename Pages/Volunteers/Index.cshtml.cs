using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RTSVolunteerSystem.Pages.Register;

namespace RTSVolunteerSystem.Pages.Volunteers
{
    public class IndexModel : PageModel
    {
        public List<VolunteerInfo> listVolunteers = new List<VolunteerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=rtsvolunteersystemdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM volunteers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VolunteerInfo volunteerInfo = new VolunteerInfo();
                                volunteerInfo.id = "" + reader.GetInt32(0);
                                volunteerInfo.fullname = reader.GetString(1);
                                volunteerInfo.email = reader.GetString(2);
                                volunteerInfo.password = reader.GetString(3);
                                volunteerInfo.phone = reader.GetString(4);
                                volunteerInfo.address = reader.GetString(5);
                                volunteerInfo.ecname = reader.GetString(6);
                                volunteerInfo.ecphone = reader.GetString(7);
                                volunteerInfo.dob = reader.GetDateTime(8).ToString();
                                volunteerInfo.availablefrom = reader.GetDateTime(9).ToString();
                                volunteerInfo.availableto = reader.GetDateTime(10).ToString();
                                volunteerInfo.comments = reader.GetString(11);
                                volunteerInfo.created_at = reader.GetDateTime(12).ToString();

                                listVolunteers.Add(volunteerInfo);
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

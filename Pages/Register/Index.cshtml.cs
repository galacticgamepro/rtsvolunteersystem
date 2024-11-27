using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace RTSVolunteerSystem.Pages.Register
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
            volunteerInfo.fullname = Request.Form["fullname"];
            volunteerInfo.email = Request.Form["email"];
            volunteerInfo.password = Request.Form["password"];
            volunteerInfo.phone = Request.Form["phone"];
            volunteerInfo.address = Request.Form["address"];
            volunteerInfo.ecname = Request.Form["ecname"];
            volunteerInfo.ecphone = Request.Form["ecphone"];
            volunteerInfo.dob = Request.Form["dob"];
            volunteerInfo.availablefrom = Request.Form["availablefrom"];
            volunteerInfo.availableto = Request.Form["availableto"];
            volunteerInfo.comments = Request.Form["comments"];

            if (volunteerInfo.fullname.Length == 0 || volunteerInfo.email.Length == 0 ||
                volunteerInfo.password.Length == 0 || volunteerInfo.phone.Length == 0 ||
                volunteerInfo.address.Length == 0 || volunteerInfo.ecname.Length == 0 ||
                volunteerInfo.ecphone.Length == 0 || volunteerInfo.dob.Length == 0 || 
                volunteerInfo.availablefrom.Length == 0 || volunteerInfo.availableto.Length == 0 ||
                volunteerInfo.comments.Length == 0)
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
                    String sql = "INSERT INTO volunteers " +
                                 "(fullname, email, password, phone, address, ecname, ecphone, dob, availablefrom, availableto, comments) VALUES " +
                                 "(@fullname, @email, @password, @phone, @address, @ecname, @ecphone, @dob, @availablefrom, @availableto, @comments)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fullname", volunteerInfo.fullname);
                        command.Parameters.AddWithValue("@email", volunteerInfo.email);
                        command.Parameters.AddWithValue("@password", volunteerInfo.password);
                        command.Parameters.AddWithValue("@phone", volunteerInfo.phone);
                        command.Parameters.AddWithValue("@address", volunteerInfo.address);
                        command.Parameters.AddWithValue("@ecname", volunteerInfo.ecname);
                        command.Parameters.AddWithValue("@ecphone", volunteerInfo.ecphone);
                        command.Parameters.AddWithValue("@dob", volunteerInfo.dob);
                        command.Parameters.AddWithValue("@availablefrom", volunteerInfo.availablefrom);
                        command.Parameters.AddWithValue("@availableto", volunteerInfo.availableto);
                        command.Parameters.AddWithValue("@comments", volunteerInfo.comments);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            volunteerInfo.fullname = "";
            volunteerInfo.email = "";
            volunteerInfo.password = "";
            volunteerInfo.phone = "";
            volunteerInfo.address = "";
            volunteerInfo.ecname = "";
            volunteerInfo.ecphone = "";
            volunteerInfo.dob = "";
            volunteerInfo.availablefrom = "";
            volunteerInfo.availableto = "";
            volunteerInfo.comments = "";
            successMessage = "New Volunteer Added Successfully";

            // Response.Redirect("/Clients/Index");
        }
    }
    public class VolunteerInfo
    {
        public String id;
        public String fullname;
        public String email;
        public String password;
        public String phone;
        public String address;
        public String ecname;
        public String ecphone;
        public String dob;
        public String availablefrom;
        public String availableto;
        public String comments;
        public String created_at;
    }
}

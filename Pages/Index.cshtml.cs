using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RTSVolunteerSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public String volunteerID = "";
        public String volunteerFullName = "";
        public String volunteerEmail = "";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String cookieid = Request.Cookies["id"];
            if (cookieid != null)
            {
                volunteerID = cookieid;
            }

            String cookiefullname = Request.Cookies["fullname"];
            if (cookiefullname != null)
            {
                volunteerFullName = cookiefullname;
            }

            String cookieemail = Request.Cookies["email"];
            if (cookieemail != null)
            {
                volunteerEmail = cookieemail;
            }
        }
    }
}

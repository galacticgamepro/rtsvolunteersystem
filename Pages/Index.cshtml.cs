using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RTSVolunteerSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public String volunteerEmail = "";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String cookieemail = Request.Cookies["email"];
            if (cookieemail != null)
            {
                volunteerEmail = cookieemail;
            }
        }
    }
}

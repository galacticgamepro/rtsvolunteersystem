using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RTSVolunteerSystem.Pages.Logout
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if (Request.Cookies["email"] != null)
            {
                Response.Cookies.Delete("email");
            }

            Response.Redirect("/Index");
        }
    }
}

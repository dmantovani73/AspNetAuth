using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetAuth.Pages
{
    [Authorize(PolicyName.Over20)]
    public class Over20Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}

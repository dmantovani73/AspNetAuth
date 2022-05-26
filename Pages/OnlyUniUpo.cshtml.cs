using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetAuth.Pages
{
    [Authorize(PolicyName.OnlyUniUpo)]
    public class OnlyUniUpoModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetAuth.Pages
{
    public class XssModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string Message { get; set; }

        public void OnGet()
        { }

        public void OnPost()
        {
            if (!ModelState.IsValid) return;

            Message = $"Hello {Input.Name}";
        }

        public class InputModel
        {
            public string Name { get; set; } = default!;
        }
    }
}

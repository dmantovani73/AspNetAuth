#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetAuth.Pages
{
    // Se aggiungo l'attributo IgnoreAntiforgeryToken disabilito la generazione e il controllo del token anti-forgery esponendomi ad attacchi CSRF.
    //[IgnoreAntiforgeryToken]
    [Authorize]
    public class CsrfModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string Message { get; set; }

        public void OnGet()
        { }

        public void OnPost()
        {
            if (!ModelState.IsValid) return;

            Message = $"{Input.Amount}€ were transferred to the account {Input.Account}";
        }

        public class InputModel
        {
            // Numero di conto.
            public string Account { get; set; }

            public decimal Amount { get; set; }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Narciarze_v_2.Data;
using Narciarze_v_2.Models;

namespace Narciarze_v_2.Pages.Strefa_Klienta
{
    public class TrasyModel : PageModel
    {
        public List<Stoki> Stokis;
        
        private ITrasy Narty_V2 { get; }
        public TrasyModel(ITrasy narty_V2) 
        {
            Narty_V2 = narty_V2;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var stokis = await Narty_V2.GetStoki();
            Stokis = stokis.ToList();
            return Page();
        }
    }
}

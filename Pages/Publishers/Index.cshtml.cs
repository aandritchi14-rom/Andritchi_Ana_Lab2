using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Andritchi_Ana_Lab2.Models;
using Andritchi_Ana_Lab2.Data;

namespace Andritchi_Ana_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Andritchi_Ana_Lab2Context _context;

    public IndexModel(Andritchi_Ana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Publisher = await _context.Publisher.ToListAsync();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Andritchi_Ana_Lab2.Models;
using Andritchi_Ana_Lab2.Data;

namespace Andritchi_Ana_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Andritchi_Ana_Lab2Context _context;

    public IndexModel(Andritchi_Ana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Category = await _context.Category.ToListAsync();
    }
}

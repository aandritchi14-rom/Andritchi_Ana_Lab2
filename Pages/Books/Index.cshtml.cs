using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Andritchi_Ana_Lab2.Data;
using Andritchi_Ana_Lab2.Models;

namespace Andritchi_Ana_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Andritchi_Ana_Lab2Context _context;

        public IndexModel(Andritchi_Ana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? AuthorID { get; set; }
        public SelectList Authors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .AsQueryable();

            if (AuthorID.HasValue)
                query = query.Where(b => b.AuthorID == AuthorID);

            Book = await query.ToListAsync();
            Authors = new SelectList(await _context.Author.ToListAsync(), "ID", "FullName");
        }
    }
}
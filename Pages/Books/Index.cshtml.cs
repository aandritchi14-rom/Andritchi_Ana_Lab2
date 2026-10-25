using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public BookData BookD { get; set; } = default!;
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? AuthorID { get; set; }
        public SelectList Authors { get; set; } = default!;

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookD = new BookData();

            var query = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .AsQueryable();

            if (AuthorID.HasValue)
                query = query.Where(b => b.AuthorID == AuthorID);

            BookD.Books = await query.OrderBy(b => b.Title).ToListAsync();

            Authors = new SelectList(await _context.Author.ToListAsync(), "ID", "FullName");

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books.Where(i => i.ID == id.Value).Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }
    }
}
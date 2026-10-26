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

        public string TitleSort { get; set; } = "";
        public string AuthorSort { get; set; } = "";
        public string CurrentFilter { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public int? AuthorID { get; set; }
        public SelectList Authors { get; set; } = default!;

        public async Task OnGetAsync(int? id, int? categoryID, string? sortOrder, string? searchString)
        {
            BookD = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString ?? "";

            var query = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .AsQueryable();

            if (AuthorID.HasValue)
                query = query.Where(b => b.AuthorID == AuthorID);

            var books = await query.OrderBy(b => b.Title).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s =>
                    (s.Author != null && (s.Author.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        || s.Author.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
                    || s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author?.FullName).ToList();
                    break;
                case "author":
                    books = books.OrderBy(s => s.Author?.FullName).ToList();
                    break;
                default:
                    books = books.OrderBy(s => s.Title).ToList();
                    break;
            }

            BookD.Books = books;

            Authors = new SelectList(await _context.Author.ToListAsync(), "ID", "FullName");

            if (id != null)
            {
                BookID = id.Value;
                Book? book = BookD.Books.Where(i => i.ID == id.Value).SingleOrDefault();
                if (book != null)
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }
    }
}
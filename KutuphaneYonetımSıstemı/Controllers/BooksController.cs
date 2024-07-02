using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneYonetımSıstemı.Models;
using KutuphaneYonetımSıstemı.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KutuphaneYonetımSıstemı.Controllers
{
    public class BooksController : Controller
    {
        private readonly KutuphaneContext _context;

        public BooksController(KutuphaneContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = _context.Book.Include(b => b.genre);
            return View(await books.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(_context.Genre, "Id", "genre_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,book_name,book_author,genreId")] BookRequest book)
        {
            if (ModelState.IsValid)
            {
                var b = new Book
                {
                    book_name = book.book_name,
                    book_author = book.book_author,
                    genreId = book.genreId
                };
                _context.Add(b);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = new SelectList(_context.Genre, "Id", "genre_name", book.genreId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.Genres = new SelectList(_context.Genre, "Id", "genre_name", book.genreId);
            return View(book);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,book_name,book_author,genreId")] BookRequest book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var b = new Book
                    {
                        Id = book.Id,
                        book_name = book.book_name,
                        book_author = book.book_author,
                        genreId = book.genreId
                    };
                    _context.Update(b);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                
                foreach (var error in errors)
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            ViewBag.Genres = new SelectList(_context.Genre, "Id", "genre_name", book.genreId);
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(b => b.Id == id);
        }
    }
}

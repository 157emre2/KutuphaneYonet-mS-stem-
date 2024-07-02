using KutuphaneYonetımSıstemı.Data;
using KutuphaneYonetımSıstemı.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetımSıstemı.Controllers
{
    public class GenresController : Controller
    {
        private readonly KutuphaneContext _context;

        public GenresController(KutuphaneContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var genres = _context.Genre.Include(g => g.Books).ToList();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,genre_name")] GenreRequest genre)
        {
            if (ModelState.IsValid)
            {
                var g = new Genre
                {
                    genre_name = genre.genre_name
                };
                _context.Add(g);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        public IActionResult Edit(int? id)
        {
            var genre = _context.Genre.Find(id);
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,genre_name")] GenreRequest genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var g = new Genre
                    {
                        Id = genre.Id,
                        genre_name = genre.genre_name
                    };
                    _context.Update(g);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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
            return View(genre);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = _context.Genre
                .FirstOrDefault(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
            return _context.Genre.Any(e => e.Id == id);
        }


    }
}

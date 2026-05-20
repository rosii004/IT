using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        private readonly WebApplication1Context _context;

        public BookController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Book
// Index прима параметар "search" од формата
// Ако search е пополнет, филтрира книги по наслов
        public async Task<IActionResult> Index(string search)
        {
            ViewBag.Search = search; // го враќа назад во полето

            var books = _context.Book
                .Include(b => b.LibraryBranch) // вклучи ги податоците за филијалата
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                books = books.Where(b => b.Title.Contains(search));

            return View(await books.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.LibraryBranch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["LibraryBranchId"] = new SelectList(_context.Set<LibraryBranch>(), "Id", "Id");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,ISBN,LibraryBranchId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibraryBranchId"] = new SelectList(_context.Set<LibraryBranch>(), "Id", "Id", book.LibraryBranchId);
            return View(book);
        }

        // GET: Book/Edit/5
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
            ViewData["LibraryBranchId"] = new SelectList(_context.Set<LibraryBranch>(), "Id", "Id", book.LibraryBranchId);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,ISBN,LibraryBranchId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
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
            ViewData["LibraryBranchId"] = new SelectList(_context.Set<LibraryBranch>(), "Id", "Id", book.LibraryBranchId);
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.LibraryBranch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
        // GET - прикажи форма за додавање член
// Се повикува кога се кликне копчето "Додади член"
        public async Task<IActionResult> AddMember(int id)
        {
            var book = await _context.Book
                .Include(b => b.Members)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            // Испраќаме информации до View-то
            ViewBag.BookId = id;
            ViewBag.BookName = $"{book.Author} - {book.Title}";
            // SelectList = листа за паѓачко мени
            ViewBag.Members = new SelectList(_context.Member, "Id", "FullName");

            return View();
        }

// POST - зачувај го избраниот член за книгата
        [HttpPost]
        public async Task<IActionResult> AddMember(int bookId, int memberId)
        {
            var book = await _context.Book
                .Include(b => b.Members)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            var member = await _context.Member.FindAsync(memberId);

            if (book == null || member == null) return NotFound();

            book.Members.Add(member); // го додаваме членот кон книгата
            await _context.SaveChangesAsync(); // зачувај во базата

            return RedirectToAction("Index"); // врати се на листата
        }
    }
}

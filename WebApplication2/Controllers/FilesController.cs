using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using System.Web;
using WebApplication2.Models;
using System.IO;

namespace WebApplication2.Controllers
{
    public class FilesController : Controller
    {
        private readonly WebApplication2Context _context;
        private int _userId;

        public FilesController(WebApplication2Context context)
        {
            _context = context;
            _userId = int.Parse(System.IO.File.ReadAllText(@"C:\\Кодинг\\Cloud\\WebApplication2\\WebApplication2\temp.txt"));
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var files = await _context.File.ToListAsync();
            var userFiles = from file in files
                            where file.UserId == _userId
                            select file;
            return _context.File != null ? 
                View(userFiles) :
                Problem("Entity set 'WebApplication2Context.File'  is null.");
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var @file = await _context.File
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@file == null)
            {
                return NotFound();
            }

            return View(@file);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,path,DateCreated")] Models.File @file,*/ IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                // Read the file content into a byte array
                byte[] fileContent;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    fileContent = memoryStream.ToArray();
                }

                // Create a new instance of the file entity
                var fileEntity = new Models.File
                {
                    path = file.FileName,
                    DateCreated = DateTime.Now,
                    Content = fileContent,
                    UserId = _userId
                    
                };

                // Save the file entity to the database
                await _context.File.AddAsync(fileEntity);
                await _context.SaveChangesAsync();

                // Optionally, you can return a success message or redirect to another page
                ViewBag.Message = "File uploaded successfully.";
            }
            else
            {
                ViewBag.Message = "No file selected.";
            }

            return View();

            
        }

        //[HttpPost]
        //public IActionResult Upload(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        // Process the uploaded file
        //        // You can save the file to disk, store it in a database, or perform any other required operations
        //        string fileName = Path.GetFileName(file.FileName);
        //        string path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }

        //        // Optionally, you can return a success message or redirect to another page
        //        ViewBag.Message = "File uploaded successfully.";
        //    }
        //    else
        //    {
        //        ViewBag.Message = "No file selected.";
        //    }

        //    return View();
        //}


        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var @file = await _context.File.FindAsync(id);
            if (@file == null)
            {
                return NotFound();
            }
            return View(@file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,path,DateCreated,DateModified")] Models.File @file)
        {
            if (id != @file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(@file.Id))
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
            return View(@file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var @file = await _context.File
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@file == null)
            {
                return NotFound();
            }

            return View(@file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.File == null)
            {
                return Problem("Entity set 'WebApplication2Context.File'  is null.");
            }
            var @file = await _context.File.FindAsync(id);
            if (@file != null)
            {
                _context.File.Remove(@file);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
          return (_context.File?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

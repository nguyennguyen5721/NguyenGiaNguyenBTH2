using NguyenGiaNguyenBTH2.Data;
using NguyenGiaNguyenBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NguyenGiaNguyenBTH2.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>Index()
        {
            var model = await _context.Student.ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            if(ModelState.IsValid)
            {
                _context.add(std);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
    }
}
 //Get : Student/edit/5
 public async Task<IActionResult> Edit(string id)
 {
    if (id ==null)
    {
        return NotFound();

    }
    var student = await _context.Student.FindAsync(id);
    if (student ==null)
    {
        return NotFound();
    }
    return View(student);
 }
        //Post:Student/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,[Bind("StudentID,StudentName")] Student std  )
        
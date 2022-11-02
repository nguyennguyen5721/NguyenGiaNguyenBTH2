using System.ComponentModel.DataAnnotations;
using NguyenGiaNguyenBTH2.Models.Process;
using NguyenGiaNguyenBTH2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace NguyenGiaNguyenBTH2.Controllers
{
public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;
    private ExcelProcess _excelProcess = new  ExcelProcess();
    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }
    //get : Employee
    public async Task<IActionResult> Index()
    {
        return View(await _context.Employee.ToListAsync());

    }
    private bool EmployeeExists(string id)
    {
        return _context.Employee.Any( e => e , EmpId == id);
    }
}
public async Task<IActionResult>Upload()
{
    return view();
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Upload(IFormFile file)
{
    if (file != null)
    {
        string fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension != ".xls" && fileExtension != ".xlsx")
        {
            ModelState.AddModelError("","Please choose excel file to upload!");
        }
        else
        {
            //rename file when  upload to server 
            var filenName = DateTime.Now.ToShortTimeString() + fileExtension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload.Excels",filenName);
            var fileLocation = new FileInfo(filePath).ToString();
            using (var stream = new FileStream(filePath,FileMode.Create))
            {
                //save file to server   
                await file.CopyToAsync(stream);
                //read data from file and write to database 
                var dt = _excelProcess.ExcelToDataTable(fileLocation);
                //using for loop to read data from dt 
                for (int i=0 ;i<dt.Rows.Count;i++)
                {
                    //createa a new Employee object
                    var emp = new Employee();
                    //set values for attributes
                    emp.EmpID = dt.Rows[i][0].ToString();
                    emp.EmpName = dt.Rows[i][1].ToString();
                    emp.Address = dt.Rows[i][2].ToString();
                    //add object to context
                    _context.Employee.Add(emp);
                }
                //save to database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
    return view();
}

}
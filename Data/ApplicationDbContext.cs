using NguyenGiaNguyenBTH2.Models;
using Microsoft.EntityFrameworkCore;
namespace NguyenGiaNguyenBTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public class ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public Dbset<Student> Students{ get; set;}
        public Dbset<NguyenGiaNguyenBTH2.Models.Employee> Employee{ get; set;}
    }
}
using System.ComponentModel.DataAnnotations
namespace NguyenGiaNguyenBTH2.Models
{
    public class Employee
    {
        [key]
        public string EmpID {get; set;}
        public string EmpName {get;set;}
        public string Address {get;set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Department
    {
        public int ID { get; set; }
        public String Name { get; set; }= string.Empty;
        public List<Staff> Staffs { get; set; }=new List<Staff>();

    }
}
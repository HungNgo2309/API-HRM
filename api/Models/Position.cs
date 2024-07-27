using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Position
    {
        public int ID {get;set;}
        public int Role { get; set; }// 0 là nhân viên ,1 là trưởng nhóm,2 là trưởng phòng, 3 là giám đốc
        public String? Name {get;set;}
        public List<Staff> Staffs { get; set; }=new List<Staff>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Staff
{
    public class CreateStaffRequestDto
    {
        public String Name { get; set; }=string.Empty;
        public String Email { get; set; }=string.Empty;
        public int Phone { get; set; }
        public String Password { get; set; }=string.Empty;
        public String TelegramID{get;set;}=string.Empty;
    }
}
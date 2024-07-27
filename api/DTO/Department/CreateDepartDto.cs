using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Department
{
    public class CreateDepartDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Tên phòng ban phải lớn hơn 5 ký tự")]
        [MaxLength(50, ErrorMessage ="Tên phòng ban không được vượt quá 50 ký tự")]
        public String Name { get; set; }= string.Empty;
    }
}
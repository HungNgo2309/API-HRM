using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.LeaveApplication
{
    public class UpdateLeaveDto
    {
        [Required(ErrorMessage = "Bạn phải điền mã nhân viên")]
        public int? StaffID { get; set; }
        [Required]
        [MinLength(5,ErrorMessage ="Nội dung phải lớn hơn 5 ký tự")]
        [MaxLength(250, ErrorMessage ="Nội dung không được vượt quá 250 ký tự")]
        public String Content { get; set; }=string.Empty;

        [Required(ErrorMessage = "Bạn phải chọn giá trị true hoặc false")]
        public bool Above { get; set; }
        [Required(ErrorMessage = "Bạn phải chọn lý do xin nghỉ phép")]
        public int? ReasonCategoryID { get; set; }

        [Required(ErrorMessage = "Ngày bắt buộc phải được cung cấp")]
        [DataType(DataType.Date, ErrorMessage = "Định dạng ngày không hợp lệ")]
        public DateTime StartDay { get; set; }
        [Required(ErrorMessage = "Ngày bắt buộc phải được cung cấp")]
        [DataType(DataType.Date, ErrorMessage = "Định dạng ngày không hợp lệ")]
        public DateTime EndDay { get; set; }
        public String Location { get; set; }=string.Empty;
        public bool State { get; set; }
        public String Reject_Reason { get; set; }= string.Empty;
        public int? AccepterID {get;set;}
        public int? SuperiorID  {get;set;}
        public int? AuthorizerID {get;set;}
    }
}
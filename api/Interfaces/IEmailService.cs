using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task<bool> SendEmailInfo(LeaveApplication leaveApplication);
        Task<bool> EmailForStaffRelate(StaffRelate staffRelate);
    }
}
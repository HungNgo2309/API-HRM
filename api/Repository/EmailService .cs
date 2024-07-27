using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Globalization;
using System.Text;
using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Repository
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IStaffRepository _staffRepo;
        private readonly ApplicationDBContext _context;

        public EmailService(IOptions<SmtpSettings> smtpSettings,IStaffRepository staffRepository,ApplicationDBContext context)
        {
            _smtpSettings = smtpSettings.Value;
            _staffRepo = staffRepository;
            _context=context;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = message };

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpSettings.Server, _smtpSettings.Port,  SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task<bool> SendEmailInfo(LeaveApplication leaveApplication)
        {
            if (leaveApplication.StaffID.HasValue)
            {
                CultureInfo vietnameseCulture = new CultureInfo("vi-VN");
                var listStaff = await _staffRepo.FindSuperior(leaveApplication.StaffID.Value);
                if (listStaff == null) // Ensure listStaff is not null
                {
                    throw new InvalidOperationException("Superior list is null.");
                }

                string title = "Đơn xin nghỉ phép";
                var reason = await _context.ResonCategorys.FirstOrDefaultAsync(c=>c.ID==leaveApplication.ReasonCategoryID);
               
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendLine(leaveApplication.Content ?? ""); // Ensure Content is not null
                messageBuilder.AppendLine("Nhân viên: " + (leaveApplication.Staff?.Name ?? "N/A")); // Ensure Staff and Staff.Name are not null
                messageBuilder.AppendLine("Mã nhân viên: " + leaveApplication.StaffID);
                messageBuilder.AppendLine("Địa chỉ xin nghỉ: " + (leaveApplication.Location ?? "N/A")); // Ensure Location is not null
                messageBuilder.AppendLine("Lý do: " + (reason?.Name ?? "N/A")); // Ensure ReasonCategory and ReasonCategory.Name are not null
                messageBuilder.AppendLine("Thời gian từ ngày: " + leaveApplication.StartDay.ToString("dd/MM/yyyy HH:mm:ss", vietnameseCulture));
                messageBuilder.AppendLine("Đến ngày: " + leaveApplication.EndDay.ToString("dd/MM/yyyy HH:mm:ss", vietnameseCulture));

                if (leaveApplication.AuthorizerID.HasValue)
                {
                    var authorizer = await _staffRepo.GetByIdAsync(leaveApplication.AuthorizerID.Value);
                    if (authorizer != null)
                    {
                        messageBuilder.AppendLine("Người ủy quyền: " + authorizer.Name + " " + leaveApplication.AuthorizerID);
                        listStaff.Add(authorizer); // Add authorizer to listStaff
                    }
                }
                 if(leaveApplication.State)
                {
                    title="Đơn xin nghỉ phép của "+leaveApplication.Staff?.Name +" đã đươc duyệt";
                }else if(!leaveApplication.State&&leaveApplication.Reject_Reason!=""){
                    title="Đơn xin nghỉ phép "+leaveApplication.Staff?.Name +" bị từ chối";
                    messageBuilder.AppendLine("Lý do từ chối " + leaveApplication.Reject_Reason);
                }
                if (leaveApplication.SuperiorID.HasValue)
                {
                    var superior = await _staffRepo.GetByIdAsync(leaveApplication.SuperiorID.Value);
                    if (superior != null)
                    {
                        listStaff.Add(superior); // Add superior to listStaff
                    }
                }
                if (leaveApplication.StaffID.HasValue)
                {
                    var staff =await _staffRepo.GetByIdAsync(leaveApplication.StaffID.Value);
                    if(staff!=null)
                    { 
                        listStaff.Add(staff);
                    }
                }
                string message = messageBuilder.ToString();
                for (int i = 0; i < listStaff.Count; i++)
                {
                    await SendEmailAsync(listStaff[i].Email, title, message);
                    if(listStaff[i].TelegramID!=null)
                    {
                        if(leaveApplication.State)
                        {
                            await SendMessageTelegram.SendMessageToTelegram(listStaff[i].TelegramID,"Đơn xin nghỉ phép của "+leaveApplication.Staff?.Name+" đã được duyệt");
                            Console.WriteLine(listStaff[i].TelegramID+listStaff[i].Name);
                        }else if(!leaveApplication.State&& leaveApplication.Reject_Reason!="")
                        {
                            await SendMessageTelegram.SendMessageToTelegram(listStaff[i].TelegramID,"Đơn xin nghỉ phép của "+leaveApplication.Staff?.Name+" đã bị từ chối. Lý do là "+leaveApplication.Reject_Reason);
                        }else{
                            await SendMessageTelegram.SendMessageToTelegram(listStaff[i].TelegramID,message);
                        } 
                    }
                }
                return true;
            }
            return false;
        }
        public async Task<bool> EmailForStaffRelate(StaffRelate staffRelate)
        {
            var leaveApplication= await _context.LeaveApplications.SingleOrDefaultAsync(c=>c.ID==staffRelate.LeaveApplicationID);
            if (leaveApplication!=null && leaveApplication.StaffID.HasValue)
            {
                var mainstaff =_context.Staffs.FirstOrDefault(c=>c.ID==leaveApplication.StaffID);
                CultureInfo vietnameseCulture = new CultureInfo("vi-VN");
                string title = "Đơn xin nghỉ phép";
                var reason = await _context.ResonCategorys.FirstOrDefaultAsync(c=>c.ID==leaveApplication.ReasonCategoryID);
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendLine(leaveApplication.Content ?? ""); // Ensure Content is not null
                messageBuilder.AppendLine("Nhân viên: " + (mainstaff?.Name ?? "N/A")); // Ensure Staff and Staff.Name are not null
                messageBuilder.AppendLine("Mã nhân viên: " + leaveApplication.StaffID);
                messageBuilder.AppendLine("Địa chỉ xin nghỉ: " + (leaveApplication.Location ?? "N/A")); // Ensure Location is not null
                messageBuilder.AppendLine("Lý do: " + (reason?.Name ?? "N/A")); // Ensure ReasonCategory and ReasonCategory.Name are not null
                messageBuilder.AppendLine("Thời gian từ ngày: " + leaveApplication.StartDay.ToString("dd/MM/yyyy HH:mm:ss", vietnameseCulture));
                messageBuilder.AppendLine("Đến ngày: " + leaveApplication.EndDay.ToString("dd/MM/yyyy HH:mm:ss", vietnameseCulture));

                if (leaveApplication.AuthorizerID.HasValue)
                {
                    var authorizer = await _staffRepo.GetByIdAsync(leaveApplication.AuthorizerID.Value);
                    if (authorizer != null)
                    {
                        messageBuilder.AppendLine("Người ủy quyền: " + authorizer.Name + " " + leaveApplication.AuthorizerID); // Add authorizer to listStaff
                    }
                }
                if(leaveApplication.State)
                {
                    title="Đơn xin nghỉ phép của "+mainstaff?.Name +" đã đươc duyệt";
                }else if(!leaveApplication.State&&leaveApplication.Reject_Reason!=""){
                    title="Đơn xin nghỉ phép "+mainstaff?.Name +" bị từ chối";
                    messageBuilder.AppendLine("Lý do từ chối " + leaveApplication.Reject_Reason);
                }
                string message = messageBuilder.ToString();
                var staff = await _context.Staffs.SingleOrDefaultAsync(c=>c.ID==staffRelate.ID_Staff);
                await SendEmailAsync(staff.Email,title,message);
                if(leaveApplication.State)
                {
                    await SendMessageTelegram.SendMessageToTelegram(staff.TelegramID,"Đơn xin nghỉ phép của "+mainstaff?.Name+" đã được duyệt");
                           
                }else if(!leaveApplication.State&& leaveApplication.Reject_Reason!="")
                    {
                    await SendMessageTelegram.SendMessageToTelegram(staff.TelegramID,"Đơn xin nghỉ phép của "+mainstaff?.Name+" đã bị từ chối. Lý do là "+leaveApplication.Reject_Reason);
                }else{
                       await SendMessageTelegram.SendMessageToTelegram(staff.TelegramID,message);
                    } 
                    return true;
            }
             return false;
        }
    }
}
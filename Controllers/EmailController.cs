using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AirBnB_for_Campers___TAKE_HOME_EXAM.Models; // Import the namespace where the Email class is defined


namespace AirBnB_for_Campers___TAKE_HOME_EXAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        // POST: api/email/send
        [HttpPost("send")]
        public async Task<IActionResult> SendEmailAsync([FromBody] Email emailModel)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(emailModel.To));
                message.From = new MailAddress("sunwoods21@gmail.com"); // Your email address
                message.Subject = emailModel.Subject;
                message.Body = emailModel.Body;
                message.IsBodyHtml = true; // You can set it to true if sending HTML emails

                // Use XOAUTH2 authentication
                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587; // SMTP port (Gmail uses 587)
                    smtp.EnableSsl = true;

                    // Authenticate using OAuth 2.0
                    smtp.Credentials = new NetworkCredential("sunwoods21@gmail.com", "sluo rame rhtz oviz");

                    await smtp.SendMailAsync(message);
                }

                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }
    }

   
}

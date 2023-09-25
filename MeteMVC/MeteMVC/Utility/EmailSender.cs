using Microsoft.AspNetCore.Identity.UI.Services;

namespace MeteMVC.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Buraya email Gönderme işlemlerimizi yapabiliriz 
            return Task.CompletedTask;
        }
    }
}

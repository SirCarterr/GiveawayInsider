using GiveAwayInsider_API.Service.IService;
using GiveAwayInsider_DataAccess;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Mail;
using SendGrid;
using GiveAwayInsider_Models;
using GiveAwayInsider_Business.Repository.IRepository;
using Newtonsoft.Json.Linq;

namespace GiveAwayInsider_API.Service
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotificationRepository _notificationRepository;

        public EmailService(UserManager<AppUser> userManager, ISendGridClient sendGridClient, INotificationRepository notificationRepository)
        {
            _userManager = userManager;
            _sendGridClient = sendGridClient;
            _notificationRepository = notificationRepository;
        }

        public async Task<ResponseModelDTO> SendNotification(IEnumerable<GiveawayDTO> giveaways, int notificationId)
        {
            NotificationDTO notification = await _notificationRepository.Get(notificationId);

            if (notification != null)
            {
                string link = $"https://localhost:7260?phrase={notification.Search.ToLower()}";
                link += !string.IsNullOrEmpty(notification.Platform) ? $"&platform={notification.Platform.ToLower()}" : "";
                link += !string.IsNullOrEmpty(notification.Type) ? $"&type={notification.Type.ToLower()}" : "";
                link += !string.IsNullOrEmpty(notification.Sort) ? $"&sortBy={notification.Sort.ToLower()}" : "";

                var from = new EmailAddress("test.company.707@gmail.com", "Giveaway Insider");
                var subject = "Giveaway Notification!";
                var to = new EmailAddress(notification.UserEmail);
                var plainTextContent = GenereateText(giveaways, notification.Search, link);
                var htmlContent = GenereateHtml(giveaways, notification.Search, link);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await _sendGridClient.SendEmailAsync(msg);
                if (response.IsSuccessStatusCode)
                {
                    return new ResponseModelDTO()
                    {
                        StatusCode = (int)response.StatusCode,
                        IsSuccess = true
                    };
                }
                else
                {
                    return new ResponseModelDTO()
                    {
                        StatusCode = (int)response.StatusCode,
                        IsSuccess = false
                    };
                }
            }
            return new ResponseModelDTO()
            {
                StatusCode = 404,
                IsSuccess = false
            };
        }

        public async Task<ResponseModelDTO> SendPasswordRecovery(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                string link = $"https://localhost:7260/account/recovery?token={token}&email={email}";

                var from = new EmailAddress("test.company.707@gmail.com", "Giveaway Insider");
                var subject = "Password Recovery";
                var to = new EmailAddress(email);
                var plainTextContent = $"Your password recovery link:\n{link}\n\nG.I. Support";
                var htmlContent = $"<h2>Your password recovery link:</h2><br/><h3>{link}</h3><br/><i>G.I. Support<i/><br/><span>P.s.: If not you sended the letter ignore/delete it<span/>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await _sendGridClient.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    return new ResponseModelDTO()
                    {
                        StatusCode = (int)response.StatusCode,
                        IsSuccess = true,
                        Data = token
                    };
                }
                else
                {
                    return new ResponseModelDTO()
                    {
                        StatusCode = (int)response.StatusCode,
                        IsSuccess = false,
                        Errors = "Recovery sending attempt failed"
                    };
                }
            }
            return new ResponseModelDTO()
            {
                StatusCode = 404,
                IsSuccess = false,
                Errors = $"User with {email} email not exist"
            };
        }

        private string GenereateHtml(IEnumerable<GiveawayDTO> giveaways,string title, string link)
        {
            string html = $"<h2>Current \'{title}\' giveaways:</h2><br/>";

            foreach (var g in giveaways)
            {
                html += $"<h3>{g.Title}</h3><b>{g.Type}</b> / <b>{g.Platforms}</b><p>{g.Instructions}</p><a href=\"{g.Open_Giveaway_Url}\">Loot</a><br/>";
            }

            html += $"<br/><a href=\"{link}\" target=\"_blank\">See more</a>";
            return html;
        }

        private string GenereateText(IEnumerable<GiveawayDTO> giveaways, string title, string link)
        {
            string text = $"Current \'{title}\' giveaways:";

            foreach (var g in giveaways)
            {
                text += $"{g.Title}\n\n{g.Type}\t/\t{g.Platforms}\n\n{g.Instructions}\n\nlink: {g.Open_Giveaway_Url}\n\n\n";
            }

            text += "See more: " + link + '\n';
            return text;
        }
    }
}

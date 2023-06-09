﻿using GiveAwayInsider_Models;
using GiveAwayInsider_Notifiier.Service;
using GiveAwayInsider_Notifiier.Service.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Notifiier
{
    public static class Program
    {
        private static readonly HttpClient _client = new() { BaseAddress = new Uri("https://localhost:7131/") };

        public static async Task Main(string[] args)
        {
            _client.DefaultRequestHeaders.Add("X-API-Key", "<your custom apikey>");
            INotificationService notificationService = new NotificationService(_client);
            INotifierService notifierService = new NotifierService(_client);
            Console.WriteLine("Notifier started\n");

            while (true)
            {
                if (DateTime.Now.Hour == 12)
                {
                    Console.WriteLine("Notifying started:");
                    IEnumerable<NotificationDTO> notifications = await notificationService.GetNotifications();
                    foreach (NotificationDTO notification in notifications)
                    {
                        var specificGiveaways = await GetSpecific(notification);
                        bool success = await notifierService.Notify(specificGiveaways, notification.Id);
                        Console.WriteLine(success ? $"Notification for {notification.UserEmail} is sent" : $"Notifying for {notification.UserEmail} is failed");
                        Thread.Sleep(500);
                    }
                    Console.WriteLine("Notifying ended\n");
                    Thread.Sleep(TimeSpan.FromMinutes(2));
                }
            }
        }

        private static async Task<IEnumerable<GiveawayDTO>> GetSpecific(NotificationDTO notification)
        {
            var response = await _client.GetAsync($"api/giveaway?platform={notification.Platform}&type={notification.Type}&sortBy={notification.Sort}");
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<GiveawayDTO>>(contentTemp);
                return result!.Where(g => g.Title.Contains(notification.Search)).Take(5);
            }
            return new List<GiveawayDTO>();
        }
    }
}

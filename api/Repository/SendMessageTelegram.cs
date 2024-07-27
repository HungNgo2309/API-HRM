using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace api.Repository
{
    public class SendMessageTelegram
    {
        private static readonly string BotToken = "7467609449:AAEhJdmrmtF5tDFUndGcWsv437z4pJy4_1A";
        // private static readonly string ChatId=string.Empty;
        // private static readonly string Message=string.Empty;
        //private static readonly string ChatId = "7348115211"; // Thay thế bằng user ID của người nhận 7335898218
        // private static readonly string Message = "Hello, this is a test message from your .NET Core app!";
        private static async Task GetChatId()
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"https://api.telegram.org/bot{BotToken}/getUpdates";
                var response = await httpClient.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseContent);

                var updates = JObject.Parse(responseContent)["result"];
                if (updates != null)
                {
                    foreach (var update in updates)
                    {
                        var chatId = update["message"]?["chat"]?["id"];
                        if (chatId != null)
                        {
                            Console.WriteLine($"Chat ID: {chatId}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No updates found.");
                }
            }
        }
        public static async Task SendMessageToTelegram(string ChatId,string Message)
        {
            await GetChatId();
            using (var httpClient = new HttpClient())
            {
                var url = $"https://api.telegram.org/bot{BotToken}/sendMessage?chat_id={ChatId}&text={Uri.EscapeDataString(Message)}";
                var response = await httpClient.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseContent);
            }
        }
    }
}
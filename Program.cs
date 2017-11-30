using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using Telegram.Bot;

namespace ReturnerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => RunBot());
            while (Console.ReadKey().KeyChar != '~');
        }
        static async Task RunBot()
        {

            try
            {
                Console.WriteLine("Press '~' for exit.");
                Console.WriteLine("Bot is initializing...");
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.APIToken))
                {
                    Console.WriteLine("Please enter your API: ");
                    string rAPI = Console.ReadLine();
                    string API = Properties.Settings.Default.APIToken;
                    Properties.Settings.Default.APIToken = rAPI;
                    var Bot = new TelegramBotClient(API);
                    var Me = await Bot.GetMeAsync();
                    Console.WriteLine($"{Me.Username}");
                    Console.WriteLine($"{Me.Username} has been initialized.");
                    int offset = 0;
                    while (true)
                    {
                        var updates = await Bot.GetUpdatesAsync(offset);
                        foreach (var update in updates)
                        {
                            offset = update.Id + 1;
                            var ChatId = update.Message.Chat.Id;
                            Console.WriteLine($"{update.Message.From.FirstName} {update.Message.From.LastName} ({update.Message.From.Username}): {update.Message.Text}");
                            Console.Write("You: ");
                            string msg = Console.ReadLine();
                            //var Message = update.Message.Text;
                            await Bot.SendTextMessageAsync(chatId: ChatId, text: msg);
                            continue;
                        }
                    }
                }

                else
                {
                    
                    Console.WriteLine("Please enter your API: ");
                    string rAPI = Console.ReadLine();
                    string API = rAPI;
                    var Bot = new TelegramBotClient(API);
                    var Me = await Bot.GetMeAsync();
                    Console.WriteLine($"{Me.Username}");
                    Console.WriteLine($"{Me.Username} has been initialized.");
                    int offset = 0;
                    while (true)
                    {
                        var updates = await Bot.GetUpdatesAsync(offset);
                        foreach (var update in updates)
                        {
                            offset = update.Id + 1;
                            var ChatId = update.Message.Chat.Id;
                            Console.WriteLine($"{update.Message.From.FirstName} {update.Message.From.LastName} ({update.Message.From.Username}): {update.Message.Text}");
                            Console.Write("You: ");
                            string msg = Console.ReadLine();
                            //var Message = update.Message.Text;
                            await Bot.SendTextMessageAsync(chatId: ChatId, text: msg);
                            continue;
                        }
                    }

                }
            }

            catch (Exception error)
            {

                Console.WriteLine(error.Message + " & we`ll reset for you in 5 seconds.");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                var t = Task.Run(() => RunBot());
            }

        }
    }
}

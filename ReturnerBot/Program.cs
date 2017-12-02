﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using Telegram.Bot;

namespace ReturnerBot
{
    class Program
    {
        #region
        private static string fileName = "";
        private static string image_save_url = $"C://robot//{fileName}.png";
        private static string video_save_url = $"C://robot//{fileName}.mp4";
        private static string music_save_url = $"C://robot//{fileName}.mp3";
        private static string ogg_save_url = $"C://robot//{fileName}.ogg";
        private static string gif_save_url = $"C://robot//{fileName}.gif";
        #endregion
        static void Main(string[] args)
        {
            var t = Task.Run(() => RunBot());
            //Close console with this key, This can be great for self-Chatting with user feature.
            while (Console.ReadKey().KeyChar != '~') ;
        }
        #region
        static async Task RunBot()
        {
            try
            {
                Console.WriteLine("Bot is initializing...");
                Console.WriteLine("Please enter your API: ");
                string API = Console.ReadLine();
                //Token has been sent by admin
                var Bot = new TelegramBotClient(API);
                //Bot starting to be initialize.
                var Me = await Bot.GetMeAsync();
                Console.WriteLine($"{Me.Username}");
                //initialized.
                Console.WriteLine($"{Me.Username} has been initialized.");
                Console.WriteLine("\n");
                //Clear all of additional out puts
                System.Threading.Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine("Press '~' for exit.");
                Console.WriteLine($"{Me.Username} is ready to receive message.\n");

                #endregion
                //offset is defined to let the robot detect which message didnt get answer.
                int offset = 0;
                //continues the loop
                while (true)
                {

                    var updates = await Bot.GetUpdatesAsync(offset);
                    foreach (var update in updates)
                    {
                        //everytime make the robot answer the newest messages, because of the incremention of ID
                        offset = update.Id + 1;

                        //ChatId used for answer.
                        var ChatId = update.Message.Chat.Id;

                        //MessageId is used to reply the answer to the request.
                        var MessageId = update.Message.MessageId;

                        //security definitions
                        List<string> Users = new List<string>() { "milad_xandi", "Mrgoong", "sara_amiini", "lmnzl", "sodizandi" };
                        List<string> Symbols = new List<string>() { "```", "`", "[", "]", "*","-" };
                        #region
                        /*if (Users.Contains(update.Message.Chat.Username))
                        {*/
                        if (update.Message.Text != null)
                        {
                            //make out put more interactive.
                            Console.WriteLine($"{update.Message.From.FirstName} {update.Message.From.LastName} ({update.Message.From.Username}): {update.Message.Text}");

                            if (update.Message.Chat.Type != Telegram.Bot.Types.Enums.ChatType.Private)
                            {
                                //do not let the robot to answer in the groups.
                                continue;
                            }
                            else if (update.Message.Text.Contains("/start"))
                            {
                                //Definition for the pre defined /start command of telegram bots.
                                await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: $@"سلام {update.Message.Chat.FirstName}, به روبات انتشار سرویس ابری ما خوش آمدی!
این روبات قابلیت دریافت و ذخیره، ارسال و بارگذاری و برخی فعالیت های جانبی دیگر مانند:
        1.تولید کد
        2.بازگشت دادن انواع پرونده
        3.تولید متن هایپرلینک
        4.ویرایش محتوا
را دارا میباشد.
برای شروع 'help' را تایپ کنید.");

                            }
                            #region
                            else if ((update.Message.Text.Contains("http") ||
                                update.Message.Text.Contains("https") ||
                                update.Message.Text.Contains(".ir") ||
                                update.Message.Text.Contains("https") ||
                                update.Message.Text.Contains(".com") ||
                                update.Message.Text.Contains(".net") ||
                                update.Message.Text.Contains("link") ||
                                update.Message.Text.Contains("help") ||
                                update.Message.Text.Contains("Help") ||
                                update.Message.Text.Contains("www") ||
                                update.Message.Text.Contains(".org") ||
                                update.Message.Text.Contains("code")
                                ))
                            {
                                var Message = update.Message.Text;
                                if (Message.Contains("[") || Message.Contains("]") || Message.Contains("(") || Message.Contains(")") || Message.Contains(":") || Message.Contains(";") || Message.Contains("@") || Message.Contains("*") || Message.Contains("_") || Message.Contains("`") || Message.Contains("```"))
                                {
                                    //reply back the pre defined commands
                                    await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: Message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                                    continue;
                                }
                                else
                                {
                                    //guides
                                    await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: @"لطفا برای استفاده از امکانات توسعه دهندگان از این الگو استفاده کنید:
                                *متن بلد*
                                _متن ایتالیک_
                                [متن](http://www.example.com/)
                                [متن](tg://user?id=123456789)
                                `کد`
                                ```بلاک زبان
                                کد پیش تنظیم شده Fixed Width
                                ```");
                                }
                            }
                            #endregion
                            else if (update.Message.Text == "ShowOurSpecialListUsers")
                            {
                                foreach (var item in Users)
                                {
                                    //show the special persons list.
                                    await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: item);
                                }
                            }
                            else
                            {
                                //let the robot take your answer and send it to user by the console.
                                //string Message = Console.ReadLine();

                                var Message = update.Message.Text;
                                int Before = Message.IndexOf("@");
                                if (Message.Contains("@")&&Before>=4)
                                {
                                    string End = Message.Remove(Before-4);

                                    await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: End + @"

@MyCoderRobot");
                                    continue;
                                }

                                if (Symbols.Contains(Message))
                                {
                                    await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: Message+@"

@MyCoderRobot",parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                                }
                                else
                                {
                                    await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: Message + @"

@MyCoderRobot");
                                }
                                //remove @ form the content

                                //make a better user-friendly interface
                                //Console.Write("You: ");

                                //replying with our own id

                            }

                        }
                        else if (update.Message.Photo != null)
                        {
                            //getting caption
                            var Caption = update.Message.Caption;

                            //getting file id of the last received photo
                            //it`s just for photos and for other data types there is no need to the " LastOrDefault() " context
                            //you should use a code like this: update.Message.Video.FileId;
                            var FileId = update.Message.Photo.LastOrDefault().FileId;
                            await Bot.SendTextMessageAsync(chatId: ChatId, text: $"در حال دریافت پرونده ...");

                            //Renaming the file
                            fileName = update.Message.Photo.LastOrDefault().FileSize.ToString();

                            //getting the file
                            var file = await Bot.GetFileAsync(fileId: FileId);

                            //checking if it has caption...
                            if (Caption != null)
                            {
                                //checking if there was any id in caption...
                                if (Caption.Contains("@"))
                                {
                                    //Define the location of saving file
                                    //You should read about System.IO library
                                    using (var saveFile = System.IO.File.Open(image_save_url, FileMode.OpenOrCreate))
                                    {
                                        //copy the downloaded file to the defined path
                                        await file.FileStream.CopyToAsync(saveFile);

                                        //making console more interactive
                                        Console.WriteLine($"New file has been received with {fileName}.png filename.");

                                        //let the user know what`s going on...
                                        await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.png در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                    }

                                    //start uploading from the defined path
                                    using (var stream = System.IO.File.Open(image_save_url, FileMode.OpenOrCreate))
                                    {
                                        //removing id with the prefix
                                        int Before = Caption.IndexOf("@");
                                        string End = Caption.Remove(Before - 4);
                                        Console.WriteLine($"{fileName}.png is uploading...");

                                        //sending file
                                        await Bot.SendPhotoAsync(chatId: ChatId, caption: End + @" @MyCoderRobot", photo: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                    }
                                }
                                else
                                {
                                    //if there was no caption
                                    using (var saveFile = System.IO.File.Open(image_save_url, FileMode.OpenOrCreate))
                                    {
                                        await file.FileStream.CopyToAsync(saveFile);
                                        Console.WriteLine($"New file has been received with {fileName}.png filename.");
                                        await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.png در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                    }

                                    using (var stream = System.IO.File.Open(image_save_url, FileMode.OpenOrCreate))
                                    {
                                        Console.WriteLine($"{fileName}.png is uploading...");
                                        await Bot.SendPhotoAsync(chatId: ChatId, caption: Caption + @"
@MyCoderRobot", photo: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                    }
                                }

                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(chatId: ChatId, text: $"در حال دریافت پرونده ...");
                                using (var saveFile = System.IO.File.Open(image_save_url, FileMode.OpenOrCreate))
                                {
                                    await file.FileStream.CopyToAsync(saveFile);
                                    Console.WriteLine($"New file has been received with {fileName}.png filename.");
                                    await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.png در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                }
                                using (var stream = System.IO.File.Open(image_save_url, FileMode.OpenOrCreate))
                                {
                                    Console.WriteLine($"{fileName}.png is uploading...");
                                    await Bot.SendPhotoAsync(chatId: ChatId, photo: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                }
                            }

                        }
                        else if (update.Message.Video != null)
                        {
                            var Caption = update.Message.Caption;
                            var FileId = update.Message.Video.FileId;
                            await Bot.SendTextMessageAsync(chatId: ChatId, text: $"در حال دریافت پرونده ...");
                            fileName = update.Message.Video.FileSize.ToString();
                            var file = await Bot.GetFileAsync(fileId: FileId);
                            if (Caption != null)
                            {

                                if (Caption.Contains("@"))
                                {
                                    int Before = Caption.IndexOf("@");
                                    string End = Caption.Remove(Before - 4);
                                    using (var saveFile = System.IO.File.Open(video_save_url, FileMode.OpenOrCreate))
                                    {
                                        await file.FileStream.CopyToAsync(saveFile);
                                        Console.WriteLine($"New file has been received with {fileName}.mp4 filename.");
                                        await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.mp4 در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                    }

                                    using (var stream = System.IO.File.Open(video_save_url, FileMode.OpenOrCreate))
                                    {
                                        Console.WriteLine($"{fileName}.mp4 is uploading...");
                                        await Bot.SendVideoAsync(chatId: ChatId, caption: End + @" @MyCoderRobot", video: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                    }
                                }
                                else
                                {
                                    using (var saveFile = System.IO.File.Open(video_save_url, FileMode.OpenOrCreate))
                                    {
                                        await file.FileStream.CopyToAsync(saveFile);
                                        Console.WriteLine($"New file has been received with {fileName}.mp4 filename.");
                                        await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.mp4 در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                    }

                                    using (var stream = System.IO.File.Open(video_save_url, FileMode.OpenOrCreate))
                                    {
                                        Console.WriteLine($"{fileName}.mp4 is uploading...");
                                        await Bot.SendVideoAsync(chatId: ChatId, caption: Caption + @"
@MyCoderRobot", video: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                    }
                                }

                            }
                            else
                            {
                                using (var saveFile = System.IO.File.Open(video_save_url, FileMode.OpenOrCreate))
                                {
                                    await file.FileStream.CopyToAsync(saveFile);
                                    Console.WriteLine($"New file has been received with {fileName}.mp4 filename.");
                                    await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.mp4 در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                }

                                using (var stream = System.IO.File.Open(video_save_url, FileMode.OpenOrCreate))
                                {
                                    Console.WriteLine($"{fileName}.mp4 is uploading...");
                                    await Bot.SendVideoAsync(chatId: ChatId, caption: Caption + @" @MyCoderRobot", video: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                }
                            }
                        }
                        else if (update.Message.Voice != null)
                        {
                            {
                                var FileId = update.Message.Voice.FileId;
                                var file = await Bot.GetFileAsync(fileId: FileId);
                                fileName = update.Message.Voice.FileSize.ToString();
                                await Bot.SendTextMessageAsync(chatId: ChatId, text: $"در حال دریافت پرونده ...");
                                using (var saveFile = System.IO.File.Open(ogg_save_url, FileMode.OpenOrCreate))
                                {
                                    await file.FileStream.CopyToAsync(saveFile);
                                    Console.WriteLine($"New file has been received with {fileName}.ogg filename.");
                                    await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.ogg در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                }
                                using (var stream = System.IO.File.Open(ogg_save_url, FileMode.OpenOrCreate))
                                {
                                    Console.WriteLine($"{fileName}.ogg is uploading...");
                                    await Bot.SendVoiceAsync(chatId: ChatId, voice: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                }
                            }
                        }
                        else if (update.Message.Audio != null)
                        {
                            {
                                var FileId = update.Message.Audio.FileId;
                                var Title = update.Message.Audio.Title;
                                var Duration = update.Message.Audio.Duration;
                                var Caption = update.Message.Caption;
                                var Performer = update.Message.Audio.Performer;
                                fileName = $@"{Title} {Caption} {Performer}";
                                await Bot.SendTextMessageAsync(chatId: ChatId, text: $"در حال دریافت پرونده ...");
                                fileName = update.Message.Audio.FileSize.ToString();
                                var file = await Bot.GetFileAsync(fileId: FileId);
                                if (Caption != null)
                                {

                                    if (Caption.Contains("@"))
                                    {
                                        int Before = Caption.IndexOf("@");
                                        string End = Caption.Remove(Before - 4);
                                        using (var saveFile = System.IO.File.Open(music_save_url, FileMode.OpenOrCreate))
                                        {
                                            await file.FileStream.CopyToAsync(saveFile);
                                            Console.WriteLine($"New file has been received with {fileName}.mp3 filename.");
                                            await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.mp3 در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                        }

                                        using (var stream = System.IO.File.Open(music_save_url, FileMode.OpenOrCreate))
                                        {
                                            Console.WriteLine($"{fileName}.mp3 is uploading...");
                                            await Bot.SendAudioAsync(chatId: ChatId, performer: Performer, title: Title, duration: Duration, caption: End + @" @MyCoderRobot", audio: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                        }
                                    }
                                    else
                                    {
                                        using (var saveFile = System.IO.File.Open(music_save_url, FileMode.OpenOrCreate))
                                        {
                                            await file.FileStream.CopyToAsync(saveFile);
                                            Console.WriteLine($"New file has been received with {fileName}.mp3 filename.");
                                            await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.mp3 در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                        }

                                        using (var stream = System.IO.File.Open(music_save_url, FileMode.OpenOrCreate))
                                        {
                                            Console.WriteLine($"{fileName}.mp3 is uploading...");
                                            await Bot.SendAudioAsync(chatId: ChatId, performer: Performer, title: Title, duration: Duration, caption: Caption + @"
@MyCoderRobot", audio: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                        }
                                    }

                                }
                                else
                                {
                                    using (var saveFile = System.IO.File.Open(music_save_url, FileMode.OpenOrCreate))
                                    {
                                        await file.FileStream.CopyToAsync(saveFile);
                                        Console.WriteLine($"New file has been received with {fileName}.mp3 filename.");
                                        await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.mp3 در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                    }

                                    using (var stream = System.IO.File.Open(music_save_url, FileMode.OpenOrCreate))
                                    {
                                        Console.WriteLine($"{fileName}.mp3 is uploading...");
                                        await Bot.SendAudioAsync(chatId: ChatId, performer: Performer, title: Title, duration: Duration, caption: Caption + @"@MyCoderRobot", audio: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                    }
                                }
                            }
                        }
                        //there is an additional type for Gif files with MimeeType to defined type of document.
                        else if (update.Message.Document != null && update.Message.Document.MimeType == "video/mp4")
                        {
                            var Caption = update.Message.Caption;
                            var FileId = update.Message.Document.FileId;

                            fileName = update.Message.Document.FileSize.ToString();
                            var file = await Bot.GetFileAsync(fileId: FileId);
                            await Bot.SendTextMessageAsync(chatId: ChatId, text: $"در حال دریافت پرونده ...");
                            if (Caption != null)
                            {
                                if (Caption.Contains("@"))
                                {
                                    int Before = Caption.IndexOf("@");
                                    string End = Caption.Remove(Before - 4);
                                    using (var saveFile = System.IO.File.Open(gif_save_url, FileMode.OpenOrCreate))
                                    {
                                        await file.FileStream.CopyToAsync(saveFile);
                                        Console.WriteLine($"New file has been received with {fileName}.gif filename.");
                                        await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.gif در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                    }

                                    using (var stream = System.IO.File.Open(gif_save_url, FileMode.OpenOrCreate))
                                    {
                                        Console.WriteLine($"{fileName}.gif is uploading...");
                                        await Bot.SendDocumentAsync(chatId: ChatId, caption: End + @" @MyCoderRobot", document: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                    }
                                }
                                else
                                {
                                    using (var saveFile = System.IO.File.Open(gif_save_url, FileMode.OpenOrCreate))
                                    {
                                        await file.FileStream.CopyToAsync(saveFile);
                                        Console.WriteLine($"New file has been received with {fileName}.gif filename.");
                                        await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.gif در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                    }

                                    using (var stream = System.IO.File.Open(gif_save_url, FileMode.OpenOrCreate))
                                    {
                                        Console.WriteLine($"{fileName}.gif is uploading...");
                                        await Bot.SendDocumentAsync(chatId: ChatId, caption: Caption + @"
@MyCoderRobot", document: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                    }
                                }

                            }
                            else
                            {
                                using (var saveFile = System.IO.File.Open(gif_save_url, FileMode.OpenOrCreate))
                                {
                                    await file.FileStream.CopyToAsync(saveFile);
                                    Console.WriteLine($"New file has been received with {fileName}.gif filename.");
                                    await Bot.SendTextMessageAsync(chatId: ChatId, text: $"پرونده ی شما دریافت شد و با نام {fileName}.gif در ابر ذخیره شد، منتظر بازگشت آن باشید ...");
                                }

                                using (var stream = System.IO.File.Open(gif_save_url, FileMode.OpenOrCreate))
                                {
                                    Console.WriteLine($"{fileName}.gif is uploading...");
                                    await Bot.SendDocumentAsync(chatId: ChatId, caption: Caption + @" @MyCoderRobot", document: new Telegram.Bot.Types.FileToSend($"{fileName}", stream), replyToMessageId: MessageId);
                                }
                            }
                        }
                        else
                        {
                            await Bot.SendTextMessageAsync(chatId: ChatId, text: $@"{update.Message.Chat.FirstName} عزیز؛
ما نتوانستیم پرونده ی ارسالی شما را پردازش کنیم، این موضوع مشخصا به دلیل فرمت آن است.
در آینده مشکل را حل خواهیم کرد!");
                        }
                        //}
                        #endregion
                        #region
                        /*else if (update.Message.Text == "CrackToLetMeCoding")
                        {
                            Users.Add(update.Message.Chat.Username.ToString());
                            await Bot.SendTextMessageAsync(chatId: ChatId, text: "نام کاربری شما پذیرفته شد، خوش آمدید!", replyToMessageId: MessageId);
                            continue;
                        }
                        else if (update.Message.Text == "ShowOurSpecialListUsers")
                        {
                            foreach (var item in Users)
                            {
                                await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: item);
                                continue;
                            }
                        }
                        else
                        {
                            await Bot.SendTextMessageAsync(chatId: ChatId, replyToMessageId: MessageId, text: $@"سلام {update.Message.Chat.FirstName}, 
متاسفانه این روبات برای استفاده شخصی طراحی شده است.
بنابراین قارد به استفاده از آن خواهید بود!");
                            continue;
                        }*/
                        #endregion
                    }
                }
            }
            #region
            catch (Telegram.Bot.Exceptions.ApiRequestException error)
            {
                Console.WriteLine($"{error.Message} , we`ll reset for you in 5 seconds.");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                var t = Task.Run(() => RunBot());
            }
            catch (NullReferenceException error)
            {
                Console.WriteLine($"{error.Message} , we`ll reset for you in 5 seconds.");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                var t = Task.Run(() => RunBot());
            }
            catch (ArgumentNullException error)
            {
                Console.WriteLine($"{error.Message} , we`ll reset for you in 5 seconds.");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                var t = Task.Run(() => RunBot());
            }
            catch (Exception error)
            {
                Console.WriteLine($"{error.Message} , we`ll reset for you in 5 seconds.");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                var t = Task.Run(() => RunBot());
            }
            #endregion
        }
    }
}

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotProject
{
    internal partial class BotManager
    {
        async Task MessageTextAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string message = update.Message.Text;
            if(message == "/start")
            {
                //hhdvkvdgcl,l,
                await OnSendStartMessageAsync(botClient, update, cancellationToken);
            }
        }

        private async Task OnSendStartMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(WorkingWithFile.ReadFromFile().Result.Find(user => user.Id == update.Message.MessageId) is null)
            {
                string message = "Ro'yxatdan o'tish uchun Leetcode usernameni kiriting";
                botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: message,
                    cancellationToken: cancellationToken);
            }
            else
            {
                string sendMessage = "1";
                ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(
                    new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            "Leetcodega o'tish",
                            "Natijalar"
                        }
                    });
                Message returnMessage = await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken,
                    text: sendMessage);
            }
        }
    }
}

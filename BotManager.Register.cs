using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotProject
{
    internal partial class BotManager
    {
        async Task RegisterUserAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationtoken)
        {
            List<TelegramUser> users = await WorkingWithFile.ReadFromFile();
            TelegramUser user = new TelegramUser()
            {
                Id = update.Message.Chat.Id,
                UserName = update.Message.From.Username
            };
            users.Add(user);
            await WorkingWithFile.WriteToFileAsync(users);

            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Registratsiya bo'ldi",
                cancellationToken: cancellationtoken
                );
        }
    }
}

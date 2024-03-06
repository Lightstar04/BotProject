using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotProject
{
    internal partial class BotManager
    {
        async Task CallBackQueryAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            await DeleteMessageAsync(client, update.CallbackQuery.Message.Chat.Id, cancellationToken);
            if (update.CallbackQuery.Data is "info")
            {
                Message message = await client.SendTextMessageAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Bu yerda Leetcode useri haqida ma'lumot bor",
                    replyMarkup: new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithCallbackData("⬅️ Asosiy menu", "menu")),
                    parseMode: ParseMode.Html);

                messageID = message.MessageId;
            }

            else if(update.CallbackQuery.Data is "menu")
            {
                await Menu(client, update.CallbackQuery.Message, cancellationToken);
            }
            else if(update.CallbackQuery.Data is "task")
            {
                await CommonButtonsAsync(client, update.CallbackQuery.Message, cancellationToken);
            }
            else if(update.CallbackQuery.Data is "update")
            {
                await UpdateInfoAsync(client, update.CallbackQuery.Message, cancellationToken);
            }

            delegateForBack = null;
            delegateForBack = Menu;
        }

        async Task CommonButtonsAsync(ITelegramBotClient client, Message message, CancellationToken token)
        {
            Message returnMessage = await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Darajani tanlang: ",
                replyMarkup: new InlineKeyboardMarkup(
                    new InlineKeyboardButton[][]
                    {
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Oson", "easy"),
                            InlineKeyboardButton.WithCallbackData("O'rta", "medium")
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Qiyin", "hard"),
                            InlineKeyboardButton.WithCallbackData("⬅️ Ortga", "back")
                        }
                    }));

            delegateForBack = null;
            delegateForBack = Menu;
        }

        async Task UpdateInfoAsync(ITelegramBotClient client, Message message, CancellationToken token)
        {
            Message updateMessage = await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Leetcode ma'lumotlarini yangilash",
                replyMarkup: new ReplyKeyboardMarkup(
                    new KeyboardButton[]
                    {
                        new KeyboardButton("⬅️ Ortga")
                    })
                { OneTimeKeyboard = true, ResizeKeyboard = true});
            delegateForBack = null;
            delegateForBack = Menu;
        }
    }
}

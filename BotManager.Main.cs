using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotProject
{
    delegate Task DelegateForBack(
        ITelegramBotClient client,
        Message message,
        CancellationToken token);
    internal partial class BotManager
    {

        DelegateForBack delegateForBack = null;
        int messageID = 0;
        public async Task Menu(ITelegramBotClient botClient, Message sendMessage, CancellationToken token)
        {
            Message message = await botClient.SendTextMessageAsync(
                chatId: sendMessage.Chat.Id,
                text: "Jarayonni tanlang",
                parseMode: ParseMode.Markdown,
                cancellationToken: token,
                replyMarkup: new InlineKeyboardMarkup(
                    new InlineKeyboardButton[][]
                    {
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithCallbackData("Akkaunt ma'lumotlarini olish", "info"),
                                InlineKeyboardButton.WithCallbackData("Kunlik vazifalarni olish", "task"),
                            },
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ma'lumotlarni yangilash", "update")
                            }
                    }));
            messageID = message.MessageId;
        }
        public async Task SendUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(update.CallbackQuery is not null)
            {
                await CallBackQueryAsync(botClient, update, cancellationToken);
            }

            else if(update.Message.Text is "/start")
            {
                await MessageTextAsync(botClient, update, cancellationToken);
            }
            
            else if(update.Message.Type == MessageType.Text)
            {
                await RegisterUserAsync(botClient, update, cancellationToken);
                await Menu(botClient, update.Message, cancellationToken);
            }
        }

        async Task DeleteMessageAsync(ITelegramBotClient client, long chatId, CancellationToken token)
        {
            if (messageID != 0)
            {
                await client.DeleteMessageAsync(
                    chatId: chatId,
                    messageId: messageID,
                    cancellationToken: token);
            }
        }
    }
}

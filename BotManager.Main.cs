using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotProject
{
    internal partial class BotManager
    {
        public async Task SendUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(update.Message.Type == MessageType.Text)
            {
                await MessageTextAsync(botClient, update, cancellationToken);
            }
        }
    }
}

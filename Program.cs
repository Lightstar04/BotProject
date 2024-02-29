using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace BotProject
{
    internal class Program
    {
        static BotManager botManager = new BotManager();
        const string token = "6994270115:AAEJmuUiC5Mj4OkcVnfLHO5td-FrHX5QZ-c";
        static void Main(string[] args)
        {
            TelegramBotClient botClient = new TelegramBotClient(token);
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ReceiverOptions receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = { }
            };

            botClient.StartReceiving(
                updateHandler: UpdateHandlerAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cancellationTokenSource.Token,
                pollingErrorHandler: ErrorHandlerAsync);

            Console.ReadKey();
        }

        static Task ErrorHandlerAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        static async Task UpdateHandlerAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if(update == null)
            {
                return;
            }
            else
            {
                await botManager.SendUpdateAsync(client, update, token);
            }
        }
    }
}

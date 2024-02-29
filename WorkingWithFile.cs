using System.Text.Json;

namespace BotProject
{
    internal class WorkingWithFile
    {
        public static async Task WriteToFileAsync(List<TelegramUser> users)
        { 
            using (StreamWriter sw = new StreamWriter(@"D:\C#\BotProject\users.json"))
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                string jsonData = JsonSerializer.Serialize(users, typeof(List<TelegramUser>), options);

                await sw.WriteAsync(jsonData);
            }
        }

        public static async Task<List<TelegramUser>> ReadFromFile()
        {
            using (StreamReader streamReader = new StreamReader(@"D:\C#\BotProject\users.json"))
            {
                string jsonData = await streamReader.ReadToEndAsync();

                List<TelegramUser> users = JsonSerializer.Deserialize<List<TelegramUser>>(jsonData);

                return users;
            }
        }
    }
}

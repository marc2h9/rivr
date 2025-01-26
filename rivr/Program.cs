using SlashCommandsBuilder;
using Discord;
using Discord.WebSocket;
using Discord.Net;
using Newtonsoft.Json;

public class Program
{
    private static DiscordSocketClient _client;

    public static async Task Main()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.Ready += Client_Ready;
        
        string tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "token.txt");
        var DISCORD_TOKEN = File.ReadAllText(tokenPath);

        await _client.LoginAsync(TokenType.Bot, DISCORD_TOKEN);
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public static async Task Client_Ready()
    {
        SlashCommandsList slashCommands = new SlashCommandsBuilder.SlashCommandsList();
        var commandBuilder = new SlashCommandBuilder();

        try
        {
            foreach(var command in slashCommands.commands())
            {
                commandBuilder.WithName(command.Name);
                commandBuilder.WithDescription(command.Description);
                await _client.CreateGlobalApplicationCommandAsync(commandBuilder.Build());
            }
        }
        catch(HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
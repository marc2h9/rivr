using SlashCommandsBuilder;
using Discord;
using Discord.WebSocket;
using Discord.Net;
using Newtonsoft.Json;

public class Program
{
    DiscordSocketConfig config = new()
    {
        UseInteractionSnowflakeDate = false
    };

    private static DiscordSocketClient _client;

    public static async Task Main()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.Ready += Client_Ready;
        _client.SlashCommandExecuted += SlashCommandHandler;
        
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

    private static async Task SlashCommandHandler(SocketSlashCommand command)
    {
        SlashCommandsList slashCommands = new SlashCommandsBuilder.SlashCommandsList();
        Console.WriteLine("Command: " + command.Data.Name);

        foreach(var slashCommand in slashCommands.commands())
        {
            if(slashCommand.Name == command.Data.Name)
            {
                await slashCommand.Execute(command);
            }
        }
    }

    public static async Task Client_Ready()
    {
        SlashCommandsList slashCommands = new SlashCommandsBuilder.SlashCommandsList();
        Console.WriteLine("Number of commands: " + slashCommands.commands().Count());

        try
        {
            foreach(var command in slashCommands.commands())
            {
                var commandBuilder = new SlashCommandBuilder();
                Console.WriteLine("Adding command: " + command.Name + " Type: " + command.CommandType);

                switch(command.CommandType)
                {
                    case "Slash":
                        commandBuilder.WithName(command.Name);
                        commandBuilder.WithDescription(command.Description);
                        await _client.CreateGlobalApplicationCommandAsync(commandBuilder.Build());
                        break;
                    /*  case "User":
                        TODO: Add user commands when framework allows it

                        commandBuilder.WithName(command.Name);
                        commandBuilder.WithDescription(command.Description);
                        await 
                        break;  */
                    default:
                        throw new Exception("Unknown command type: " + command.CommandType);
                        break;
                }
            }
        }
        catch(HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
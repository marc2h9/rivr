using SlashCommandsBuilder;
using Discord;
using Discord.WebSocket;

public class Ping : SlashCommand
{
    public Ping()
    {
        Name = "ping";
        Description = "Ping!";
        Type = ApplicationCommandType.Slash;
    }

    public async Task Execute(SocketSlashCommand command)
    {
        await command.RespondAsync("Pong!");
    }
}
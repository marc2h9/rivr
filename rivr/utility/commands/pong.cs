using SlashCommandsBuilder;
using Discord;
using Discord.WebSocket;

public class Pong : SlashCommand
{
    public Pong()
    {
        Name = "pong";
        Description = "Pong!";
        CommandType = "Slash";
    }

    public async Task Execute(SocketSlashCommand command)
    {
        await command.RespondAsync("Pong!");
    }
}
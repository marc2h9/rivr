using System;
using DotNetEnv;

using Discord;
using Discord.WebSocket;

// Starts Discord Bot
public class Program
{
    private static DiscordSocketClient _client;

    public static async Task Main()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;

        var root = Directory.GetCurrentDirectory();
        var dotenv = Path.Combine(root, "../../../.env");
        DotNetEnv.Env.Load(dotenv);

        var token = Environment.GetEnvironmentVariable("TOKEN");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg) 
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
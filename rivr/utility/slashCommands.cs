using System.Reflection;
using Discord;

namespace SlashCommandsBuilder
{
    public class SlashCommand
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ApplicationCommandType Type { get; set; }
    }

    public class SlashCommandsList()
    {
        // Slash command construction
        public List<SlashCommand> commands()
        {
            var commands_copy = new List<SlashCommand>();

            var assembly = Assembly.GetExecutingAssembly();
            var allTypes = assembly.GetTypes();
            var slashCommandTypes = allTypes.Where(myType => myType.IsSubclassOf(typeof(SlashCommand)));

            foreach(var type in slashCommandTypes)
            {
                var command = (SlashCommand)Activator.CreateInstance(type)!;
                commands_copy.Add(command);
            }
            
            // Return the list of items once we have looped through all types
            return commands_copy;
        }
        
    }
}
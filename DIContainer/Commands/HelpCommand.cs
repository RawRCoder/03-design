using System;
using System.Collections.Generic;
using System.IO;

namespace DIContainer.Commands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand(TextWriter outputStream, Lazy<IEnumerable<ICommand>> allCommands)
        {
            OutputStream = outputStream;
            AllCommands = allCommands;
        }

        private TextWriter OutputStream { get; }
        private Lazy<IEnumerable<ICommand>> AllCommands { get; }
        public override void Execute()
        {
            foreach (var cmd in AllCommands.Value)
                OutputStream.WriteLine($"\t{cmd.Name}");
        }
    }
}

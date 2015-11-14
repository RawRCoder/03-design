using System;
using System.IO;
using System.Threading;

namespace DIContainer.Commands
{
    public class TimerCommand : BaseCommand
    {
        private TextWriter OutputStream { get; }
        private readonly CommandLineArgs arguments;

        public TimerCommand(CommandLineArgs arguments, TextWriter outputStream)
        {
            this.arguments = arguments;
            OutputStream = outputStream;
        }

        public override void Execute()
        {
            var timeout = TimeSpan.FromMilliseconds(arguments.GetInt(0));
            OutputStream.WriteLine("Waiting for " + timeout);
            Thread.Sleep(timeout);
            OutputStream.WriteLine("Done!");
        }
    }
}
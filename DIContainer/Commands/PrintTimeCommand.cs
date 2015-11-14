using System;
using System.IO;

namespace DIContainer.Commands
{
    public class PrintTimeCommand : BaseCommand
    {
        public PrintTimeCommand(TextWriter outputStream)
        {
            OutputStream = outputStream;
        }

        private TextWriter OutputStream { get; }
        public override void Execute()
        {
            OutputStream.WriteLine(DateTime.Now);
        }
    }
}
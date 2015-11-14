using System;
using System.IO;
using System.Linq;
using DIContainer.Commands;
using Ninject;
using Ninject.Parameters;

namespace DIContainer
{
    public class Program
    {
        private TextWriter OutputStream { get; }
        private readonly CommandLineArgs arguments;
        private readonly ICommand[] commands;

        public Program(CommandLineArgs arguments, TextWriter outputStream, ICommand[] commands)
        {
            this.arguments = arguments;
            OutputStream = outputStream;
            this.commands = commands;
        
        }

        static void Main(string[] args)
        {
            var k = new StandardKernel();
            k.Bind<TextWriter>().ToConstant(Console.Out);
            k.Bind<CommandLineArgs>().To<CommandLineArgs>().WithConstructorArgument(args);
            k.Bind<ICommand>().To<PrintTimeCommand>();
            k.Bind<ICommand>().To<HelpCommand>();
            k.Bind<ICommand>().To<TimerCommand>();
            k.Get<Program>().Run();/*
            var printTime = k.Get<PrintTimeCommand>();
            var timer = k.Get<TimerCommand>();
            new Program(k.Get<CommandLineArgs>(), k.Get<TextWriter>(), printTime, timer).Run();*/
        }

        public void Run()
        {
            if (arguments.Command == null)
            {
                OutputStream.WriteLine("Please specify <command> as the first command line argument");
                return;
            }
            var command = commands.FirstOrDefault(c => c.Name.Equals(arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                OutputStream.WriteLine($"Sorry. Unknown command {arguments.Command}");
            else
                command.Execute();
        }
    }
}

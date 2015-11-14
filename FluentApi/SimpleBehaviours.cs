using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentTask
{
    public class SayBehavior : IBehavior
    {
        public SayBehavior(string message)
        {
            Message = message;
        }
        public string Message { get; }
        public void Execute()
        {
            Console.WriteLine(Message);
        }
    }
    public class JumpBehavior : SayBehavior
    {
        public JumpBehavior(JumpHeight height) : base("Я "+(height==JumpHeight.High?"ПРЫГНУЛ":"УПАЛ" )+" НАКУЙ!")
        {
            Height = height;
        }

        public JumpHeight Height { get; }
    }
    public class DelayBehavior : IBehavior
    {
        public DelayBehavior(TimeSpan period)
        {
            Period = period;
        }

        public TimeSpan Period { get; }
        public void Execute()
        {
            Thread.Sleep(Period);
        }
    }
    public class UntilKeyPressBehavior : IBehavior
    {
        public UntilKeyPressBehavior(Func<Behavior, Behavior> someAction)
        {
            SomeAction = someAction;
        }

        public Func<Behavior, Behavior> SomeAction { get; }
        public void Execute()
        {
            var behavior = new Behavior();
            SomeAction(behavior);
            while (!Console.KeyAvailable)
            {
                behavior.Execute();
            }
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
    }
}

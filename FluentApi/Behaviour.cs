using System;
using System.Collections.Generic;

namespace FluentTask
{
    public class Behavior
    {
        private List<IBehavior> Schedule { get; } = new List<IBehavior>();

        public Behavior Say(string what)
        {
            Schedule.Add(new SayBehavior(what));
            return this;
        }

        public Behavior UntilKeyPressed(Func<Behavior, Behavior> act)
        {
            Schedule.Add(new UntilKeyPressBehavior(act));
            return this;
        }

        public Behavior Jump(JumpHeight jumpHeight)
        {
            Schedule.Add(new JumpBehavior(jumpHeight));
            return this;
        }

        public Behavior Delay(TimeSpan secs)
        {
            Schedule.Add(new DelayBehavior(secs));
            return this;
        }

        public void Execute()
        {
            foreach (var action in Schedule)
                action.Execute();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jeeves
{
    public class CommandExecutor
    {
        private readonly Dictionary<Func<string, bool>, Action<string>> _commands;

        public CommandExecutor()
        {
            _commands = new Dictionary<Func<string, bool>, Action<string>>
            {
                {s => s.Equals("exit"), s => Environment.Exit(0)},
                {s => s.Equals("hello"), s => Console.WriteLine("hello trooper")},
                {s => s.StartsWith("say"), s => Console.WriteLine(s.Remove(0, 4))},
            };
        }
    }





}


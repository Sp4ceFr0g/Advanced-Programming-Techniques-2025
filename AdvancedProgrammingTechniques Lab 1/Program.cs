using System;
using System.Collections.Generic;

internal class Program
{
    static bool runs = true;

    public delegate void OnResult(string result);
    public delegate string ReadInput();

    interface Operation
    {
        void perform();
    }

    class Sum : Operation
    {
        private ReadInput read;
        private OnResult onResult;

        public Sum(ReadInput read, OnResult onResult)
        {
            this.read = read;
            this.onResult = onResult;
        }

        public void perform()
        {
            int firstTerm = int.Parse(read());
            int secondTerm = int.Parse(read());
            onResult((firstTerm + secondTerm).ToString());
        }
    }

    class Dif : Operation
    {
        private ReadInput read;
        private OnResult onResult;

        public Dif(ReadInput read, OnResult onResult)
        {
            this.read = read;
            this.onResult = onResult;
        }

        public void perform()
        {
            int firstTerm = int.Parse(read());
            int secondTerm = int.Parse(read());
            onResult((firstTerm - secondTerm).ToString());
        }
    }

    class Exit : Operation
    {
        private Action exitAction;
        private OnResult onResult;

        public Exit(Action exitAction, OnResult onResult)
        {
            this.exitAction = exitAction;
            this.onResult = onResult;
        }

        public void perform()
        {
            exitAction();
            onResult("Program end");
        }
    }

    private static void Main(string[] args)
    {
        var commands = new Dictionary<string, Operation>
        {
            { "sum", new Sum(Console.ReadLine, Console.WriteLine) },
            { "dif", new Dif(Console.ReadLine, Console.WriteLine) },
            { "exit", new Exit(() => runs = false, Console.WriteLine) }
        };

        while (runs)
        {
            string command = Console.ReadLine();
            if (commands.TryGetValue(command, out Operation op))
            {
                op.perform();
            }
            else
            {
                Console.WriteLine("Unknown command");
            }
        }
    }
}
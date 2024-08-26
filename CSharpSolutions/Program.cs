using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSolutions
{
    internal class Program
    {
        private static readonly object _lock = new();

        static void Main(string[] args)
        {
            Thread t1 = new(Write);
            Thread t2 = new(Read);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.ReadLine();
        }

        public static void Write()
        {
            Monitor.Enter(_lock);
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Write Thread Working..." + i);
                    Monitor.Pulse(_lock);
                    Monitor.Wait(_lock);
                    Console.WriteLine("Write Thread Completed..." + i);
                }
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }

        public static void Read()
        {
            Monitor.Enter(_lock);
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Monitor.Wait(_lock);
                    Console.WriteLine("Read Thread Working..." + i);
                    Monitor.Pulse(_lock);
                    Console.WriteLine("Read Thread Completed..." + i);
                }
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }

    }
}

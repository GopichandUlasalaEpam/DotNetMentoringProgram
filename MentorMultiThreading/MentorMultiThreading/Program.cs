using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MentorMultiThreading
{
    internal class Program
    {
        static int[] numberArray;
        static int firstSum = 0;
        static ManualResetEvent resetEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the array");
            int size = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter the array of integers (space-separated):");
            string input = Console.ReadLine();
            numberArray = Array.ConvertAll(input.Split(' '), int.Parse);
            int midPoint = numberArray.Length / 2;
            Console.WriteLine("Main method thread pause executing");
            //First Thread
            ThreadPool.QueueUserWorkItem(SumOperation, new LimitValues(0, midPoint));

            //SecondThread
            ThreadPool.QueueUserWorkItem(SumOperation, new LimitValues(midPoint, size));
            //making the main thread to wait
            resetEvent.WaitOne();

            Console.WriteLine("Main method thread starts executing");
            int overrallEvenSum = firstSum;
            Console.WriteLine($"From main thread the sum of even numbers of array is :{overrallEvenSum}");

        }
        static void SumOperation(object values)
        {
            int start = 0, end = 0;

            try
            {
                LimitValues limitValues = (LimitValues)values;
                start = limitValues.start;
                end = limitValues.end;
                resetEvent.Reset();
                for (int i = start; i < end; i++)
                {
                    if (numberArray[i] % 2 == 0)
                    {
                        Interlocked.Add(ref firstSum, numberArray[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Format exception occured kindly check the parameters passed");
            }
            finally
            {
                resetEvent.Set();
                if (start == 0)
                {
                    Console.WriteLine("Thread1 Completed sending signal to thread2 ");
                }
                else
                {
                    Console.WriteLine("Thread2 Completed sending signal to main thread");
                }

            }
            

        }
        public class LimitValues
        {
            public int start, end;
            public LimitValues(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }
    }
}

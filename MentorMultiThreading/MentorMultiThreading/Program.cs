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
        static int firstSum=0;
        static ManualResetEvent resetEvent=new ManualResetEvent(false);
        
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the array");
            int size =Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter the array of integers (space-separated):");
            string input = Console.ReadLine();
            numberArray = Array.ConvertAll(input.Split(' '), int.Parse);
            int midPoint = numberArray.Length / 2;
            //First Thread
            ThreadPool.QueueUserWorkItem(new WaitCallback(SumOperation), new LimitValues(0, midPoint));
            //SecondThread
            ThreadPool.QueueUserWorkItem(new WaitCallback(SumOperation), new LimitValues(midPoint, size));
            //making the main thread to wait
            resetEvent.WaitOne();
            Console.WriteLine("Main method thread");
            int overrallEvenSum = firstSum;
            Console.WriteLine($"From main thread the sum of even numbers of array is :{overrallEvenSum}");




        }
        static void SumOperation(object values)
        {
            LimitValues limitValues = (LimitValues) values;
            int start = limitValues.start;
            int end = limitValues.end;
            resetEvent.Reset();
            for (int i = start; i < end; i++)
            {
                if (numberArray[i] % 2 == 0)
                {
                    Interlocked.Add(ref firstSum, numberArray[i]);
                }
            }
            if (start != 0)
            {
                resetEvent.Set();
            }


        }
        public class LimitValues
        {
            public int start, end;
            public LimitValues(int start, int end) 
            { 
            this.start = start;
            this.end = end;}
        }
    }
}

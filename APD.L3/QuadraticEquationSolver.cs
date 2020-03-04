using System;
using System.Threading;

namespace APD.L3
{
    public class QuadraticEquationSolver
    {
        private int a;
        private int b;
        private int c;
        private double x1, x2, d;
        
        private readonly ManualResetEvent deltaReady;
        private readonly ManualResetEvent x1Ready;
        private readonly ManualResetEvent x2Ready;

        public QuadraticEquationSolver()
        {
            deltaReady = new ManualResetEvent(false);
            x1Ready = new ManualResetEvent(false);
            x2Ready = new ManualResetEvent(false);
        }

        public Tuple<double, double> Solve(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            var threadX1 = new Thread(ComputeX1);
            var threadX2 = new Thread(ComputeX2);
            var threadDelta = new Thread(ComputeDelta);

            threadDelta.Start();
            threadX1.Start();
            threadX2.Start();

            x1Ready.WaitOne();
            x2Ready.WaitOne();

            return new Tuple<double, double>(x1, x2);
        }

        private void ComputeDelta()
        {
            d = b * b - 4 * a * c; 
            Console.WriteLine("delta = " + d); 
            Console.WriteLine("threadDelta: sleeping"); 
            
            Thread.Sleep(5000); 
            
            Console.WriteLine("threadDelta: ready"); 
            deltaReady.Set();
        }

        private void ComputeX1()
        {
            Console.WriteLine("tx1: waiting..."); 
            deltaReady.WaitOne(); 
            
            x1 = (-b + Math.Sqrt(d)) / (2 * a); 
            
            Console.WriteLine("x1 = " + x1);
            x1Ready.Set();
        }

        private void ComputeX2()
        {
            Console.WriteLine("tx2: waiting..."); 
            deltaReady.WaitOne(); 
            
            x2 = (-b - Math.Sqrt(d)) / (2 * a); 
            
            Console.WriteLine("x2 = " + x2);
            x2Ready.Set();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DeliveryEngine
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Going to start schedule thread");

            Schedule schedulethr = new Schedule();
            Thread Schedulethread = new Thread(new ThreadStart(schedulethr.Start));
            Schedulethread.Start();

            Thread.Sleep(2000);

            
            Console.WriteLine("Going to start delivery thread");
            Delivery deliverythr = new Delivery();
            Thread Deliverythred = new Thread(new ThreadStart(deliverythr.Start));
            Deliverythred.Start();
            
            Console.WriteLine("Engine is running....");
        }
    }
}

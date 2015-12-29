using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lolbrary;

namespace Lolbrary.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                CPULock, MemoryLeak, and StorageFiller run on a separate thread, 
                so normal processing can/will continue.
            */

            //CPU Lock Example - check your CPU usage while this is running
            //CPULock locker = new CPULock();

            //Generates a file under C:\Temp that continually grows in size.
            //StorageFiller fill = new StorageFiller();

            //Throws an exception equivalent to a segmentation fault / memory access violation
            //SegFault.GenSegFault();

            //Gets "random" int and double and then adds them together
            //var x = RandomNumber.GetInt();
            //var y = RandomNumber.GetDouble();
            //Console.WriteLine(x);
            //Console.WriteLine(y);
            //Console.WriteLine(x + y);

            //Generates "nonsense" string
            //var nonsense = Nonsense.GetNonsense(Nonsense.NonsenseLength.Short).Result;
            //Console.WriteLine(nonsense);

            //Memory leak example - check memory usage while this is running
            MemoryLeak leaker = new MemoryLeak();
            while(leaker.WorkerIsRunning)
            {
                Console.WriteLine(leaker.AllocatedMemory);
            }

            Console.ReadLine();
        }
    }
}

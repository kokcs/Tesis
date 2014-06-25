using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CPURAM
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] procesos = Process.GetProcesses();
            string process = "ac_client";
            StreamWriter sw =  File.AppendText(".\\CPURAM.txt");
            
            int num1=0;

            //Console.WriteLine("No.\t%OfTimeThePprocessor\tPrivateWorkingSet\tRamUsage\tNetwork\tDate");
            Console.WriteLine("No.\t%CPU Usage\tRAM\tRamUsage\tNetwork\tDate");
            sw.WriteLine("No.\tCPU Usage\tRAM\tRAM Usage\tNetwork\tDate");
            sw.Close();
            while (true)
            {
                foreach (Process p in procesos)
                {
                    using (PerformanceCounter PerformanceCounter = new PerformanceCounter("Process", "% Processor Time", p.ProcessName))
                    {
                        using (PerformanceCounter performanceCounter2 = new PerformanceCounter("Process", "Working Set - Private", p.ProcessName))
                        {
                            using (PerformanceCounter performanceCounter3 = new PerformanceCounter("Process", "IO Data Bytes/sec", p.ProcessName))
                            {
                                if (p.ProcessName == process)
                                {
                                    try
                                    {
                                        

                                        sw = File.AppendText(".\\CPURAM.txt");
                                        ++num1;

                                        float num2  =  PerformanceCounter.NextValue();
                                        Thread.Sleep(1000);
                                        double num3 = performanceCounter3.NextValue();
                                        double num4 = performanceCounter2.NextValue();
                                        float num5 = PerformanceCounter.NextValue();

                                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t\t{4}\t{5}", new object[6]
                                        {
                                            (object) num1,
                                            (object) (float)((double)num5),
                                            (object) (num4/1024),
                                            (object) (p.WorkingSet64 / 1024L ),
                                            (object) num3,
                                            (object) DateTime.Now
                                         });

                                        sw.WriteLine("{0}\t{1}\t{2}\t{3}\t\t{4}\t\t{5}", (object)num1, (object)(float)((double)num5), (object)(num4/ 1024L), (object)(p.WorkingSet64 / 1024L ), (object)num3, (object)DateTime.Now);
                                        sw.Close();
                                    }
                                    catch (Exception)
                                    {

                                        throw;
                                    }

                                }
                            }

                        }

                    }

                }
                procesos = Process.GetProcesses();


            }
            

        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberGuvenlik
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arguments = new List<string>();

            arguments.Add(@"/c nmap -p 80 --script http-sql-injection,http-sitemap-generator,http-php-version,http-sql-injection,ssl-ccs-injection,http-csrf testphp.vulnweb.com -oX - ");


            foreach (var argument in arguments)
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = argument,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                List<string> lines = new List<string>();
                while (!proc.StandardOutput.EndOfStream)
                {
                    lines.Add(proc.StandardOutput.ReadLine());
                }

                DoSomething(lines);
                lines.Clear();
            }




            Console.ReadKey();
        }

        public static void DoSomething(List<string> lines)
        {
            System.IO.File.WriteAllLines(@"D:\kayit.xml", lines);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

    }
}

using System;
using System.Configuration;
using System.Timers;

namespace DynamicSection.SampleConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //To avoid been boring, after 10 seconds print something ..
            Timer timer = new Timer(10000);
            timer.Elapsed += Run;
            timer.Start();

            Console.WriteLine("Starting ...");
            Console.WriteLine(Environment.NewLine);

            //Let it's run now to show initial values on screen
            Console.WriteLine("Running first time to show default values");
            Run(null, null);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Now wait for timer next tick, after next '00' second frame and watch the magic !");
            Console.WriteLine(Environment.NewLine);
            Console.ReadLine();
        }

        private static void Run(object sender, ElapsedEventArgs e)
        {
            //As anyone will do this will access your custom configuration section
            var configurationSection = (VTEXCustomConfigurationSection)ConfigurationManager.GetSection("VTEXCustom");

            //Supposing you didn't removed it from config, lol ...
            if (configurationSection != null)
            {
                //Write current values
                Console.WriteLine(configurationSection.TestValue + " - " + configurationSection.OtherValue);
            }
        }
    }
}
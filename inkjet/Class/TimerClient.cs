using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using inkjet.UserControls;

namespace inkjet.Class
{
    public class TimerClient
    {
        public static System.Timers.Timer aTimer = new System.Timers.Timer();
        public static void Start_timer()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            //aTimer.Enabled = true;
         
            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            client.Program program = new client.Program();
            program.Execute_Client();
        }

    }
}

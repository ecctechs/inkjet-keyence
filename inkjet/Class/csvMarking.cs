using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace inkjet.Class
{
    public class csvMarking
    {
        //[Name(name: "Current Data")]
        //public string CurrentData { get; set; }

        //[Name(name: "Block No.")]
        //public string BlockNo { get; set; }

        //[Name(name: "Type")]
        //public string Type { get; set; }

        //[Name(name: "Size")]
        //public string Size { get; set; }

        [Name(name: "Detail")]
        public string Detail { get; set; }

    }

    public class csvMarking_chk
    {
        public bool chk { get; set; }

        private static bool csv_running = false;
        public static void set_running(bool chk)
        {
            csv_running = chk;
        }
        public static bool get_status_running()
        {
            return csv_running;
        }
    }
}

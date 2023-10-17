using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace inkjet.Class
{
    public class Shift
    {
        [Name(name: "NO.")]
        public int ShiftNo { get; set; }

        [Name(name: "Shift Name")]
        public string ShiftName { get; set; }

        [Name(name: "Start")]
        public string Start { get; set; }

        [Name(name: "End")]
        public string End { get; set; }
    }
}

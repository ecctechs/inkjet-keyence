using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace inkjet.Class
{
    public class Datalog
    {
        [Name(name: "Ink Jet")]
        public string InkJet { get; set; }

        [Name(name: "Program")]
        public string Program { get; set; }

        [Name(name: "Qty")]
        public string Qty { get; set; }

        [Name(name: "DateTime Start")]
        public string DateStart { get; set; }

        [Name(name: "DateTime End / Last Updated")]
        public string DateEnd { get; set; }

        [Name(name: "Shift")]
        public string Shift { get; set; }
    }
}

using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inkjet.Class
{
    public class CsvMarkingCreateCSV
    {
        [Name(name: "Detail")]
        public string Detail { get; set; }

        [Name(name: "Inkjet")]
        public string inkjet_name { get; set; }

        [Name(name: "timestamp")]
        public string Timestamp { get; set; }
    }
}

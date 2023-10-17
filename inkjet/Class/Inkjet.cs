using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace inkjet.Class
{
    public class Inkjet
    {
        [Name(name: "InkJet ID")]
        public int InkJetID { get; set; }

        [Name(name: "InkJet Name")]
        public string InkJetName { get; set; }

        [Name(name: "IP Address")]
        public string IPAdress { get; set; }

        [Name(name: "Status")]
        public string Status { get; set; }

        [Name(name: "Status_inkjet")]
        public string Status_inkjet { get; set; }

        [Name(name: "Ink")]
        public string Ink { get; set; }

        [Name(name: "Solvent")]
        public string Solvent { get; set; }

        [Name(name: "Pump")]
        public string Pump { get; set; }

        [Name(name: "Filter")]
        public string Filter { get; set; }

        [Name(name: "Program")]
        public string Program
        {
            get; set;
        }
    }
}

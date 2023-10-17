using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace inkjet.Class
{
    public class Error
    {
        //[Name(name: "Error ID")]
        //public int ErrorID { get; set; }

        [Name(name: "Ink Jet")]
        public string InkJet { get; set; }

        [Name(name: "Error Type")]
        public string ErrorType { get; set; }

        [Name(name: "Error Code")]
        public string ErrorCode { get; set; }

        [Name(name: "Detail")]
        public string Detail { get; set; }

        [Name(name: "Date")]
        public string Date { get; set; }

        [Name(name: "Time")]
        public string Time { get; set; }

        [Name(name: "Shift")]
        public string Shift { get; set; }

        [Name(name: "InK Level")]
        public string InKLevel { get; set; }

        [Name(name: "Solvent level")]
        public string SolventLevel { get; set; }

        [Name(name: "Pump Life time")]
        public string PumpLifetime { get; set; }

        [Name(name: "Filter Life time")]
        public string FilterLifetime { get; set; }


    }
}
